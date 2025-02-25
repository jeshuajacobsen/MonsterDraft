using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Zenject;

public class MainPhase : GameState
{
    public Camera mainCamera;
    public static UnityEvent ExitMainPhase = new UnityEvent();

    public MainPhase() : base() {
        currentState = new IdleState(this);
    }

    private List<Tile> validTargets = new List<Tile>();
    public int playedActionCardStep = 0;
    public List<SmallCardView> selectedCards = new List<SmallCardView>();
    public Card playedCard;
    public Card gainedCard;
    public int treasuresPlayed = 0;
    
    public List<Card> cardsToAutoPlay = new List<Card>();
    public bool autoPlaying = false;

    public Tile selectedTile;
    public int savedValue;
    public List<Card> foundCards = new List<Card>();

    private GameManager _gameManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, DiContainer container)
    {
        _gameManager = gameManager;
        _container = container;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Main Phase");
        RoundManager.instance.Actions = 1;
        RoundManager.instance.Mana = 0;
        RoundManager.instance.Coins = 0;
        mainCamera = Camera.main;
        currentState = new IdleState(this);
        currentState.EnterState();
        MoveMonstersForward();
        DecrementBuffDurations();
    }

    public void DecrementBuffDurations()
    {
        for (int row = 1; row <= 3; row++)
        {
            for (int tile = 1; tile <= 7; tile++)
            {
                Tile tileComponent = RoundManager.instance.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}").GetComponent<Tile>();
                if (tileComponent.monster != null)
                {
                    List<MonsterBuff> buffsToRemove = new List<MonsterBuff>();
                    foreach (var buff in tileComponent.monster.buffs)
                    {
                        buff.duration--;
                        if (buff.duration <= 0)
                        {
                            buffsToRemove.Add(buff);
                        }
                    }
                    foreach (var buff in buffsToRemove)
                    {
                        tileComponent.monster.buffs.Remove(buff);
                    }
                }
            }
        }
    }

    public void MoveMonstersForward()
    {
        for (int row = 1; row <= 3; row++)
        {
            for (int i = 7; i >= 1; i--)
            {
                Tile tile = RoundManager.instance.DungeonPanel.transform.Find($"CombatRow{row}/Tile{i}").GetComponent<Tile>();
                if (tile.monster != null && tile.monster.team == "Ally" && RoundManager.instance.CanMoveMonster(tile.monster))
                {
                    RoundManager.instance.MoveMonster(tile.monster);
                }
            }
        }
    }

    public override void UpdateState()
    {
        currentState.UpdateState();
    }

    public bool IsInsideOptionPanel(Vector3 position)
    {
        bool isInsideOptionPanel = RectTransformUtility.RectangleContainsScreenPoint(
            RoundManager.instance.monsterOptionPanel.GetComponent<RectTransform>(),
            position,
            mainCamera
        );
        if (RoundManager.instance.monsterOptionPanel.gameObject.activeSelf && isInsideOptionPanel)
        {
            return true;
        }
        return false;
    }

    public void HandleMouseInDungeon(Vector2 mousePosition)
    {
        for(int i = 1; i <= 7; i++)
        {
            RoundManager.instance.dungeonRow1.transform.Find("Tile" + i).GetComponent<Tile>().HandlePointerDown(mousePosition);
            RoundManager.instance.dungeonRow2.transform.Find("Tile" + i).GetComponent<Tile>().HandlePointerDown(mousePosition);
            RoundManager.instance.dungeonRow3.transform.Find("Tile" + i).GetComponent<Tile>().HandlePointerDown(mousePosition);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Main Phase");
        RoundManager.instance.Actions = 0;
        RoundManager.instance.DiscardHand();
        RoundManager.instance.Mana = 0;
        RoundManager.instance.Coins = 0;
        List<PersistentEffect> effectsToRemove = new List<PersistentEffect>();
        foreach (var effect in RoundManager.instance.persistentEffects)
        {
            effect.duration--;
            if (effect.duration <= 0)
            {
                effectsToRemove.Add(effect);
            }
        }
        foreach (var effect in effectsToRemove)
        {
            RoundManager.instance.persistentEffects.Remove(effect);
        }
        currentState.ExitState();
        ExitMainPhase.Invoke();
    }

    public override bool CanPlayCard(Card card, Vector3 position)
    {
        if (card is MonsterCard)
        {
            MonsterCard monsterCard = (MonsterCard)card;
            if (monsterCard.ManaCost > RoundManager.instance.Mana)
            {
                return false;
            }
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 7; tile++)
                {
                    Transform tileTransform = RoundManager.instance.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                    if (tileTransform != null && tileTransform.GetComponent<Collider2D>().bounds.Contains(position))
                    {
                        return true;
                    }
                }
            }
        }
        if (card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)card;
            if (RoundManager.instance.Actions > 0)
            {
                return true;
            }
        }
        if (card is TreasureCard)
        {
            return true;
        }
        CancelPartialPlay();
        return false;
    }

    public void CancelPartialPlay()
    {
        playedActionCardStep = 0;
        playedCard = null;
        if (!autoPlaying)
        {
            for (int j = 0; j < RoundManager.instance.hand.Count; j++)
            {
                if (RoundManager.instance.hand[j].isDragging)
                {
                    RoundManager.instance.hand[j].CancelPlay();
                }
            }
        }
        SwitchPhaseState(new IdleState(this));
    }

    public void CancelFullPlay()
    {
        playedActionCardStep = 0;
        if (!autoPlaying)
        {
            RoundManager.instance.AddCardToHand(playedCard);
        }
        playedCard = null;
        SwitchPhaseState(new IdleState(this));
    }

    public void FinishPlay()
    {
        if (!autoPlaying && playedCard is ActionCard)
        {
            RoundManager.instance.Actions--;
        }
        playedActionCardStep = 0;
        playedCard = null;
        ScrollRect scrollRect = RoundManager.instance.handContent.transform.GetComponentInParent<ScrollRect>();
        scrollRect.enabled = true;
        SwitchPhaseState(new IdleState(this));
    }

    public void FinishOnGain()
    {
        gainedCard = null;
        SwitchPhaseState(new IdleState(this));
    }

    public void RemoveCard(SmallCardView cardView)
    {
        RoundManager.instance.discardPile.AddCard(cardView.card);
        RoundManager.instance.RemoveCardFromHand(cardView);
        if (cardView != null)
        {
            cardView.DestroyCardView();
        }
    }

    public void PlayTreasureCard(TreasureCard treasureCard)
    {
        treasuresPlayed++;
        RoundManager.instance.Coins += treasureCard.CoinGeneration;
        RoundManager.instance.Mana += treasureCard.ManaGeneration;
        ScrollRect scrollRect = RoundManager.instance.handContent.transform.GetComponentInParent<ScrollRect>();
        scrollRect.enabled = true;
        if (treasureCard.Effects.Count > 0)
        {
            playedCard = treasureCard;
            SwitchPhaseState(new ResolvingEffectState(this));
        } else {
            SwitchPhaseState(new IdleState(this));
        }
    }

    public override void PlayCard(Card card, Vector3 position)
    {

    }

    private void PlayMonster(MonsterCard monsterCard, Tile target)
    {
        Monster newMonster = _container.InstantiatePrefabForComponent<Monster>(RoundManager.instance.MonsterPrefab, target.transform);
        newMonster.InitValues(monsterCard, target, "Ally");
        target.monster = newMonster;
    }

    public void PlayCardWithTarget(SmallCardView cardView, Tile target)
    {
        VisualEffect visualEffect = null;
        if (cardView.card is MonsterCard)
        {
            RoundManager.instance.Mana -= ((MonsterCard)cardView.card).ManaCost;
            PlayMonster((MonsterCard)cardView.card, target);
        }
        else if (cardView.card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)cardView.card;
            for (int i = playedActionCardStep; i < actionCard.Effects.Count; i++)
            {
                string[] effectParts = actionCard.Effects[i].Split(' ');
                if (effectParts[0] == "Damage")
                {
                    if (visualEffect != null)
                    {
                        visualEffect.reachedTarget.AddListener(() => {
                            target.monster.Health -= int.Parse(effectParts[1]);
                            RoundManager.instance.AddFloatyNumber(int.Parse(effectParts[1]), target, true);
                        });
                    } else {
                        target.monster.Health -= int.Parse(effectParts[1]);
                    }
                    playedActionCardStep++;
                } else if (effectParts[0] == "Heal")
                {
                    int heal = int.Parse(effectParts[1]);
                    target.monster.Health += heal;
                    playedActionCardStep++;
                } else if (effectParts[0] == "Buff")
                {
                    int buffValue = 0;
                    string buffType = effectParts[1];
                    string buffDescription = "";
                    int duration = 0;
                    if (effectParts[2] == "Plus")
                    {
                        buffValue = int.Parse(effectParts[3]);
                    } else if (effectParts[2] == "Minus")
                    {
                        buffValue = -int.Parse(effectParts[3]);
                    }
                    if (effectParts[4] == "Duration")
                    {
                        duration = int.Parse(effectParts[5]);
                    }
                    buffDescription = buffValue > 0 ? "+" : "-" + buffValue + " " + buffType;
                    
                    target.monster.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                    playedActionCardStep++;
                } else if (effectParts[0] == "Animate")
                {
                    if (effectParts[1] == "Fireball")
                    {
                        visualEffect = RoundManager.instance.AddVisualEffect("Fireball", target);
                        playedActionCardStep++;
                    }
                } else if (effectParts[0] == "Destroy")
                {
                    target.monster.Health = 0;
                    playedActionCardStep++;
                }
            }
            FinishPlay();
            RemoveCard(cardView);
            return;
        }
        RemoveCard(cardView);
        SwitchPhaseState(new IdleState(this));
    }

    public void AutoPlayMonsterCard(MonsterCard monsterCard, Tile target)
    {
        PlayMonster(monsterCard, target);
    }

    public override void SelectTile(Tile tile, Vector2 pointerPosition)
    {
        if (tile.monster != null && !tile.monster.IsOnInfoButton(pointerPosition))
        {
            RoundManager.instance.monsterOptionPanel.SetActive(true);
            RoundManager.instance.monsterOptionPanel
                .GetComponent<MonsterOptionsPanel>()
                .SetActiveTile(tile, pointerPosition);
        }
    }

    // public override void DoneButton()
    // {
    //     roundManager.doneButton.gameObject.SetActive(false);
    //     roundManager.cancelButton.gameObject.SetActive(false);
    //     if (selectingCards)
    //     {
    //         selectingCards = false;
    //         selectedCards.ForEach(cardView => {
    //             cardView.GetComponent<Image>().color = Color.white;
    //         });
    //         ContinuePlayingCard();
    //         selectedCards.Clear();
    //     }
    // }

    public void CancelButton()
    {
        CancelFullPlay();
        RoundManager.instance.doneButton.gameObject.SetActive(false);
        RoundManager.instance.cancelButton.gameObject.SetActive(false);
        if (playedCard != null)
        {
            RoundManager.instance.AddCardToHand(playedCard);
            playedCard = null;
        }
    }

    public override void SwitchPhaseState(CardPlayState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
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

    public override void EnterState()
    {
        Debug.Log("Entering Main Phase");
        _playerStats.Actions = 1;
        _playerStats.Mana = 0;
        _playerStats.Coins = 0;
        mainCamera = Camera.main;
        currentState = _container.Instantiate<IdleState>();
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
                Tile tileComponent = _uiManager.dungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}").GetComponent<Tile>();
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
                Tile tile = _uiManager.dungeonPanel.transform.Find($"CombatRow{row}/Tile{i}").GetComponent<Tile>();
                if (tile.monster != null && tile.monster.team == "Ally" && _roundManager.CanMoveMonster(tile.monster))
                {
                    _roundManager.MoveMonster(tile.monster);
                }
            }
        }
    }

    public override void UpdateState()
    {
        currentState.UpdateState();
    }

    

    public override void ExitState()
    {
        Debug.Log("Exiting Main Phase");
        _playerStats.Actions = 0;
        _roundManager.DiscardHand();
        _playerStats.Mana = 0;
        _playerStats.Coins = 0;
        List<PersistentEffect> effectsToRemove = new List<PersistentEffect>();
        foreach (var effect in _roundManager.persistentEffects)
        {
            effect.duration--;
            if (effect.duration <= 0)
            {
                effectsToRemove.Add(effect);
            }
        }
        foreach (var effect in effectsToRemove)
        {
            _roundManager.persistentEffects.Remove(effect);
        }
        currentState.ExitState();
        ExitMainPhase.Invoke();
    }

    public override bool CanPlayCard(Card card, Vector3 position)
    {
        if (card is MonsterCard)
        {
            MonsterCard monsterCard = (MonsterCard)card;
            if (monsterCard.ManaCost > _playerStats.Mana)
            {
                return false;
            }
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 7; tile++)
                {
                    Transform tileTransform = _uiManager.dungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
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
            if (_playerStats.Actions > 0)
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
            for (int j = 0; j < _roundManager.hand.Count; j++)
            {
                if (_roundManager.hand[j].isDragging)
                {
                    _roundManager.hand[j].CancelPlay();
                }
            }
        }
        SwitchPhaseState(_container.Instantiate<IdleState>());
    }

    public void CancelFullPlay()
    {
        playedActionCardStep = 0;
        if (!autoPlaying)
        {
            _roundManager.AddCardToHand(playedCard);
        }
        playedCard = null;
        SwitchPhaseState(_container.Instantiate<IdleState>());
    }

    public void FinishPlay()
    {
        if (!autoPlaying && playedCard is ActionCard)
        {
            _playerStats.Actions--;
        }
        playedActionCardStep = 0;
        playedCard = null;
        ScrollRect scrollRect = _roundManager.handContent.transform.GetComponentInParent<ScrollRect>();
        scrollRect.enabled = true;
        SwitchPhaseState(_container.Instantiate<IdleState>());
    }

    public void FinishOnGain()
    {
        gainedCard = null;
        SwitchPhaseState(_container.Instantiate<IdleState>());
    }

    public void RemoveCard(SmallCardView cardView)
    {
        _roundManager.discardPile.AddCard(cardView.card);
        _roundManager.RemoveCardFromHand(cardView);
        if (cardView != null)
        {
            cardView.DestroyCardView();
        }
    }

    public void PlayTreasureCard(TreasureCard treasureCard)
    {
        treasuresPlayed++;
        _playerStats.Coins += treasureCard.CoinGeneration;
        _playerStats.Mana += treasureCard.ManaGeneration;
        ScrollRect scrollRect = _roundManager.handContent.transform.GetComponentInParent<ScrollRect>();
        scrollRect.enabled = true;
        if (treasureCard.Effects.Count > 0)
        {
            playedCard = treasureCard;
            SwitchPhaseState(_container.Instantiate<ResolvingEffectState>());
        } else {
            SwitchPhaseState(_container.Instantiate<IdleState>());
        }
    }

    public override void PlayCard(Card card, Vector3 position)
    {

    }

    private void PlayMonster(MonsterCard monsterCard, Tile target)
    {
        Monster newMonster = _container.InstantiatePrefabForComponent<Monster>(_roundManager.MonsterPrefab, target.transform);
        newMonster.Initialize(monsterCard, target, "Ally");
        target.monster = newMonster;
    }

    public void PlayCardWithTarget(SmallCardView cardView, Tile target)
    {
        CardVisualEffect visualEffect = null;
        if (cardView.card is MonsterCard)
        {
            _playerStats.Mana -= ((MonsterCard)cardView.card).ManaCost;
            PlayMonster((MonsterCard)cardView.card, target);
        }
        else if (cardView.card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)cardView.card;
            playedCard = actionCard;
            for (int i = playedActionCardStep; i < actionCard.Effects.Count; i++)
            {
                string[] effectParts = actionCard.Effects[i].Split(' ');
                if (effectParts[0] == "Damage")
                {
                    if (visualEffect != null)
                    {
                        visualEffect.reachedTarget.AddListener(() => {
                            target.monster.Health -= int.Parse(effectParts[1]);
                            _roundManager.AddFloatyNumber(int.Parse(effectParts[1]), target, true);
                        });
                    } else {
                        target.monster.Health -= int.Parse(effectParts[1]);
                        _roundManager.AddFloatyNumber(int.Parse(effectParts[1]), target, true);
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
                        visualEffect = _roundManager.AddCardVisualEffect("Fireball", target);
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
        SwitchPhaseState(_container.Instantiate<IdleState>());
    }

    public void AutoPlayMonsterCard(MonsterCard monsterCard, Tile target)
    {
        PlayMonster(monsterCard, target);
    }

    public override void SwitchPhaseState(CardPlayState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
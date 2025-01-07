using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MainPhase : GameState
{
    public Camera mainCamera;
    public static UnityEvent ExitMainPhase = new UnityEvent();

    public MainPhase(RoundManager roundManager) : base(roundManager) {
        currentState = new IdleState(this);
    }

    private List<Tile> validTargets = new List<Tile>();
    public int playedActionCardStep = 0;
    public List<SmallCardView> selectedCards = new List<SmallCardView>();
    public Card playedCard;
    public CardPlayState currentState;
    public List<Card> cardsToAutoPlay = new List<Card>();
    public bool autoPlaying = false;

    public Tile selectedTile;

    public override void EnterState()
    {
        Debug.Log("Entering Main Phase");
        roundManager.Actions = 1;
        mainCamera = Camera.main;
        currentState = new IdleState(this);
        currentState.EnterState();
    }

    public override void UpdateState()
    {

        currentState.UpdateState();
    }

    public bool IsInsideOptionPanel(Vector3 position)
    {
        bool isInsideOptionPanel = RectTransformUtility.RectangleContainsScreenPoint(
            roundManager.monsterOptionPanel.GetComponent<RectTransform>(),
            position,
            mainCamera
        );
        if (roundManager.monsterOptionPanel.gameObject.activeSelf && isInsideOptionPanel)
        {
            return true;
        }
        return false;
    }

    public void HandleMouseInDungeon()
    {
        for(int i = 1; i <= 7; i++)
        {
            roundManager.dungeonRow1.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
            roundManager.dungeonRow2.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
            roundManager.dungeonRow3.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Main Phase");
        roundManager.Actions = 0;
        roundManager.DiscardHand();
        roundManager.Mana = 0;
        roundManager.Coins = 0;
        currentState.ExitState();
        ExitMainPhase.Invoke();
    }

    public override bool CanPlayCard(Card card, Vector3 position)
    {
        if (card is MonsterCard)
        {
            MonsterCard monsterCard = (MonsterCard)card;
            if (monsterCard.ManaCost > roundManager.Mana)
            {
                return false;
            }
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 7; tile++)
                {
                    Transform tileTransform = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
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
            if (roundManager.Actions > 0 && RequirementsMet(actionCard))
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
            for (int j = 0; j < roundManager.hand.Count; j++)
            {
                if (roundManager.hand[j].isDragging)
                {
                    roundManager.hand[j].CancelPlay();
                }
            }
        }
        SetState(new IdleState(this));
    }

    public void CancelFullPlay()
    {
        playedActionCardStep = 0;
        if (!autoPlaying)
        {
            roundManager.AddCardToHand(playedCard);
        }
        playedCard = null;
        SetState(new IdleState(this));
    }

    public void FinishPlay()
    {
        if (!autoPlaying)
        {
            roundManager.Actions--;
        }
        playedActionCardStep = 0;
        playedCard = null;
        ScrollRect scrollRect = roundManager.handContent.transform.GetComponentInParent<ScrollRect>();
        scrollRect.enabled = true;
        SetState(new IdleState(this));
    }


    public bool RequirementsMet(ActionCard actionCard)
    {
        if (actionCard.requirements.Count == 0)
        {
            return true;
        }
        foreach (string requirement in actionCard.requirements)
        {
            string[] requirementParts = requirement.Split(' ');
            if (requirementParts[0] == "Target")
            {
                if (requirementParts[1] == "Enemy")
                {
                    for (int row = 1; row <= 3; row++)
                    {
                        for (int tile = 1; tile <= 7; tile++)
                        {
                            Transform tileTransform = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                            if (tileTransform != null)
                            {
                                Tile tileComponent = tileTransform.GetComponent<Tile>();
                                if (tileComponent.monster != null && tileComponent.monster.team == "Enemy")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                    
                } else if (requirementParts[1] == "Ally")
                {
                    for (int row = 1; row <= 3; row++)
                    {
                        for (int tile = 1; tile <= 7; tile++)
                        {
                            Transform tileTransform = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                            if (tileTransform != null)
                            {
                                Tile tileComponent = tileTransform.GetComponent<Tile>();
                                if (tileComponent.monster != null && tileComponent.monster.team == "Player")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }

    public void RemoveCard(SmallCardView cardView)
    {
        roundManager.discardPile.AddCard(cardView.card);
        roundManager.RemoveCardFromHand(cardView);
        if (cardView != null)
        {
            Destroy(cardView.gameObject);
        }
    }

    public void PlayTreasureCard(TreasureCard treasureCard)
    {
        roundManager.Coins += treasureCard.GoldGeneration;
        roundManager.Mana += treasureCard.ManaGeneration;
        roundManager.discardPile.AddCard(treasureCard);
        SmallCardView treasureCardView = roundManager.hand.Find(x => x.card == treasureCard);
        roundManager.RemoveCardFromHand(treasureCardView);
        Destroy(treasureCardView.gameObject);
        ScrollRect scrollRect = roundManager.handContent.transform.GetComponentInParent<ScrollRect>();
        scrollRect.enabled = true;
        SetState(new IdleState(this));
    }

    public override void PlayCard(Card card, Vector3 position)
    {

    }

    public void PlayCardWithTarget(SmallCardView cardView, Tile target)
    {
        if (cardView.card is MonsterCard)
        {
            MonsterCard monsterCard = (MonsterCard)cardView.card;
            Monster newMonster = Instantiate(roundManager.MonsterPrefab, target.transform);
            newMonster.InitValues(monsterCard, target, "Player");
            target.monster = newMonster;
            roundManager.Mana -= monsterCard.ManaCost;
        }
        else if (cardView.card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)cardView.card;
            for (int i = playedActionCardStep; i < actionCard.effects.Count; i++)
            {
                string[] effectParts = actionCard.effects[i].Split(' ');
                if (effectParts[0] == "Damage")
                {
                    int damage = int.Parse(effectParts[1]);
                    target.monster.Health -= damage;
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
                }
            }
            FinishPlay();
            RemoveCard(cardView);
            return;
        }
        RemoveCard(cardView);
        SetState(new IdleState(this));
    }

    public override void SelectTile(Tile tile, Vector3 position)
    {
        if (tile.monster != null)
        {
            roundManager.monsterOptionPanel.gameObject.SetActive(true);
            roundManager.monsterOptionPanel.transform.position = position;
            Vector3 panelSize = roundManager.monsterOptionPanel.GetComponent<RectTransform>().sizeDelta;
            roundManager.monsterOptionPanel.transform.position += new Vector3(panelSize.x / 2, -panelSize.y / 2, 0);
            roundManager.monsterOptionPanel.transform.GetComponent<MonsterOptionsPanel>().SetActiveTile(tile);
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
        roundManager.doneButton.gameObject.SetActive(false);
        roundManager.cancelButton.gameObject.SetActive(false);
        if (playedCard != null)
        {
            roundManager.AddCardToHand(playedCard);
            playedCard = null;
        }
    }

    public void SetState(CardPlayState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
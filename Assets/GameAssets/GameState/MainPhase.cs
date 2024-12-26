using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MainPhase : GameState
{
    private Camera mainCamera;
    public static UnityEvent ExitMainPhase = new UnityEvent();

    public MainPhase(RoundManager roundManager) : base(roundManager) { }

    public bool selectingTarget = false;
    private List<Tile> validTargets = new List<Tile>();
    private int playedActionCardStep = 0;
    private Tile selectedTile;

    public override void EnterState()
    {
        Debug.Log("Entering Main Phase");
        roundManager.Actions = 1;
        mainCamera = Camera.main;
    }

    public override void UpdateState()
    {
        if (selectingTarget)
        {
            if (Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < validTargets.Count; i++)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(
                        validTargets[i].GetComponent<RectTransform>(),
                        Input.mousePosition,
                        mainCamera
                    ))
                    {
                        selectingTarget = false;
                        selectedTile = validTargets[i];
                        validTargets.ForEach(tile => {
                            if (tile != selectedTile)
                            {
                                tile.GetComponent<Image>().color = Color.white;
                            }
                        });
                        validTargets.Clear();
                        for (int j = 0; j < roundManager.hand.Count; j++)
                        {
                            if (roundManager.hand[j].isDragging)
                            {
                                roundManager.hand[j].HandleMouseUp();
                                return;
                            }
                        }
                        return;
                    }
                }
                for (int j = 0; j < roundManager.hand.Count; j++)
                {
                    if (roundManager.hand[j].isDragging)
                    {
                        roundManager.hand[j].CancelPlay();
                        selectingTarget = false;
                        validTargets.ForEach(tile => {
                            tile.GetComponent<Image>().color = Color.white;
                        });
                        validTargets.Clear();
                        playedActionCardStep = 0;
                        return;
                    }
                }
            }
        } else 
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool isInsideOptionPanel = RectTransformUtility.RectangleContainsScreenPoint(
                    roundManager.monsterOptionPanel.GetComponent<RectTransform>(),
                    Input.mousePosition,
                    mainCamera
                );
                if (roundManager.monsterOptionPanel.gameObject.activeSelf && isInsideOptionPanel)
                {
                    return;
                } else {
                    roundManager.monsterOptionPanel.gameObject.SetActive(false);
                    roundManager.hand.ForEach(cardView => {
                        if (cardView.card is ActionCard && RequirementsMet((ActionCard)cardView.card))
                        {
                            ActionCard actionCard = (ActionCard)cardView.card;
                            
                            cardView.HandleMouseDown();
                            if (cardView.isDragging && actionCard.StartsWithTarget())
                            {
                                MarkValidTargets(actionCard);
                                playedActionCardStep++;
                                selectingTarget = true;
                            }
                        } else {
                            cardView.HandleMouseDown();
                        }
                        
                    });
                    for(int i = 1; i <= 7; i++)
                    {
                        roundManager.dungeonRow1.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
                        roundManager.dungeonRow2.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
                        roundManager.dungeonRow3.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < roundManager.hand.Count; i++)
                {
                    if (roundManager.hand[i] == null) continue;
                    if (roundManager.hand[i].isDragging)
                    {
                        roundManager.hand[i].HandleMouseUp();
                        break;
                    }
                }
                // for(int i = 1; i <= 7; i++)
                // {
                //     dungeonRow1.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseUp();
                //     dungeonRow2.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseUp();
                //     dungeonRow3.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseUp();
                // }
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Main Phase");
        roundManager.Actions = 0;
        roundManager.DiscardHand();
        roundManager.Mana = 0;
        roundManager.Coins = 0;
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
        return false;
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

    public override void PlayCard(Card card, Vector3 position)
    {
        if (card is TreasureCard)
        {
            TreasureCard treasureCard = (TreasureCard)card;
            roundManager.Coins += treasureCard.GoldGeneration;
            roundManager.Mana += treasureCard.ManaGeneration;
            roundManager.discardPile.AddCard(card);
        }
        else if (card is MonsterCard && roundManager.DungeonPanel.activeSelf)
        {
            MonsterCard monsterCard = (MonsterCard)card;
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 7; tile++)
                {
                    Transform tileTransform = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                    if (tileTransform != null && tileTransform.GetComponent<Collider2D>().bounds.Contains(position))
                    {
                        Monster newMonster = Instantiate(roundManager.MonsterPrefab, tileTransform);
                        newMonster.InitValues(monsterCard, tileTransform.GetComponent<Tile>(), "Player");
                        tileTransform.GetComponent<Tile>().monster = newMonster;
                        roundManager.discardPile.AddCard(card);
                        roundManager.hand.Remove(roundManager.hand.Find(x => x.card == card));
                        roundManager.Mana -= monsterCard.ManaCost;
                        return;
                    }
                }
            }
        }
        else if (card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)card;
            for (int i = playedActionCardStep; i < actionCard.effects.Count; i++)
            {
                string[] effectParts = actionCard.effects[i].Split(' ');
                if (effectParts[0] == "Damage")
                {
                    int damage = int.Parse(effectParts[1]);
                    selectedTile.monster.Health -= damage;
                    playedActionCardStep++;
                } else if (effectParts[0] == "Heal")
                {
                    int heal = int.Parse(effectParts[1]);
                    selectedTile.monster.Health += heal;
                    playedActionCardStep++;
                }
            }
            playedActionCardStep = 0;
            if (selectedTile != null)
            {
                selectedTile.GetComponent<Image>().color = Color.white;
                selectedTile = null;
            }
            roundManager.Actions--;
            roundManager.discardPile.AddCard(card);
        }
        roundManager.hand.Remove(roundManager.hand.Find(x => x.card == card));
    }

    public void MarkValidTargets(ActionCard actionCard)
    {
        foreach (string effect in actionCard.effects)
        {
            string[] effectParts = effect.Split(' ');
            if (effectParts[0] == "Target")
            {
                selectingTarget = true;
                if (effectParts[1] == "Enemy")
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
                                    validTargets.Add(tileComponent);
                                    tileComponent.GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                                }
                            }
                        }
                    }
                } else if (effectParts[1] == "Ally")
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
                                    validTargets.Add(tileComponent);
                                    tileComponent.GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                                }
                            }
                        }
                    }
                }
            }
        }
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
}
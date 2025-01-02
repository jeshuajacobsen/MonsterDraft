using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingTargetMonsterState : CardPlayState
{
    private List<Tile> validTargets;
    private SmallCardView cardView;
    

    public SelectingTargetMonsterState(MainPhase mainPhase, SmallCardView cardView) : base(mainPhase)
    {
        this.cardView = cardView;
        validTargets = new List<Tile>();
    }

    public override void EnterState()
    {
        Debug.Log("Selecting Monster Tile State Entered");
        MarkValidTargets(cardView.card as ActionCard);
        //mainPhase.playedActionCardStep++;
    }

    public override void HandleInput()
    {
        // Handle target selection input
    }

    public override void UpdateState()
    {
        if (Input.GetMouseButtonUp(0))
        {
            HandleCardDrop(cardView, Input.mousePosition);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Selecting Monster Tile State");
        validTargets.ForEach(tile => tile.GetComponent<Image>().color = Color.white);
        validTargets.Clear();
    }

    public void MarkValidTargets(ActionCard actionCard)
    {
        foreach (string effect in actionCard.effects)
        {
            string[] effectParts = effect.Split(' ');
            if (effectParts[0] == "Target")
            {
                if (effectParts[1] == "Enemy")
                {
                    for (int row = 1; row <= 3; row++)
                    {
                        for (int tile = 1; tile <= 7; tile++)
                        {
                            Transform tileTransform = RoundManager.instance.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
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
                            Transform tileTransform = RoundManager.instance.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
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

    public void HandleCardDrop(SmallCardView cardView, Vector2 dropPosition)
    {
        RectTransform parentRect = RoundManager.instance.handContent.GetComponent<RectTransform>();
        bool isInsideHand = RectTransformUtility.RectangleContainsScreenPoint(
            parentRect,
            dropPosition,
            mainPhase.mainCamera
        );

        if (isInsideHand)
        {
            mainPhase.CancelPartialPlay();
            return;
        }
        for (int i = 0; i < validTargets.Count; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                validTargets[i].GetComponent<RectTransform>(),
                dropPosition,
                mainPhase.mainCamera
            ))
            {
                if (mainPhase.CanPlayCard(cardView.card, cardView.transform.position))
                {
                    mainPhase.PlayCardWithTarget((ActionCard)cardView.card, validTargets[i]);
                    return;
                }
            }
        }
        mainPhase.CancelPartialPlay();
    }
    
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuickSelectingTargetMonsterState : CardPlayState
{
    private List<Tile> validTargets;
    private SmallCardView cardView;
    

    public QuickSelectingTargetMonsterState Initialize(SmallCardView cardView)
    {
        this.cardView = cardView;
        validTargets = new List<Tile>();
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Quick Selecting Monster Tile State Entered");
        MarkValidTargets(cardView.card as ActionCard);
        //mainPhase.playedActionCardStep++;
    }

    public override void HandleInput()
    {
        // Handle target selection input
    }

    public override void UpdateState()
    {
        bool pointerUp = false;
        Vector2 pointerPosition = Vector2.zero;

        if (Input.GetMouseButtonUp(0))
        {
            pointerUp = true;
            pointerPosition = Input.mousePosition;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            pointerUp = true;
            pointerPosition = Input.GetTouch(0).position;
        } else {
            pointerPosition = Input.mousePosition;
        }

        if (pointerUp)
        {
            HandleCardDrop(cardView, pointerPosition);
        } else {
            cardView.HandleDrag(pointerPosition);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Quick Selecting Monster Tile State");
        validTargets.ForEach(tile => tile.GetComponent<Image>().color = Color.white);
        validTargets.ForEach(tile => tile.GetComponent<Image>().color = new Color(
            tile.GetComponent<Image>().color.r, tile.GetComponent<Image>().color.g, tile.GetComponent<Image>().color.b, 0.3f));
        validTargets.Clear();
    }

    public void MarkValidTargets(ActionCard actionCard)
    {
        foreach (string effect in actionCard.Effects)
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
                            Transform tileTransform = _uiManager.dungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
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
                            Transform tileTransform = _uiManager.dungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                            if (tileTransform != null)
                            {
                                Tile tileComponent = tileTransform.GetComponent<Tile>();
                                if (tileComponent.monster != null && tileComponent.monster.team == "Ally")
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
        MainPhase mainPhase = _roundManager.gameState as MainPhase;
        RectTransform parentRect = _cardManager.handContent.transform.parent.parent.parent.GetComponent<RectTransform>();
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
                    mainPhase.PlayCardWithTarget(cardView, validTargets[i]);
                    return;
                }
            }
        }
        mainPhase.CancelPartialPlay();
    }
    
}

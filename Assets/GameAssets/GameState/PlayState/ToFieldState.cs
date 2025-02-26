using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToFieldState : CardPlayState
{
    private SmallCardView cardView;
    

    public ToFieldState Initialize(SmallCardView cardView)
    {
        this.cardView = cardView;
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Play to field state Entered");
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
        Debug.Log("Exiting play to field State");
    }

    public void HandleCardDrop(SmallCardView cardView, Vector2 dropPosition)
    {
        RectTransform parentRect = _roundManager.handContent.transform.parent.parent.parent.GetComponent<RectTransform>();

        bool isInsideHand = RectTransformUtility.RectangleContainsScreenPoint(
            parentRect,
            dropPosition,
            mainPhase.mainCamera
        );

        if (isInsideHand || !mainPhase.CanPlayCard(cardView.card, dropPosition))
        {
            mainPhase.CancelPartialPlay();
            return;
        }

        if (cardView.card is TreasureCard)
        {
            mainPhase.RemoveCard(cardView);
            mainPhase.PlayTreasureCard((TreasureCard)cardView.card);
            return;
        }
        else if (cardView.card is ActionCard)
        {   
            mainPhase.playedCard = cardView.card;
            mainPhase.RemoveCard(cardView);
            mainPhase.SwitchPhaseState(_container.Instantiate<ResolvingEffectState>());
            return;
        }
        else
        {
            mainPhase.CancelPartialPlay();
        }
    }
    
}

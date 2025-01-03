using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToFieldState : CardPlayState
{
    private SmallCardView cardView;
    

    public ToFieldState(MainPhase mainPhase, SmallCardView cardView) : base(mainPhase)
    {
        this.cardView = cardView;
        
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
        if (Input.GetMouseButtonUp(0))
        {
            cardView.HandleMouseUp();
            HandleCardDrop(cardView, Input.mousePosition);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting play to field State");
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

        if (cardView.card is TreasureCard)
        {
            mainPhase.PlayTreasureCard((TreasureCard)cardView.card);
            return;
        }
        else if (cardView.card is ActionCard)
        {   
            mainPhase.playedCard = cardView.card;
            mainPhase.RemoveCard(cardView);
            mainPhase.SetState(new ResolvingEffectState(mainPhase));
            return;
        }
        else
        {
            mainPhase.CancelPartialPlay();
        }
    }
    
}

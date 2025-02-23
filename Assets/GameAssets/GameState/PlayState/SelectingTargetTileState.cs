using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingTargetTileState : CardPlayState
{
    private List<Tile> validTargets;
    private Card card;
    

    public SelectingTargetTileState(MainPhase mainPhase, Card card) : base(mainPhase)
    {
        this.card = card;
        validTargets = new List<Tile>();
    }

    public override void EnterState()
    {
        Debug.Log("Quick Selecting Monster Tile State Entered");
        MarkValidTargets(card as ActionCard);
        //mainPhase.playedActionCardStep++;
    }

    public override void HandleInput()
    {
        // Handle target selection input
    }

    public override void UpdateState()
    {
        bool pointerDown = false;
        Vector2 pointerPosition = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            pointerDown = true;
            pointerPosition = Input.mousePosition;
        }

        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pointerDown = true;
            pointerPosition = Input.GetTouch(0).position;
        }

        if (pointerDown)
        {
            for (int i = 0; i < validTargets.Count; i++)
            {
                RectTransform targetRect = validTargets[i].GetComponent<RectTransform>();

                if (RectTransformUtility.RectangleContainsScreenPoint(targetRect, pointerPosition, mainPhase.mainCamera))
                {
                    mainPhase.selectedTile = validTargets[i];
                    mainPhase.SwitchPhaseState(new ResolvingEffectState(mainPhase));
                    break;
                }
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Quick Selecting Monster Tile State");
        validTargets.ForEach(tile => tile.GetComponent<Image>().color = Color.white);
        validTargets.Clear();
    }

    public void MarkValidTargets(ActionCard actionCard)
    {
        string effect = actionCard.Effects[mainPhase.playedActionCardStep - 1];

        string[] effectParts = effect.Split(' ');
        if (effectParts[0] == "Target")
        {
            
        }
    }
    
}

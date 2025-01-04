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
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < validTargets.Count; i++)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(
                    validTargets[i].GetComponent<RectTransform>(),
                    Input.mousePosition,
                    mainPhase.mainCamera
                ))
                {
                    mainPhase.selectedTile = validTargets[i];
                    mainPhase.SetState(new ResolvingEffectState(mainPhase));
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
        string effect = actionCard.effects[mainPhase.playedActionCardStep - 1];

        string[] effectParts = effect.Split(' ');
        if (effectParts[0] == "Target")
        {
            
        }
    }
    
}

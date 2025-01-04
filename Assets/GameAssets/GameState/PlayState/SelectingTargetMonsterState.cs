using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingTargetMonsterState : CardPlayState
{
    private List<Tile> validTargets;
    private Card card;
    

    public SelectingTargetMonsterState(MainPhase mainPhase, Card card) : base(mainPhase)
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

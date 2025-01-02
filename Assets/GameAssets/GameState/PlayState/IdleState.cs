using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IdleState : CardPlayState
{
    public IdleState(MainPhase mainPhase) : base(mainPhase) { }

    public override void EnterState()
    {
        Debug.Log("Idle State Entered");
    }

    public override void HandleInput()
    {
        // Check for card play input and transition to other states as needed
    }

    public override void UpdateState() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mainPhase.IsInsideOptionPanel(Input.mousePosition))
            {
                return;
            } else {
                RoundManager.instance.monsterOptionPanel.gameObject.SetActive(false);
                foreach (var cardView in RoundManager.instance.hand)
                {
                    cardView.HandleMouseDown();

                    if (cardView.isDragging)
                    {
                        if (cardView.card is ActionCard)
                        {
                            ActionCard actionCard = (ActionCard)cardView.card;

                            if (actionCard.StartsWithTarget())
                            {
                                mainPhase.playedActionCardStep++;
                                mainPhase.SetState(new SelectingTargetMonsterState(mainPhase, cardView));
                            }
                            else
                            {
                                mainPhase.SetState(new ToFieldState(mainPhase, cardView));
                            }
                            
                        }
                        if (cardView.card is MonsterCard)
                        {
                            mainPhase.SetState(new SelectingMonsterTileState(mainPhase, cardView));
                        }
                        else if (cardView.card is TreasureCard)
                        {
                            mainPhase.SetState(new ToFieldState(mainPhase, cardView));
                        }

                        break;
                    }
                }
                mainPhase.HandleMouseInDungeon();
            }

        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Idle State");
    }
}

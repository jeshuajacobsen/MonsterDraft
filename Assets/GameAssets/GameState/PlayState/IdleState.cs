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
        mainPhase.selectedTile = null;
        mainPhase.selectedCards.Clear();
        mainPhase.playedActionCardStep = 0;
        mainPhase.playedCard = null;
        mainPhase.foundCards.Clear();
        if (mainPhase.cardsToAutoPlay.Count > 0)
        {
            mainPhase.autoPlaying = true;
            mainPhase.playedCard = mainPhase.cardsToAutoPlay[0];
            if (mainPhase.cardsToAutoPlay[0] is MonsterCard)
            {
                MonsterCard monsterCard = (MonsterCard)mainPhase.cardsToAutoPlay[0];
                mainPhase.cardsToAutoPlay.RemoveAt(0);
                mainPhase.SwitchPhaseState(new AutoPlayingMonsterState(mainPhase, monsterCard));
            } else {
                mainPhase.cardsToAutoPlay.RemoveAt(0);
                mainPhase.SwitchPhaseState(new ResolvingEffectState(mainPhase));
            }
        } else {
            mainPhase.autoPlaying = false;
        }
    }

    public override void HandleInput()
    {
        // Check for card play input and transition to other states as needed
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
            if (mainPhase.IsInsideOptionPanel(pointerPosition))
            {
                return;
            }
            else
            {
                RoundManager.instance.monsterOptionPanel.gameObject.SetActive(false);

                foreach (var cardView in RoundManager.instance.hand)
                {
                    cardView.HandleMouseDown(pointerPosition);

                    if (cardView.isDragging)
                    {
                        if (cardView.card is ActionCard actionCard)
                        {
                            if (actionCard.StartsWithTarget())
                            {
                                mainPhase.playedActionCardStep++;
                                mainPhase.SwitchPhaseState(new QuickSelectingTargetMonsterState(mainPhase, cardView));
                            }
                            else
                            {
                                mainPhase.SwitchPhaseState(new ToFieldState(mainPhase, cardView));
                            }
                        }
                        else if (cardView.card is MonsterCard)
                        {
                            mainPhase.SwitchPhaseState(new QuickSelectingMonsterTileState(mainPhase, cardView));
                        }
                        else if (cardView.card is TreasureCard)
                        {
                            mainPhase.SwitchPhaseState(new ToFieldState(mainPhase, cardView));
                        }
                        break;
                    }
                }
                
                mainPhase.HandleMouseInDungeon(Input.mousePosition);
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Idle State");
    }
}

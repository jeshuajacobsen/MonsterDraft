using System;
using UnityEngine;

public class ResolvingEffectState : CardPlayState
{

    public ResolvingEffectState(MainPhase mainPhase) : base(mainPhase) 
    { 
    }

    public override void EnterState()
    {
        Debug.Log("Resolving Effect State Entered");
        ResolveEffects();
    }

    public override void HandleInput() { }

    public override void UpdateState()
    {

        
    }

    private void ResolveEffects()
    {
        for (int i = mainPhase.playedActionCardStep; i < ((ActionCard)mainPhase.playedCard).effects.Count; i++)
        {
            string[] effectParts = ((ActionCard)mainPhase.playedCard).effects[i].Split(' ');
            if (effectParts[0] == "Actions")
            {
                RoundManager.instance.Actions += int.Parse(effectParts[1]);
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0]  == "Coins") 
            {
                RoundManager.instance.Coins += int.Parse(effectParts[1]);
                mainPhase.playedActionCardStep++;
                
            } else if (effectParts[0] == "Draw")
            {
                if (effectParts[1] == "x")
                {
                    int x = mainPhase.selectedCards.Count;
                    for (int j = 0; j < x; j++)
                    {
                        RoundManager.instance.AddCardToHand(RoundManager.instance.roundDeck.DrawCard());
                    }
                } else {
                    for (int j = 0; j < int.Parse(effectParts[1]); j++)
                    {
                        RoundManager.instance.AddCardToHand(RoundManager.instance.roundDeck.DrawCard());
                    }
                }
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Select")
            {
                mainPhase.playedActionCardStep++;
                if (effectParts[1] == "Cards")
                {
                    mainPhase.SetState(new SelectingCardsState(mainPhase, effectParts[2], "None"));
                } else if (effectParts[1] == "Treasure")
                {
                    mainPhase.SetState(new SelectingCardsState(mainPhase, effectParts[2], "Treasure"));
                }
                return;
            } else if (effectParts[0] == "Discard")
            {
                if (effectParts[1] == "x")
                {
                    int x = mainPhase.selectedCards.Count;
                    for (int j = 0; j < x; j++)
                    {
                        RoundManager.instance.discardPile.AddCard(mainPhase.selectedCards[j].card);
                        RoundManager.instance.hand.Remove(mainPhase.selectedCards[j]);
                        Destroy(mainPhase.selectedCards[j].gameObject);
                    }
                } else if (effectParts[1] == "Selected") 
                {
                    for (int j = 0; j < mainPhase.selectedCards.Count; j++)
                    {
                        RoundManager.instance.discardPile.AddCard(mainPhase.selectedCards[j].card);
                        RoundManager.instance.hand.Remove(mainPhase.selectedCards[j]);
                        Destroy(mainPhase.selectedCards[j].gameObject);
                    }
                }
                
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Trash")
            {
                if (effectParts[1] == "Selected") 
                {
                    for (int j = 0; j < mainPhase.selectedCards.Count; j++)
                    {
                        RoundManager.instance.hand.Remove(mainPhase.selectedCards[j]);
                        Destroy(mainPhase.selectedCards[j].gameObject);
                    }
                }
                
                mainPhase.playedActionCardStep++;

            } else if (effectParts[0] == "Gain")
            {
                mainPhase.playedActionCardStep++;
                if (effectParts[1] == "Treasure")
                {
                    string restriction = "Treasure";
                    if (effectParts[2] == "Costing")
                    {
                        if (effectParts[3] == "Selected")
                        {
                            if (effectParts[4] == "Cost")
                            {
                                
                                if (effectParts[5] == "Plus")
                                {
                                    int cost = 0;
                                    cost += mainPhase.selectedCards[0].card.Cost;
                                    cost += int.Parse(effectParts[6]);

                                    mainPhase.SetState(new GainingCardState(mainPhase, restriction, cost));
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        
        mainPhase.FinishPlay();
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Resolving Effect State");
    }
}

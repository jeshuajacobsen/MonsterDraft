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
                if (effectParts[1] == "x")
                {
                    if (effectParts[2] == "Times")
                    {
                        RoundManager.instance.Coins += mainPhase.selectedCards.Count * int.Parse(effectParts[3]);
                    }
                } else {
                    RoundManager.instance.Coins += int.Parse(effectParts[1]);
                }
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
                } else if (effectParts[1] == "Action")
                {
                    mainPhase.SetState(new SelectingCardsState(mainPhase, effectParts[2], "Action"));
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
                        RoundManager.instance.RemoveCardFromHand(mainPhase.selectedCards[j]);
                        Destroy(mainPhase.selectedCards[j].gameObject);
                    }
                } else if (effectParts[1] == "Selected") 
                {
                    for (int j = 0; j < mainPhase.selectedCards.Count; j++)
                    {
                        RoundManager.instance.discardPile.AddCard(mainPhase.selectedCards[j].card);
                        RoundManager.instance.RemoveCardFromHand(mainPhase.selectedCards[j]);
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
                        RoundManager.instance.RemoveCardFromHand(mainPhase.selectedCards[j]);
                        Destroy(mainPhase.selectedCards[j].gameObject);
                    }
                }
                
                mainPhase.playedActionCardStep++;

            } else if (effectParts[0] == "Gain")
            {
                string restriction = "None";
                mainPhase.playedActionCardStep++;
                if (effectParts[1] == "Treasure")
                {
                    restriction = "Treasure";
                }
                if (effectParts[2] == "Costing")
                {
                    if (effectParts[3] == "Selected")
                    {
                        if (effectParts[4] == "Cost")
                        {
                            
                            if (effectParts[5] == "Plus")
                            {
                                int cost = 0;
                                if (mainPhase.selectedCards.Count > 0)
                                {
                                    cost += mainPhase.selectedCards[0].card.Cost;
                                    cost += int.Parse(effectParts[6]);
                                    bool cancelable = mainPhase.playedActionCardStep == 1;
                                    mainPhase.SetState(new GainingCardState(mainPhase, restriction, cost, cancelable));
                                    return;
                                }
                            }
                        }
                    } else if (effectParts[3] == "Saved")
                    {
                        bool cancelable = mainPhase.playedActionCardStep == 1;
                        mainPhase.SetState(new GainingCardState(mainPhase, restriction, mainPhase.savedValue, cancelable));
                        return;
                    }
                }
            } else if (effectParts[0] == "Play")
            {
                mainPhase.playedActionCardStep++;
                if (effectParts[1] == "Selected")
                {
                    if (mainPhase.selectedCards.Count > 0)
                    {
                        for (int j = 0; j < int.Parse(effectParts[2]); j++)
                        {
                            mainPhase.cardsToAutoPlay.Add(mainPhase.selectedCards[0].card);
                        }
                        mainPhase.RemoveCard(mainPhase.selectedCards[0]);
                    }
                    
                }
            } else if (effectParts[0] == "Target")
            {
                mainPhase.playedActionCardStep++;
                mainPhase.SetState(new SelectingTargetMonsterState(mainPhase, mainPhase.playedCard));
            } else if (effectParts[0] == "Damage")
            {
                int damage = int.Parse(effectParts[1]);
                mainPhase.selectedTile.monster.Health -= damage;
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Heal")
            {
                int heal = int.Parse(effectParts[1]);
                mainPhase.selectedTile.monster.Health += heal;
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Buff")
            {
                int buffValue = 0;
                string buffType = effectParts[1];
                string buffDescription = "";
                int duration = 0;
                if (effectParts[2] == "Plus")
                {
                    buffValue = int.Parse(effectParts[3]);
                } else if (effectParts[2] == "Minus")
                {
                    buffValue = -int.Parse(effectParts[3]);
                }
                if (effectParts[4] == "Duration")
                {
                    duration = int.Parse(effectParts[5]);
                }
                buffDescription = buffValue > 0 ? "+" : "-" + buffValue + " " + buffType;
                
                mainPhase.selectedTile.monster.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Save")
            {
                if (effectParts[1] == "x")
                {
                    if (effectParts[2] == "Sum" && effectParts[3] == "Costs")
                    {
                        int sum = 0;
                        for (int j = 0; j < mainPhase.selectedCards.Count; j++)
                        {
                            sum += mainPhase.selectedCards[j].card.Cost;
                        }
                        mainPhase.savedValue = sum;
                    }
                }
                mainPhase.playedActionCardStep++;
            }
        }
        
        mainPhase.FinishPlay();
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Resolving Effect State");
    }
}

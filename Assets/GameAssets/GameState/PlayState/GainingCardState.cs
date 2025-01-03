using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GainingCardState : CardPlayState
{
    private int cost;
    private string restriction;
    private List<SmallCardView> selectedCards = new List<SmallCardView>();
    
    public GainingCardState(MainPhase mainPhase, string restriction, int cost) : base(mainPhase)
    {
        this.restriction = restriction;
        this.cost = cost;
    }

    public override void EnterState()
    {
        Debug.Log("Gaining card State Entered");
        RoundManager.instance.isGainingCard = true;
        RoundManager.instance.messageText.text = "Select a " + restriction +" card to gain costing up to " + cost + " coins";

    }

    public override void HandleInput()
    {
        // Handle target selection input
    }

    public override void UpdateState()
    {
        
    }

    public bool MeetsRestrictions(Card card)
    {
        if (card.Cost > cost)
        {
            return false;
        }
        if (restriction == "None")
        {
            return true;
        }
        else if (restriction == "Treasure")
        {
            if (card is TreasureCard)
            {
                return true;
            }
        }
        return false;
    }

    public void GainCard(Card card)
    {
        RoundManager.instance.discardPile.AddCard(card);
        mainPhase.SetState(new ResolvingEffectState(mainPhase));
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Gaining card State");
        RoundManager.instance.isGainingCard = false;
        RoundManager.instance.messageText.text = "";
    }
}

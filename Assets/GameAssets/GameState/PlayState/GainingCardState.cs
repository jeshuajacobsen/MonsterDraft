using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GainingCardState : CardPlayState
{
    private int cost;
    private string restriction;
    private List<SmallCardView> selectedCards = new List<SmallCardView>();
    private bool cancelable;
    
    public GainingCardState(MainPhase mainPhase, string restriction, int cost, bool cancelable = true) : base(mainPhase)
    {
        this.restriction = restriction;
        this.cost = cost;
        this.cancelable = cancelable;
    }

    public override void EnterState()
    {
        Debug.Log("Gaining card State Entered");
        RoundManager.instance.isGainingCard = true;
        RoundManager.instance.messageText.text = "Select a " + (restriction == "None" ? "" : restriction) +" card to gain costing up to " + cost + " coins";
        RoundManager.instance.messageText.gameObject.SetActive(true);
        RoundManager.instance.SetupDoneButton(cancelable);
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
        if (card.CoinCost > cost)
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
        mainPhase.SwitchPhaseState(new ResolvingEffectState(mainPhase));
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Gaining card State");
        RoundManager.instance.isGainingCard = false;
        RoundManager.instance.messageText.text = "";
        RoundManager.instance.messageText.gameObject.SetActive(false);
        RoundManager.instance.CleanupDoneButton();
    }
}

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
    
    public GainingCardState Initialize(string restriction, int cost, bool cancelable = true)
    {
        this.restriction = restriction;
        this.cost = cost;
        this.cancelable = cancelable;
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Gaining card State Entered");
        _roundManager.isGainingCard = true;
        _roundManager.messageText.text = "Select a " + (restriction == "None" ? "" : restriction) +" card to gain costing up to " + cost + " coins";
        _roundManager.messageText.gameObject.SetActive(true);
        _roundManager.SetupDoneButton(cancelable);
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
        _roundManager.discardPile.AddCard(card);
        mainPhase.SwitchPhaseState(_container.Instantiate<ResolvingEffectState>());
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Gaining card State");
        _roundManager.isGainingCard = false;
        _roundManager.messageText.text = "";
        _roundManager.messageText.gameObject.SetActive(false);
        _roundManager.CleanupDoneButton();
    }
}

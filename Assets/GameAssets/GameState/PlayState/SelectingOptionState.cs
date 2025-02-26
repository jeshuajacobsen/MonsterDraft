using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingOptionState : CardPlayState
{
    private List<string> options;
    private List<Card> displayCards;
    private bool showCards = false;
    
    public SelectingOptionState Initialize(List<string> options)
    {
        this.options = options;
        this.displayCards = new List<Card>();
        return this;
    }

    public SelectingOptionState Initialize(List<string> options, List<Card> displayCards)
    {
        this.options = options;
        this.displayCards = displayCards;
        this.showCards = true;
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Selecting option State Entered");
        if (this.showCards)
        {
            _roundManager.SetupOptionPanel(this.options, this.displayCards);
        }
        else
        {
            _roundManager.SetupOptionPanel(this.options);
        }
    }

    public override void HandleInput()
    {
        // Handle target selection input
    }

    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {
        Debug.Log("Exiting Selecting option State");
        _roundManager.SelectingOptionPanel.SetActive(false);;
    }
}

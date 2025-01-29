using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingOptionState : CardPlayState
{
    private List<string> options;
    private List<Card> displayCards;
    private bool showCards = false;
    
    public SelectingOptionState(MainPhase mainPhase, List<string> options) : base(mainPhase)
    {
        this.options = options;
        this.displayCards = new List<Card>();
    }

    public SelectingOptionState(MainPhase mainPhase, List<string> options, List<Card> displayCards) : base(mainPhase)
    {
        this.options = options;
        this.displayCards = displayCards;
        this.showCards = true;
    }

    public override void EnterState()
    {
        Debug.Log("Selecting option State Entered");
        if (this.showCards)
        {
            RoundManager.instance.SetupOptionPanel(this.options, this.displayCards);
        }
        else
        {
            RoundManager.instance.SetupOptionPanel(this.options);
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
        RoundManager.instance.SelectingOptionPanel.SetActive(false);;
    }
}

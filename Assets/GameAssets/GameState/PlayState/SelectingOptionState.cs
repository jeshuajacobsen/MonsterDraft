using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingOptionState : CardPlayState
{
    private List<string> options;
    
    public SelectingOptionState(MainPhase mainPhase, List<string> options) : base(mainPhase)
    {
        this.options = options;
    }

    public override void EnterState()
    {
        Debug.Log("Selecting option State Entered");
        RoundManager.instance.SetupOptionPanel(this.options);
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

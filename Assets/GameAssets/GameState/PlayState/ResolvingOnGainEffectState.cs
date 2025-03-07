using System;
using UnityEngine;
using System.Collections.Generic;

public class ResolvingOnGainEffectState : CardPlayState
{

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
        MainPhase mainPhase = _roundManager.gameState as MainPhase;
        for (int i = mainPhase.playedActionCardStep; i < ((ActionCard)mainPhase.gainedCard).OnGainEffects.Count; i++)
        {
            string[] effectParts = ((ActionCard)mainPhase.gainedCard).OnGainEffects[i].Split(' ');
            if (effectParts[0] == "Option")
            {
                List<string> options = new List<string>();
                for(int j = 1; j < effectParts.Length; j += 2)
                {
                    options.Add(effectParts[j] + " " + effectParts[j + 1]);
                }
                mainPhase.playedActionCardStep++;
                mainPhase.SwitchPhaseState(_container.Instantiate<SelectingOptionState>().Initialize(options));
                return;
            }
        }
        mainPhase.FinishOnGain();
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Resolving Effect State");
    }
}

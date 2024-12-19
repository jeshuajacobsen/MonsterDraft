using UnityEngine;
using System.Collections;

public class DrawPhase : GameState
{
    public DrawPhase(RoundManager roundManager) : base(roundManager) { }

    public override void EnterState()
    {
        Debug.Log("Entering Draw Phase");
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Draw Phase");
    }

    public override bool CanPlayCard(Card card, Vector3 position)
    {
        return false;
    }

    public override void PlayCard(Card card, Vector3 position)
    {
        Debug.LogError("Cards cannot be played during the Draw Phase!");
    }
}
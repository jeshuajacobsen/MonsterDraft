using UnityEngine;
using System.Collections;

public class DrawPhase : GameState
{
    public DrawPhase(RoundManager roundManager) : base(roundManager) { }

    public override void EnterState()
    {
        Debug.Log("Entering Draw Phase");
        roundManager.DiscardHand();
        for (int i = 0; i < 5; i++)
        {
            roundManager.AddCardToHand(roundManager.roundDeck.DrawCard());
        }
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

    public override void SelectTile(Tile tile, Vector3 position)
    {
        Debug.LogError("Tiles cannot be selected during the Draw Phase!");
    }
}
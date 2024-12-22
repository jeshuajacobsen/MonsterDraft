using UnityEngine;

public class EnemyPhase : GameState
{
    public EnemyPhase(RoundManager roundManager) : base(roundManager) { }

    public override void EnterState()
    {
        Debug.Log("Entering Enemy Phase");
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Enemy Phase");
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
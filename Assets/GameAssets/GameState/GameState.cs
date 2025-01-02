using UnityEngine;

public abstract class GameState : MonoBehaviour
{


    protected RoundManager roundManager;
    public GameState(RoundManager roundManager)
    {
        this.roundManager = roundManager;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract bool CanPlayCard(Card card, Vector3 position);
    public abstract void PlayCard(Card card, Vector3 position);

    public abstract void SelectTile(Tile tile, Vector3 position);

}
using UnityEngine;

public abstract class CardPlayState : MonoBehaviour
{
    protected MainPhase mainPhase;

    public CardPlayState(MainPhase mainPhase)
    {
        this.mainPhase = mainPhase;
    }

    public abstract void EnterState();
    public abstract void HandleInput();
    public abstract void UpdateState();
    public abstract void ExitState();
}

using UnityEngine;
using Zenject;

public abstract class CardPlayState
{
    protected MainPhase mainPhase;

    protected RoundManager _roundManager;
    protected DiContainer _container;

    [Inject]
    public void Construct(RoundManager roundManager, DiContainer container)
    {
        _roundManager = roundManager;
        mainPhase = (MainPhase)_roundManager.gameState;
        _container = container;
    }

    public abstract void EnterState();
    public abstract void HandleInput();
    public abstract void UpdateState();
    public abstract void ExitState();
}

using UnityEngine;
using Zenject;

public abstract class CardPlayState
{
    protected MainPhase mainPhase;

    protected RoundManager _roundManager;
    protected RoundUIManager _uiManager;
    protected DungeonManager _dungeonManager;
    protected DiContainer _container;
    protected PlayerStats _playerStats;

    [Inject]
    public void Construct(RoundManager roundManager, 
                          PlayerStats playerStats, 
                          RoundUIManager uiManager,
                          DungeonManager dungeonManager, 
                          DiContainer container)
    {
        _roundManager = roundManager;
        _playerStats = playerStats;
        _uiManager = uiManager;
        _dungeonManager = dungeonManager;
        mainPhase = (MainPhase)_roundManager.gameState;
        _container = container;
    }

    public abstract void EnterState();
    public abstract void HandleInput();
    public abstract void UpdateState();
    public abstract void ExitState();
}

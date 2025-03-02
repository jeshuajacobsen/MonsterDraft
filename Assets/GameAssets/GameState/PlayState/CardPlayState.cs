using UnityEngine;
using Zenject;

public abstract class CardPlayState
{
    protected RoundManager _roundManager;
    protected RoundUIManager _uiManager;
    protected DungeonManager _dungeonManager;
    protected CardManager _cardManager;
    protected CombatManager _combatManager;
    protected VisualEffectManager _visualEffectManager;
    protected DiContainer _container;
    protected PlayerStats _playerStats;

    [Inject]
    public void Construct(RoundManager roundManager, 
                          PlayerStats playerStats, 
                          RoundUIManager uiManager,
                          DungeonManager dungeonManager,
                          CardManager cardManager,
                          VisualEffectManager visualEffectManager,
                          CombatManager combatManager,
                          DiContainer container)
    {
        _roundManager = roundManager;
        _playerStats = playerStats;
        _uiManager = uiManager;
        _dungeonManager = dungeonManager;
        _cardManager = cardManager;
        _combatManager = combatManager;
        _visualEffectManager = visualEffectManager;
        _container = container;
    }

    public abstract void EnterState();
    public abstract void HandleInput();
    public abstract void UpdateState();
    public abstract void ExitState();
}

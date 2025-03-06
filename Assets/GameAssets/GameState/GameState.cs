using UnityEngine;
using Zenject;

public abstract class GameState
{
    public CardPlayState currentState;
    public Camera mainCamera;

    protected IGameManager _gameManager;
    protected RoundManager _roundManager;
    protected RoundUIManager _uiManager;
    protected CardManager _cardManager;
    protected CombatManager _combatManager;
    protected VisualEffectManager _visualEffectManager;
    protected Monster.Factory _monsterFactory;
    protected PlayerStats _playerStats;
    protected DiContainer _container;

    [Inject]
    public void Construct(IGameManager gameManager, 
                          RoundManager roundManager, 
                          RoundUIManager uiManager, 
                          CardManager cardManager,
                          CombatManager combatManager,
                          VisualEffectManager visualEffectManager,
                          Monster.Factory monsterFactory,
                          PlayerStats playerStats, 
                          DiContainer container)
    {
        _gameManager = gameManager;
        _roundManager = roundManager;
        _uiManager = uiManager;
        _cardManager = cardManager;
        _combatManager = combatManager;
        _visualEffectManager = visualEffectManager;
        _monsterFactory = monsterFactory;
        _playerStats = playerStats;
        _container = container;
    }

    public GameState()
    {
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract bool CanPlayCard(Card card, Vector3 position);
    public abstract void PlayCard(Card card, Vector3 position);

    public abstract void SwitchPhaseState(CardPlayState newState);

}
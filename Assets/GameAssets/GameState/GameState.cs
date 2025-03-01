using UnityEngine;
using Zenject;

public abstract class GameState
{
    public CardPlayState currentState;

    protected GameManager _gameManager;
    protected RoundManager _roundManager;
    protected RoundUIManager _uiManager;
    protected PlayerStats _playerStats;
    protected DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, RoundManager roundManager, RoundUIManager uiManager, PlayerStats playerStats, DiContainer container)
    {
        _gameManager = gameManager;
        _roundManager = roundManager;
        _uiManager = uiManager;
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
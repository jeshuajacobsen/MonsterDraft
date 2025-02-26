using UnityEngine;
using Zenject;

public abstract class GameState
{
    public CardPlayState currentState;

    protected GameManager _gameManager;
    protected DiContainer _container;
    protected RoundManager _roundManager;

    [Inject]
    public void Construct(GameManager gameManager, RoundManager roundManager, DiContainer container)
    {
        _gameManager = gameManager;
        _roundManager = roundManager;
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

    public abstract void SelectTile(Tile tile, Vector2 position);

    public abstract void SwitchPhaseState(CardPlayState newState);

}
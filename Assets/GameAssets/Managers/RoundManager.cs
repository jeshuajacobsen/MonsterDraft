using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class RoundManager : MonoBehaviour
{
    public GameObject roundPanel;

    public GameState gameState;
    public LayerMask draggableLayer; 

    public List<Card> cardsGainedThisRound = new List<Card>();

    public List<PersistentEffect> persistentEffects = new List<PersistentEffect>();

    private IGameManager _gameManager;
    private RunManager _runManager;
    private DiContainer _container;



    [Inject]
    public void Construct(IGameManager gameManager, 
                          RunManager runManager,
                          DiContainer container)
    {
        _gameManager = gameManager;
        _runManager = runManager;
        _container = container;
    }

    void Awake()
    {

    }

    public void Start()
    {
        _runManager.startRoundEvent.AddListener(StartRound);
    }

    public void Update()
    {
        try
        {
            if (gameState != null)
            {
                gameState.UpdateState();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void OnDoneButtonClicked()
    {
        if (gameState is AutoPlayingMonsterState)
        {
            gameState.SwitchPhaseState(_container.Instantiate<IdleState>());
        }
        else
        {
            gameState.SwitchPhaseState(_container.Instantiate<ResolvingEffectState>());
        }
    }

    public void RefundGemsAndReturnToIdle(int gemCost)
    {
        _gameManager.Gems += gemCost;
        gameState.SwitchPhaseState(_container.Instantiate<IdleState>());
    }

    public void StartRound()
    {
        cardsGainedThisRound.Clear();
        roundPanel.gameObject.SetActive(true);
        roundPanel.transform.Find("TownPanel").gameObject.SetActive(true);
        //TODO add guaranteed cards to dungeon data.
        roundPanel.transform.Find("TownPanel").GetComponent<TownPanel>().StartRound(new List<string> {});
        
        gameState = _container.Instantiate<MainPhase>();
        gameState.EnterState();
    }

    public void SwitchState(GameState newState)
    {
        gameState?.ExitState();
        gameState = newState;
        gameState.EnterState();
    }

    public void EndTurn()
    {
        if (gameState is MainPhase)
        {
            SwitchState(_container.Instantiate<DrawPhase>());
            SwitchState(_container.Instantiate<EnemyPhase>());
            SwitchState(_container.Instantiate<MainPhase>());
        }
    }

    



}
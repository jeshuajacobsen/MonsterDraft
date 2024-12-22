using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    [SerializeField] private GameObject roundPanel;
    [SerializeField] private SmallCardView SmallCardViewPrefab;
    [SerializeField] private GameObject HandContent;
    public GameObject DungeonPanel;
    public Monster MonsterPrefab;

    public GameState gameState;

    public RoundDeck roundDeck;
    public DiscardPile discardPile;

    public LayerMask draggableLayer; 

    public List<SmallCardView> hand = new List<SmallCardView>();
    public DungeonRow dungeonRow1;
    public DungeonRow dungeonRow2;
    public DungeonRow dungeonRow3;

    [SerializeField] private GameObject DeckDiscardPanel;
    public GameObject monsterOptionPanel;
    private int _coins;
    public int Coins { get { return _coins; } 
        set { 
            _coins = value; 
            DeckDiscardPanel.transform.Find("CoinsImage").Find("Text").GetComponent<TextMeshProUGUI>().text = value.ToString();
        } 
    }

    private int _mana;
    public int Mana { get { return _mana; } 
        set { 
            _mana = value; 
            DeckDiscardPanel.transform.Find("ManaImage").Find("Text").GetComponent<TextMeshProUGUI>().text = value.ToString();
        } 
    }

    private int _actions;
    public int Actions { get { return _actions; } 
        set { 
            _actions = value; 
            DeckDiscardPanel.transform.Find("ActionsImage").Find("Text").GetComponent<TextMeshProUGUI>().text = value.ToString();
        } 
    }

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        try
        {
            gameState.UpdateState();
        }
        catch (Exception e)
        {
        }
    }

    public void StartRound()
    {
        roundPanel.gameObject.SetActive(true);
        roundPanel.transform.Find("TownPanel").gameObject.SetActive(true);
        roundPanel.transform.Find("TownPanel").GetComponent<TownPanel>().StartRound();
        roundDeck = new RoundDeck(RunManager.instance.runDeck);
        List<Card> newHand = roundDeck.DrawHand();
        foreach (Card card in newHand)
        {
            SmallCardView newCard = Instantiate(SmallCardViewPrefab, HandContent.transform);
            newCard.InitValues(card);
            hand.Add(newCard);
        }
        gameState = new MainPhase(this);
        gameState.EnterState();
    }

    public void SwitchState(GameState newState)
    {
        gameState?.ExitState(); // Exit the current state
        gameState = newState;
        gameState.EnterState(); // Enter the new state
    }

    public void EndTurn()
    {
        if (gameState is MainPhase)
        {
            SwitchState(new DrawPhase(this));
            SwitchState(new EnemyPhase(this));
            SwitchState(new MainPhase(this));
        }
    }

    public void DiscardHand()
    {
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            if (hand[i] != null)
            {
                discardPile.AddCard(hand[i].card);
                Destroy(hand[i].gameObject);
            }
        }
        hand.Clear();
    }

    public void AddCardToHand(Card card)
    {
        SmallCardView newCard = Instantiate(SmallCardViewPrefab, HandContent.transform);
        newCard.InitValues(card);
        hand.Add(newCard);
    }

    public void MoveMonster(Monster monster)
    {
        int maxDistance = monster.Movement;
        for (int i = 1; i <= monster.Movement; i++)
        {
            Tile nextTile = monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, i);
            if (nextTile != null && nextTile.GetComponent<Tile>().monster != null)
            {
                maxDistance = i;
                break;
            }
        }
        string tileName = monster.tileOn.name;
        int currentTileNumber = int.Parse(tileName.Substring(4));
        Transform parentTransform = monster.tileOn.transform.parent;
        int newTileNumber = Math.Min(currentTileNumber + monster.Movement, currentTileNumber + maxDistance);
        if (newTileNumber > 7)
        {
            newTileNumber = 7;
        }
        Transform newTile = parentTransform.Find("Tile" + newTileNumber);
        monster.tileOn.monster = null;
Debug.Log("Moving monster to " + newTile.name);
        monster.MoveTile(newTile.GetComponent<Tile>());
    }
}
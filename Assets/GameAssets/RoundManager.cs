using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    public GameObject roundPanel;
    [SerializeField] private SmallCardView SmallCardViewPrefab;
    public GameObject handContent;
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
    public Dungeon currentDungeon;

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

    public GameObject EnemyBase;
    public GameObject PlayerBase;

    public LargeCardView largeCardView;
    public LargeMonsterView largeMonsterView;

    public Button doneButton;
    public Button cancelButton;
    public bool isGainingCard = false;
    public TextMeshProUGUI messageText;

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
            Debug.Log(e);
        }
    }

    public void SetupDoneButton()
    {
        doneButton.gameObject.SetActive(true);
        doneButton.onClick.AddListener(OnDoneButtonClicked);
        if (gameState is MainPhase && ((MainPhase)gameState).autoPlaying)
        {
            return;
        }
        
        cancelButton.gameObject.SetActive(true);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    public void CleanupDoneButton()
    {
        doneButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        doneButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    public void OnDoneButtonClicked()
    {
        Debug.Log("OnDoneButtonClicked called");
        MainPhase mainPhase = (MainPhase)gameState;
        mainPhase.SetState(new ResolvingEffectState(mainPhase));
    }

    public void OnCancelButtonClicked()
    {
        MainPhase mainPhase = (MainPhase)gameState;
        mainPhase.CancelFullPlay();
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
            AddCardToHand(card);
        }
        currentDungeon = new Dungeon("Dungeon1");
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

        foreach (Transform child in handContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddCardToHand(Card card)
    {
        SmallCardView newCard = Instantiate(SmallCardViewPrefab, handContent.transform);
        newCard.InitValues(card);
        hand.Add(newCard);
        RectTransform newItemRect = newCard.GetComponent<RectTransform>();

        ResizeHand();
    }

    public void RemoveCardFromHand(SmallCardView cardView)
    {
        hand.Remove(cardView);
        Destroy(cardView.gameObject);
        ResizeHand();
    }

    public void ResizeHand()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            RectTransform newItemRect = hand[i].GetComponent<RectTransform>();
            float itemWidth = newItemRect.rect.width;
            newItemRect.anchoredPosition = new Vector2(i * (itemWidth + 10) + itemWidth/2, newItemRect.rect.height / 2 + 20);
        }
        float totalWidth = hand.Count * SmallCardViewPrefab.GetComponent<RectTransform>().rect.width + 10;
        handContent.GetComponent<RectTransform>().sizeDelta = new Vector2(totalWidth, handContent.GetComponent<RectTransform>().rect.height);
    }

    public void MoveMonster(Monster monster)
    {
        int maxDistance = monster.Movement;
        for (int i = 1; i <= monster.Movement; i++)
        {
            Tile nextTile = monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, i);
            if (nextTile != null && nextTile.GetComponent<Tile>().monster != null)
            {
                maxDistance = i - 1;
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
        if (newTileNumber < 1)
        {
            newTileNumber = 1;
        }
        Transform newTile = parentTransform.Find("Tile" + newTileNumber);
        monster.tileOn.monster = null;
        monster.MoveTile(newTile.GetComponent<Tile>());
    }

    public bool CanMoveMonster(Monster monster)
    {
        Tile nextTile = monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, 1);
        return nextTile != null && nextTile.GetComponent<Tile>().monster == null;
    }

    public bool EnemyCanMove(Monster monster)
    {
        return CheckForMonster(monster.tileOn, -1) == null;
    }

    public void MoveEnemyMonster(Monster monster)
    {

        int maxDistance = monster.Movement;
        for (int i = 1; i <= monster.Movement; i++)
        {
            Monster blockingMonster = CheckForMonster(monster.tileOn, -i);
            if (blockingMonster != null)
            {
                maxDistance = i - 1;
                break;
            }
        }
        string tileName = monster.tileOn.name;
        int currentTileNumber = int.Parse(tileName.Substring(4));
        Transform parentTransform = monster.tileOn.transform.parent;
        int newTileNumber = Math.Max(currentTileNumber - monster.Movement, currentTileNumber - maxDistance);
        if (newTileNumber > 7)
        {
            newTileNumber = 7;
        }
        if (newTileNumber < 1)
        {
            newTileNumber = 1;
        }
        Transform newTile = parentTransform.Find("Tile" + newTileNumber);
        monster.tileOn.monster = null;
        monster.tileOn = newTile.GetComponent<Tile>();
        monster.MoveTile(newTile.GetComponent<Tile>());

        
    }

    public void UseSkill(Monster monster, SkillData skill)
    {
        if(skill.ManaCost <= Mana)
        {
            Mana -= skill.ManaCost;
            for (int i = 1; i <= skill.Range; i++)
            {
                Monster target = CheckForMonster(monster.tileOn, i);
                int currentTileNumber = int.Parse(monster.tileOn.name.Substring(4));
                if (target != null && target.team == "Enemy")
                {
                    target.Health -= DamageDealtToMonster(target, monster, skill);
                    monster.actionsUsedThisTurn.Add(skill.name);
                    break;
                } else if (currentTileNumber + i > 7)
                {
                    EnemyBase.GetComponent<EnemyBase>().Health -= skill.Damage;
                    monster.actionsUsedThisTurn.Add(skill.name);
                    break;
                }
            }
        }
    }

    public int DamageDealtToMonster(Monster defender, Monster attacker, SkillData skill)
    {
        float damageMultiplier = (float)attacker.Attack / (attacker.Attack + defender.Defense);
        int damage = Mathf.RoundToInt(damageMultiplier * skill.Damage);
        return damage;
    }

    public bool EnemyCanUseSkill(Monster monster, SkillData skill)
    {
        for (int i = 1; i <= skill.Range; i++)
        {
            Monster target = CheckForMonster(monster.tileOn, -i);
            int currentTileNumber = int.Parse(monster.tileOn.name.Substring(4));
            if ((target != null && target.team == "Player") || currentTileNumber - i < 1)
            {
                return true;
            }
        }
        return false;
    }

    public void EnemyUseSkill(Monster monster, SkillData skill)
    {
        for (int i = 1; i <= skill.Range; i++)
        {
            int currentTileNumber = int.Parse(monster.tileOn.name.Substring(4));
            Monster target = CheckForMonster(monster.tileOn, -i);
            if (target != null)
            {
                target.Health -= DamageDealtToMonster(target, monster, skill);
                monster.actionsUsedThisTurn.Add(skill.name);
                break;
            } else if (currentTileNumber - i < 1)
            {
                PlayerBase.GetComponent<PlayerBase>().Health -= skill.Damage;
                break;
            }
        }
    }

    public static Monster CheckForMonster(Tile currentTile, int distance)
    {
        Tile nextTile;
        if (distance < 0)
        {
            nextTile = currentTile.dungeonRow.GetPreviousTile(currentTile, -distance);
            if (nextTile != null && nextTile.monster != null)
            {
                return nextTile.monster;
            }
            return null;
        }
        nextTile = currentTile.dungeonRow.GetNextTile(currentTile, distance);
        if (nextTile != null && nextTile.monster != null)
        {
            return nextTile.monster;
        }
        return null;
    }
}
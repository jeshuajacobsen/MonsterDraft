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

    public List<Card> cardsGainedThisRound = new List<Card>();
    public string dungeonName;

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
            //Debug.Log(e);
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

    public void StartRound(string dungeonName, int roundNumber)
    {
        this.dungeonName = dungeonName;
        foreach (DungeonRow row in new DungeonRow[] { dungeonRow1, dungeonRow2, dungeonRow3 })
        {
            foreach (Transform tileTransform in row.transform)
            {
                Tile tile = tileTransform.GetComponent<Tile>();
                if (tile != null && tile.monster != null)
                {
                    Destroy(tile.monster.gameObject);
                    tile.monster = null;
                }
            } 
        }
        PlayerBase.GetComponent<PlayerBase>().MaxHealth = 20;
        EnemyBase.GetComponent<EnemyBase>().MaxHealth = 20;
        PlayerBase.GetComponent<PlayerBase>().Health = PlayerBase.GetComponent<PlayerBase>().MaxHealth;
        EnemyBase.GetComponent<EnemyBase>().Health = EnemyBase.GetComponent<EnemyBase>().MaxHealth;
        roundPanel.gameObject.SetActive(true);
        roundPanel.transform.Find("TownPanel").gameObject.SetActive(true);
        roundPanel.transform.Find("TownPanel").GetComponent<TownPanel>().StartRound();
        discardPile.cards.Clear();
        roundDeck = new RoundDeck(RunManager.instance.runDeck);
        DiscardHand();
        List<Card> newHand = roundDeck.DrawHand();
        foreach (Card card in newHand)
        {
            AddCardToHand(card);
        }
        currentDungeon = new Dungeon(dungeonName, roundNumber);
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
            Tile nextTile = monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, i, monster.tileOn.dungeonRow);
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
        Tile nextTile = monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow);
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

    public void UseSkill(Monster monster, SkillData skill, Tile tile)
    {
        Mana -= skill.ManaCost;
        tile.monster.Health -= DamageDealtToMonster(tile.monster, monster, skill);
        monster.actionsUsedThisTurn.Add(skill.name);
    }

    public void UseSkillOnBase(Monster monster, SkillData skill)
    {
        EnemyBase.GetComponent<EnemyBase>().Health -= skill.Damage;
        monster.actionsUsedThisTurn.Add(skill.name);

        if (EnemyBase.GetComponent<EnemyBase>().Health <= 0)
        {
            RunManager.instance.EndRound(cardsGainedThisRound);
        }
    }
    

    public void SelectSkill(Monster monster, SkillData skill)
    {
        if(skill.ManaCost <= Mana)
        {
            MainPhase mainPhase = (MainPhase)gameState;
            mainPhase.SetState(new SelectingSkillTargetState(mainPhase, monster, skill));
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
        int currentTileNumber = int.Parse(monster.tileOn.name.Substring(4));
        return GetValidTargetsForEnemy(monster, skill).Count > 0 || currentTileNumber - skill.Range < 1;
    }

    public void EnemyUseSkill(Monster monster, SkillData skill)
    {
        for (int i = 1; i <= skill.Range; i++)
        {
            int currentTileNumber = int.Parse(monster.tileOn.name.Substring(4));
            List<Tile> validTargets = GetValidTargetsForEnemy(monster, skill);
            if (currentTileNumber - i < 1)
            {
                PlayerBase.GetComponent<PlayerBase>().Health -= skill.Damage;
                monster.actionsUsedThisTurn.Add(skill.name);
                if (PlayerBase.GetComponent<PlayerBase>().Health <= 0)
                {
                    RunManager.instance.EndRoundLose();
                }
                break;
            }
            else if (validTargets.Count > 0)
            {
                Tile target = validTargets[UnityEngine.Random.Range(0, validTargets.Count)];
                target.monster.Health -= DamageDealtToMonster(target.monster, monster, skill);
                monster.actionsUsedThisTurn.Add(skill.name);
                break;
            }
        }
    }

    public List<Tile> GetValidTargets(Monster monster, SkillData skill)
    {
        List<Tile> validTargets = new List<Tile>();
        string[] directionArray = skill.directions.Split(' ');

        if (directionArray.Length == 0)
        {
            Debug.Log("No directions");
            return validTargets;
        }

        int tileIndex = int.Parse(monster.tileOn.name.Replace("Tile", ""));

        foreach (string direction in directionArray)
        {
            List<Tile> targetTiles = new List<Tile>();

            switch (direction)
            {
                case "Up":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetTile(tileIndex));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetTile(tileIndex));
                        }
                    }
                    break;

                case "Down":
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetTile(tileIndex));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetTile(tileIndex));
                        }
                    }
                    break;

                case "Backward":
                    for (int i = 1; i <= skill.Range; i++)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.GetPreviousTile(monster.tileOn, i, monster.tileOn.dungeonRow));
                    }
                    

                    break;

                case "Forward":
                    for (int i = 1; i <= skill.Range; i++)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, i, monster.tileOn.dungeonRow));
                    }
                    break;

                case "DiagonalBackward":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetPreviousTile(monster.tileOn, 1, monster.tileOn.dungeonRow.upRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetPreviousTile(monster.tileOn, 2, monster.tileOn.dungeonRow.upRow.upRow));
                        }
                    }
                    
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetPreviousTile(monster.tileOn, 1, monster.tileOn.dungeonRow.downRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetPreviousTile(monster.tileOn, 2, monster.tileOn.dungeonRow.downRow.downRow));
                        }
                    }
                    break;

                case "DiagonalForward":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow.upRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetNextTile(monster.tileOn, 2, monster.tileOn.dungeonRow.upRow.upRow));
                        }
                    }
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow.downRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetNextTile(monster.tileOn, 2, monster.tileOn.dungeonRow.downRow.downRow));
                        }
                    }
                    break;

                default:
                    Debug.Log("Invalid direction");
                    break;
            }

            foreach (Tile targetTile in targetTiles)
            {
                if (targetTile != null && targetTile.monster != null && targetTile.monster.team == "Enemy")
                {
                    validTargets.Add(targetTile);
                }
            }
        }

        return validTargets;
    }

    public List<Tile> GetValidTargetsForEnemy(Monster monster, SkillData skill)
    {
        
        List<Tile> validTargets = new List<Tile>();
        string[] directionArray = skill.directions.Split(' ');

        if (directionArray.Length == 0)
        {
            Debug.Log("No directions");
            return validTargets;
        }

        int tileIndex = int.Parse(monster.tileOn.name.Replace("Tile", ""));

        foreach (string direction in directionArray)
        {
            List<Tile> targetTiles = new List<Tile>();

            switch (direction)
            {
                case "Up":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetTile(tileIndex));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetTile(tileIndex));
                        }
                    }
                    break;

                case "Down":
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetTile(tileIndex));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetTile(tileIndex));
                        }
                    }
                    break;

                case "Forward":
                    for (int i = 1; i <= skill.Range; i++)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.GetPreviousTile(monster.tileOn, i, monster.tileOn.dungeonRow));
                    }
                    break;

                case "Backward":
                    for (int i = 1; i <= skill.Range; i++)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, i, monster.tileOn.dungeonRow));
                    }
                    break;

                case "DiagonalForward":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetPreviousTile(monster.tileOn, 1, monster.tileOn.dungeonRow.upRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetPreviousTile(monster.tileOn, 2, monster.tileOn.dungeonRow.upRow.upRow));
                        }
                    }
                    
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetPreviousTile(monster.tileOn, 1, monster.tileOn.dungeonRow.downRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetPreviousTile(monster.tileOn, 2, monster.tileOn.dungeonRow.downRow.downRow));
                        }
                    }
                    break;

                case "DiagonalBackward":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow.upRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetNextTile(monster.tileOn, 2, monster.tileOn.dungeonRow.upRow.upRow));
                        }
                    }
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow.downRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetNextTile(monster.tileOn, 2, monster.tileOn.dungeonRow.downRow.downRow));
                        }
                    }
                    break;

                default:
                    Debug.Log("Invalid direction");
                    break;
            }
            foreach (Tile targetTile in targetTiles)
            {
                if (targetTile != null && targetTile.monster != null && targetTile.monster.team == "Player")
                {
                    validTargets.Add(targetTile);
                }
            }
        }

        return validTargets;
    }

    public static Monster CheckForMonster(Tile currentTile, int distance)
    {
        Tile nextTile;
        if (distance < 0)
        {
            nextTile = currentTile.dungeonRow.GetPreviousTile(currentTile, -distance, currentTile.dungeonRow);
            if (nextTile != null && nextTile.monster != null)
            {
                return nextTile.monster;
            }
            return null;
        }
        nextTile = currentTile.dungeonRow.GetNextTile(currentTile, distance, currentTile.dungeonRow);
        if (nextTile != null && nextTile.monster != null)
        {
            return nextTile.monster;
        }
        return null;
    }
}
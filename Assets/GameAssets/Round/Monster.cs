using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Zenject;

public class Monster : MonoBehaviour
{

    public string name;

    private int _health;
    public int Health { 
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            GameObject healthBar = transform.Find("HealthBar/BarFill").gameObject;
            if (healthBar != null)
            {
                RectTransform barRectTransform = healthBar.GetComponent<RectTransform>();

                float healthPercent = Mathf.Clamp01((float)_health / MaxHealth);

                barRectTransform.localScale = new Vector3(healthPercent, 1, 1);
            }
            if (_health <= 0)
            {
                if (team == "Enemy")
                {
                    _playerStats.Experience += this.experienceGiven;
                }
                tileOn.monster = null;
                if (_uiManager.largeMonsterView1.monster == this || 
                    _uiManager.largeMonsterView2.monster == this || 
                    _uiManager.largeMonsterView3.monster == this)
                {
                   _uiManager.CloseMonsterInfoPanel();
                }
                
                Destroy(gameObject);
            }
        }
    }
    public int MaxHealth { get; set; }
    private int _attack;
    public int Attack { 
        get{
            int attack = 0;
            foreach (MonsterBuff buff in buffs)
            {
                if (buff.type == "Attack")
                {
                    attack += buff.amount;
                }
            }
            return _attack + attack;
        } 
        set
        {
            _attack = value;
        }
    }

    private int _defense;
    public int Defense { 
        get {
            int defense = 0;
            foreach (MonsterBuff buff in buffs)
            {
                if (buff.type == "Defense")
                {
                    defense += buff.amount;
                }
            }
            return _defense + defense;
        } 
        set {
            _defense = value;
        }
    }
    public int Movement { get; set; }

    public Tile tileOn;

    public List<string> actionsUsedThisTurn = new List<string>();

    public SkillData skill1;
    public SkillData skill2;

    public string team;

    public int ManaCost { get; set; }

    public List<MonsterBuff> buffs = new List<MonsterBuff>();

    public string evolvesFrom;
    public string evolvesTo;

    private int experienceGiven;
    private int experienceRequired;

    public class Factory : PlaceholderFactory<Monster> { }

    private IGameManager _gameManager;
    private RoundUIManager _uiManager;
    private PlayerStats _playerStats;
    private SpriteManager _spriteManager;
    private CardFactory _cardFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(IGameManager gameManager,  
                          RoundUIManager uiManager,
                          PlayerStats playerStats,
                          SpriteManager spriteManager,
                          CardFactory cardFactory,
                          DiContainer container)
    {
        _gameManager = gameManager;
        _uiManager = uiManager;
        _playerStats = playerStats;
        _spriteManager = spriteManager;
        _cardFactory = cardFactory;
        _container = container;
    }

    void Start()
    {
        MainPhase.ExitMainPhase.AddListener(() => actionsUsedThisTurn.Clear());
        transform.Find("EvolveButton").GetComponent<Button>().onClick.AddListener(Evolve);

        MonsterInfoButton infoButton = GetComponentInChildren<MonsterInfoButton>();
        if (infoButton != null)
        {
            _container.Inject(infoButton);
        }
    }

    void Update()
    {
        if (team == "Ally" && !string.IsNullOrEmpty(evolvesTo) && _playerStats.Experience >= experienceRequired)
        {
            transform.Find("EvolveButton").gameObject.SetActive(true);
        } else {
            transform.Find("EvolveButton").gameObject.SetActive(false);
        }
    }

    private void Evolve()
    {
        MonsterCard monsterCard = _cardFactory.CreateCard(evolvesTo, _gameManager.CardLevels[evolvesTo]) as MonsterCard;
        _playerStats.Experience -= experienceRequired;
        this.Initialize(monsterCard, tileOn, team);
    }

    public void Initialize(MonsterCard monsterCard, Tile tile, string team)
    {
        this.team = team;
        if (team == "Enemy")
        {
            transform.Find("TeamBackground").GetComponent<Image>().sprite = _spriteManager.GetSprite("EnemyBackground");
        } else {
            transform.Find("TeamBackground").GetComponent<Image>().sprite = _spriteManager.GetSprite("PlayerBackground");
        }
        this.tileOn = tile;
        this.tileOn.monster = this;
        this.name = monsterCard.Name;
        transform.Find("Image").GetComponent<Image>().sprite = _spriteManager.GetSprite(this.name);
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = this.name;
        this.MaxHealth = monsterCard.Health;
        this.Health = monsterCard.Health;
        this.Attack = monsterCard.Attack;
        this.Defense = monsterCard.Defense;
        this.Movement = monsterCard.Movement;
        this.skill1 = monsterCard.skill1;
        this.skill2 = monsterCard.skill2;
        this.ManaCost = monsterCard.ManaCost;
        this.evolvesFrom = monsterCard.evolvesFrom;
        this.evolvesTo = monsterCard.evolvesTo;
        this.experienceGiven = monsterCard.experienceGiven;
        this.experienceRequired = monsterCard.experienceRequired;
    }

    public bool IsOnInfoButton(Vector2 mousePosition)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public void MoveTile(Tile tile)
    {
        tileOn.monster = null;
        tileOn = tile;
        tile.monster = this;
        transform.SetParent(tile.transform);
        transform.position = tile.transform.position;
        //.Add("Movement");
    }

}

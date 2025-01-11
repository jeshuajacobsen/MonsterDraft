using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
                tileOn.monster = null;
                Destroy(gameObject);
            }
        }
    }
    public int MaxHealth { get; set; }
    public int Attack { get; set; }

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

    void Start()
    {
        MainPhase.ExitMainPhase.AddListener(() => actionsUsedThisTurn.Clear());
    }

    void Update()
    {
        
    }

    public void InitValues(MonsterCard monsterCard, Tile tile, string team)
    {
        this.team = team;
        if (team == "Enemy")
        {
            transform.Find("TeamBackground").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite("EnemyBackground");
        } else {
            transform.Find("TeamBackground").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite("PlayerBackground");
        }
        this.tileOn = tile;
        this.tileOn.monster = this;
        this.name = monsterCard.Name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite(this.name);
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = this.name;
        this.MaxHealth = monsterCard.Health;
        this.Health = monsterCard.Health;
        this.Attack = monsterCard.Attack;
        this.Defense = monsterCard.Defense;
        this.Movement = monsterCard.Movement;
        this.skill1 = monsterCard.skill1;
        this.skill2 = monsterCard.skill2;
        this.ManaCost = monsterCard.ManaCost;
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

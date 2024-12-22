using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Monster : MonoBehaviour
{

    public string name;
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Movement { get; set; }

    public Tile tileOn;

    public List<string> actionsUsedThisTurn = new List<string>();

    public SkillData skill1;
    public SkillData skill2;

    void Start()
    {
        MainPhase.ExitMainPhase.AddListener(() => actionsUsedThisTurn.Clear());
    }

    void Update()
    {
        
    }

    public void InitValues(MonsterCard monsterCard, Tile tile)
    {
        this.tileOn = tile;
        this.name = monsterCard.Name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetCardSprite(this.name);
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = this.name;
        this.Health = monsterCard.Health;
        this.Attack = monsterCard.Attack;
        this.Defense = monsterCard.Defense;
        this.Movement = monsterCard.Movement;
        this.skill1 = monsterCard.skill1;
        this.skill2 = monsterCard.skill2;
    }

    public void MoveTile(Tile tile)
    {
        tileOn.monster = null;
        tileOn = tile;
        tile.monster = this;
        transform.SetParent(tile.transform);
        transform.position = tile.transform.position;
        actionsUsedThisTurn.Add("Movement");
    }

    public void AttackMonster(Monster target)
    {
        int damage = Attack - target.Defense;
        if (damage < 0)
        {
            damage = 0;
        }
        target.Health -= damage;
        actionsUsedThisTurn.Add(skill1.name);
    }
}

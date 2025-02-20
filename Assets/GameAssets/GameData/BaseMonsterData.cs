using System.Collections.Generic;
using UnityEngine;

public class BaseMonsterData
{
    public string name;
    public int Attack { get; set; }
    public int Health { get; set; }
    public int Defense { get; set; }
    public int Movement { get; set; }
    public int ManaCost { get; set; }
    public string Description { get; set; }
    public int CoinCost { get; set; }

    public string rarity = "Common";

    public string skill1Name;
    public string skill2Name;

    public string evolvesFrom = "";
    public string evolvesTo = "";

    public int experienceGiven = 100;
    public int experienceRequired = 400;

    public int PrestigeCost { get; set; }

    public int maxLevel;
    public List<MonsterCardLevelData> levelData = new List<MonsterCardLevelData>();
    public int baseLevelUpCost;

    public BaseMonsterData(string name)
    {
        PrestigeCost = 0;
        this.name = name;
        Description = "This is the description for " + name;

        switch (name)
        {
            case "Borble":
                SetBaseStats(100, 260, 80, 1, 80, 120, "Bubble", "Wave", "Epic", "Pupal", null, 400, 1000);
                break;
            case "Pupal":
                SetBaseStats(160, 380, 120, 1, 100, 140, "Wave", "Water Jet", "Epic", "Aquafly", "Borble", 600, 0);
                break;
            case "Aquafly":
                SetBaseStats(100, 510, 100, 2, 120, 180, "Water Jet", "Aqua Blast", "Epic", null, "Pupal", 900, 0);
                break;
            case "Leafree":
                SetBaseStats(60, 220, 70, 2, 60, 80, "Leaf", "Growth", "Rare", "Leafear", null, 300, 500);
                break;
            case "Leafear":
                SetBaseStats(120, 350, 130, 2, 80, 120, "Growth", "Solar Beam", "Rare", null, "Leafree", 500, 0);
                break;
            case "Olla":
                SetBaseStats(150, 350, 140, 1, 70, 110, "Wrap", "Poison Sting", "Rare", null, null, 500, 600);
                break;
            case "Owisp":
                SetBaseStats(50, 150, 40, 1, 40, 60, "Spark", "Burn", "Uncommon", "Wallowisp", null, 200, 300);
                break;
            case "Wallowisp":
                SetBaseStats(90, 220, 70, 1, 40, 80, "Burn", "Heat Wave", "Uncommon", null, "Owisp", 300, 0);
                break;
            case "Slimy":
                SetBaseStats(20, 120, 10, 1, 20, 30, "Goo", "Slime Ball", "Common", "Slimier", null, 100, 100);
                break;
            case "Slimier":
                SetBaseStats(30, 180, 20, 1, 40, 50, "Slime Ball", "Multiply", "Common", "Slimiest", "Slimy", 200, 0);
                break;
            case "Slimiest":
                SetBaseStats(60, 350, 40, 1, 60, 70, "Multiply", "Slime Storm", "Common", null, "Slimier", 300, 0);
                break;
            case "Snowbug":
                SetBaseStats(80, 200, 50, 1, 50, 60, "Chill", "Ice Shard", "Uncommon", "Snant", null, 200, 300);
                break;
            case "Snant":
                SetBaseStats(120, 300, 80, 1, 60, 70, "Ice Shard", "Freeze", "Uncommon", "Snowpede", "Snowbug", 300, 0);
                break;
            case "Snowpede":
                SetBaseStats(170, 450, 120, 1, 80, 100, "Freeze", "Blizzard", "Uncommon", null, "Snant", 500, 0);
                break;
            case "Squrl":
                SetBaseStats(110, 350, 80, 1, 80, 120, "Bite", "Drain", "Epic", "Squrile", null, 400, 1000);
                break;
            case "Squrile":
                SetBaseStats(150, 450, 120, 1, 100, 150, "Drain", "Nightmare", "Epic", null, "Squrl", 700, 0);
                break;
            case "Zaple":
                SetBaseStats(30, 100, 10, 1, 20, 40, "Zap", "Shock", "Common", "Lightna", null, 100, 100);
                break;
            case "Lightna":
                SetBaseStats(50, 150, 20, 2, 30, 60, "Shock", "Lightning", "Common", "Thunda", "Zaple", 200, 0);
                break;
            case "Thunda":
                SetBaseStats(80, 230, 30, 2, 50, 100, "Lightning", "Thunder Bolt", "Common", null, "Lightna", 400, 0);
                break;
            default:
                Debug.Log("Invalid name: " + name);
                SetBaseStats(0, 0, 0, 0, 0, 0, "Zap", "Spark", "Common", null, null, 0, 0);
                break;
        }

        maxLevel = levelData.Count + 1;
    }

    private void SetBaseStats(int attack, int health, int defense, int movement, int manaCost, int coinCost,
                              string skill1, string skill2, string rarity, string evolvesTo, string evolvesFrom,
                              int experience, int prestigeCost)
    {
        Attack = attack;
        Health = health;
        Defense = defense;
        Movement = movement;
        ManaCost = manaCost;
        CoinCost = coinCost;
        skill1Name = skill1;
        skill2Name = skill2;
        this.rarity = rarity;
        this.evolvesTo = evolvesTo;
        this.evolvesFrom = evolvesFrom;
        experienceGiven = experience;
        PrestigeCost = prestigeCost;

        GenerateLevelData();
    }

    private void GenerateLevelData()
    {
        levelData.Clear();
        float attackMultiplier = 0.1f;
        float healthMultiplier = 0.15f;
        float defenseMultiplier = 0.12f;
        float costMultiplier = 0.2f;
        float manaMultiplier = 0.1f;

        int currentAttack = Attack;
        int currentHealth = Health;
        int currentDefense = Defense;
        int currentManaCost = ManaCost;
        int currentCoinCost = CoinCost;

        for (int i = 0; i < 9; i++)
        {
            int atkIncrease = Mathf.RoundToInt(currentAttack * attackMultiplier);
            int hpIncrease = Mathf.RoundToInt(currentHealth * healthMultiplier);
            int defIncrease = Mathf.RoundToInt(currentDefense * defenseMultiplier);
            int manaIncrease = Mathf.RoundToInt(currentManaCost * manaMultiplier);
            int coinIncrease = Mathf.RoundToInt(currentCoinCost * costMultiplier);

            levelData.Add(new MonsterCardLevelData(atkIncrease, hpIncrease, defIncrease, 0, manaIncrease, coinIncrease));

            currentAttack += atkIncrease;
            currentHealth += hpIncrease;
            currentDefense += defIncrease;
            currentManaCost += manaIncrease;
            currentCoinCost += coinIncrease;
        }

        switch (rarity)
        {
            case "Common":
                baseLevelUpCost = 20;
                break;
            case "Uncommon":
                baseLevelUpCost = 50;
                break;
            case "Rare":
                baseLevelUpCost = 120;
                break;
            case "Epic":
                baseLevelUpCost = 300;
                break;
            default:
                baseLevelUpCost = 20;
                break;
        }
    }
}

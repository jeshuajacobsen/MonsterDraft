using System.Collections.Generic;

public class Card
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    private int _cost;
    public int CoinCost {
        get
        {
            int addedCost = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedCost += GameManager.instance.gameData.GetBaseMonsterData(Name).levelData[i].coinCost;
            }
            return _cost + addedCost;
        }
        set
        {
            _cost = value;
        }
    }
    public int PrestigeCost { get; set; }
    public List<string> OnGainEffects { get; set; }
    public List<string> effects;
    public int maxLevel = 1;
    public int level = 1;
    private int baseLevelUpCost;
    public int LevelUpCost {
        get
        {
            return baseLevelUpCost * level;
        }
    }

    public Card(string name, string type, int level)
    {
        OnGainEffects = new List<string>();
        effects = new List<string>();
        Name = name;
        Type = type;
        this.level = level;
        if (type == "Monster")
        {
            BaseMonsterData baseStats = GameManager.instance.gameData.GetBaseMonsterData(name);
            Description = baseStats.Description;
            CoinCost = baseStats.CoinCost;
            PrestigeCost = baseStats.PrestigeCost;
            baseLevelUpCost = baseStats.baseLevelUpCost;
            maxLevel = baseStats.maxLevel;
        }
        else if (type == "Treasure")
        {
            TreasureData baseStats = GameManager.instance.gameData.GetTreasureData(name);
            Description = baseStats.Description;
            CoinCost = baseStats.CoinCost;
            PrestigeCost = baseStats.PrestigeCost;
            OnGainEffects = baseStats.onGainEffects;
            effects = baseStats.effects;
            baseLevelUpCost = baseStats.baseLevelUpCost;
            maxLevel = baseStats.maxLevel;
        }
        else if (type == "Action")
        {
            BaseActionData baseStats = GameManager.instance.gameData.GetActionData(name);
            Description = baseStats.Description;
            CoinCost = baseStats.CoinCost;
            PrestigeCost = baseStats.PrestigeCost;
            OnGainEffects = baseStats.onGainEffects;
            effects = baseStats.effects;
            baseLevelUpCost = baseStats.baseLevelUpCost;
            maxLevel = baseStats.maxLevel;
        }

    }
}
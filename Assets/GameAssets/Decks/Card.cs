using System.Collections.Generic;

public class Card
{
    public string Name { get; set; }
    public string Type { get; set; }
    private int _cost;
    public int CoinCost {
        get
        {
            int addedCost = 0;
            for(int i = 0; i < level - 1; i++)
            {
                if (Type == "Monster")
                    addedCost += GameManager.instance.gameData.GetBaseMonsterData(Name).levelData[i].coinCost;
                else if (Type == "Treasure")
                    addedCost += GameManager.instance.gameData.GetTreasureData(Name).levelData[i].coinCost;
                else if (Type == "Action")
                    addedCost += GameManager.instance.gameData.GetActionData(Name).levelData[i].coinCost;
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
    private List<string> _effects;
    public List<string> Effects {
        get
        {
            Dictionary<int, string> changedEffects = new Dictionary<int, string>();
            for(int i = 0; i < level - 1; i++)
            {
                if (Type == "Treasure")
                    changedEffects = GameManager.instance.gameData.GetTreasureData(Name).levelData[i].effectChanges;
                else if (Type == "Action")
                    changedEffects = GameManager.instance.gameData.GetActionData(Name).levelData[i].effectChanges;
                
                foreach (KeyValuePair<int, string> effectChange in changedEffects)
                {
                    if (_effects.Count > effectChange.Key)
                    {
                        _effects[effectChange.Key] = effectChange.Value;
                    }
                    else
                    {
                        _effects.Add(effectChange.Value);
                    }
                }
            }
            return _effects;
        }
        set
        {
            _effects = value;
        }
    }

    private Dictionary<string, string> _effectVariables = new Dictionary<string, string>();
    public Dictionary<string, string> EffectVariables {
        get
        {
            Dictionary<string, string> variableChanges = new Dictionary<string, string>();
            for(int i = 0; i < level - 1; i++)
            {
                if (Type == "Treasure")
                    variableChanges = GameManager.instance.gameData.GetTreasureData(Name).levelData[i].effectVariableChanges;
                else if (Type == "Action")
                    variableChanges = GameManager.instance.gameData.GetActionData(Name).levelData[i].effectVariableChanges;
                
                foreach (KeyValuePair<string, string> entry in variableChanges)
                {
                    if (_effectVariables.ContainsKey(entry.Key))
                    {
                        _effectVariables[entry.Key] = entry.Value;
                    }
                    else
                    {
                        _effectVariables.Add(entry.Key, entry.Value);
                    }
                }
            }
            return _effectVariables;
        }
        set
        {
            _effectVariables = value;
        }
    }
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
        Effects = new List<string>();
        Name = name;
        Type = type;
        this.level = level;
        if (type == "Monster")
        {
            BaseMonsterData baseStats = GameManager.instance.gameData.GetBaseMonsterData(name);
            CoinCost = baseStats.CoinCost;
            PrestigeCost = baseStats.PrestigeCost;
            baseLevelUpCost = baseStats.baseLevelUpCost;
            maxLevel = baseStats.maxLevel;
        }
        else if (type == "Treasure")
        {
            TreasureData baseStats = GameManager.instance.gameData.GetTreasureData(name);
            CoinCost = baseStats.CoinCost;
            PrestigeCost = baseStats.PrestigeCost;
            OnGainEffects = baseStats.onGainEffects;
            Effects = baseStats.effects;
            baseLevelUpCost = baseStats.baseLevelUpCost;
            maxLevel = baseStats.maxLevel;
            EffectVariables = baseStats.effectVariables;
        }
        else if (type == "Action")
        {
            BaseActionData baseStats = GameManager.instance.gameData.GetActionData(name);
            CoinCost = baseStats.CoinCost;
            PrestigeCost = baseStats.PrestigeCost;
            OnGainEffects = baseStats.onGainEffects;
            Effects = baseStats.effects;
            baseLevelUpCost = baseStats.baseLevelUpCost;
            maxLevel = baseStats.maxLevel;
            EffectVariables = baseStats.effectVariables;
        }

    }
}
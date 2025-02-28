using System.Collections.Generic;
using Zenject;

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
                    addedCost += _gameManager.gameData.GetBaseMonsterData(Name).levelData[i].coinCostChange;
                else if (Type == "Treasure")
                    addedCost += _gameManager.gameData.GetTreasureData(Name).levelData[i].coinCostChange;
                else if (Type == "Action")
                    addedCost += _gameManager.gameData.GetActionData(Name).levelData[i].coinCostChange;
            }
            return _cost + addedCost;
        }
        set
        {
            _cost = value;
        }
    }
    private int _baseLevelUpPrestigeCost;
    public int LevelUpPrestigeCost { 
        get
        {
            return _baseLevelUpPrestigeCost * level;
        }   
        set
        {
            _baseLevelUpPrestigeCost = value;
        } 
    }
    public int BuyCardPrestigeCost { get; set; }
    private List<string> _onGainEffects;
    public List<string> OnGainEffects  {
        get 
        {
            List<string> modifiedEffects = new List<string>();
            
            foreach (var effect in _onGainEffects)
            {
                string currentEffect = effect;
                if (this.EffectVariables != null)
                {
                    foreach (var effectVariable in this.EffectVariables)
                    {
                        currentEffect = currentEffect.Replace("{" + effectVariable.Key + "}", effectVariable.Value);
                    }
                }
                modifiedEffects.Add(currentEffect);
            }
            return modifiedEffects;
        }
        set
        {
            _onGainEffects = value;
        }
    }
    private List<string> _effects;
    public List<string> Effects {
        get
        {
            Dictionary<int, string> changedEffects = new Dictionary<int, string>();
            for(int i = 0; i < level - 1; i++)
            {
                if (Type == "Treasure")
                    changedEffects = _gameManager.gameData.GetTreasureData(Name).levelData[i].effectChanges;
                else if (Type == "Action")
                    changedEffects = _gameManager.gameData.GetActionData(Name).levelData[i].effectChanges;
                
                if (changedEffects != null)
                {
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
            }
            List<string> modifiedEffects = new List<string>();
            
            foreach (var effect in _effects)
            {
                string currentEffect = effect;
                if (this.EffectVariables != null)
                {
                    foreach (var effectVariable in this.EffectVariables)
                    {
                        currentEffect = currentEffect.Replace("{" + effectVariable.Key + "}", effectVariable.Value);
                    }
                }
                modifiedEffects.Add(currentEffect);
            }
            return modifiedEffects;
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
                    variableChanges = _gameManager.gameData.GetTreasureData(Name).levelData[i].effectVariableChanges;
                else if (Type == "Action")
                    variableChanges = _gameManager.gameData.GetActionData(Name).levelData[i].effectVariableChanges;
                
                if (variableChanges != null)
                {
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

    protected GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Initialize(string name, string type, int level)
    {
        OnGainEffects = new List<string>();
        Effects = new List<string>();
        EffectVariables = new Dictionary<string, string>();

        Name = name;
        Type = type;
        this.level = level;

        if (type == "Monster")
        {
            BaseMonsterData baseStats = _gameManager.gameData.GetBaseMonsterData(name);
            CoinCost = baseStats.CoinCost;
            LevelUpPrestigeCost = baseStats.LevelUpPrestigeCost;
            maxLevel = baseStats.maxLevel;
            BuyCardPrestigeCost = baseStats.BuyCardPrestigeCost;
        }
        else if (type == "Treasure")
        {
            TreasureData baseStats = _gameManager.gameData.GetTreasureData(name);
            CoinCost = baseStats.CoinCost;
            LevelUpPrestigeCost = baseStats.LevelUpPrestigeCost;
            OnGainEffects = new List<string>(baseStats.onGainEffects);
            Effects = new List<string>(baseStats.effects);
            maxLevel = baseStats.maxLevel;
            EffectVariables = new Dictionary<string, string>(baseStats.effectVariables);
            BuyCardPrestigeCost = baseStats.BuyCardPrestigeCost;
        }
        else if (type == "Action")
        {
            BaseActionData baseStats = _gameManager.gameData.GetActionData(name);
            CoinCost = baseStats.CoinCost;
            LevelUpPrestigeCost = baseStats.LevelUpPrestigeCost;
            OnGainEffects = new List<string>(baseStats.onGainEffects);
            Effects = new List<string>(baseStats.effects);
            maxLevel = baseStats.maxLevel;
            EffectVariables = new Dictionary<string, string>(baseStats.effectVariables);
            BuyCardPrestigeCost = baseStats.BuyCardPrestigeCost;
        }
    }
}
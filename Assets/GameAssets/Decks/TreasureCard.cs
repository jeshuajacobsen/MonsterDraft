using System.Collections.Generic;
using UnityEngine;

public class TreasureCard : Card
{
    private int _coinGeneration;
    public int CoinGeneration { 
        get
        {
            int addedCoinGeneration = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedCoinGeneration += GameManager.instance.gameData.GetTreasureData(Name).levelData[i].coinGeneration;
            }
            return _coinGeneration + addedCoinGeneration;
        }
        set
        {
            _coinGeneration = value;
            
        }
    }
    private int _manaGeneration;
    public int ManaGeneration {
        get
        {
            int addedManaGeneration = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedManaGeneration += GameManager.instance.gameData.GetTreasureData(Name).levelData[i].manaGeneration;
            }
            return _manaGeneration + addedManaGeneration;
        }
        set
        {
            _manaGeneration = value;
        }
    }
    private string _description;
    public string Description {
        get
        {
            string description = _description;
            List<TreasureCardLevelData> levelData = GameManager.instance.gameData.GetTreasureData(Name).levelData;
            for (int i = 0; i < level - 1; i++)
            {
                if (!string.IsNullOrEmpty(levelData[i].description))
                {
                    description = levelData[i].description;
                }
            }
            description = description.Replace("{CoinGeneration}", CoinGeneration.ToString());
            description = description.Replace("{ManaGeneration}", ManaGeneration.ToString());
            foreach (var effectVariable in this.EffectVariables)
            {
                description = description.Replace("{" + effectVariable.Key + "}", effectVariable.Value.ToString());
            }
            return description;
        }
    }

    public string GetColoredDescription(Color coinColor, Color manaColor)
    {
        string coinReplacement = "<color=#" + ColorUtility.ToHtmlStringRGB(coinColor) + ">" + CoinGeneration + "</color>";
        string manaReplacement = "<color=#" + ColorUtility.ToHtmlStringRGB(manaColor) + ">" + ManaGeneration + "</color>";
        string description = _description;
        List<TreasureCardLevelData> levelData = GameManager.instance.gameData.GetTreasureData(Name).levelData;
        for (int i = 0; i < level - 1; i++)
        {
            if (!string.IsNullOrEmpty(levelData[i].description))
            {
                description = levelData[i].description;
            }
        }
        description = description.Replace("{CoinGeneration}", coinReplacement).Replace("{ManaGeneration}", manaReplacement);

        Dictionary<string, string> variableChanges = new Dictionary<string, string>();
        if (level > 1)
        {
            if (Type == "Treasure")
                variableChanges = GameManager.instance.gameData.GetTreasureData(Name).levelData[level - 2].effectVariableChanges;
            else if (Type == "Action")
                variableChanges = GameManager.instance.gameData.GetActionData(Name).levelData[level - 2].effectVariableChanges;
        }
        foreach (var effectVariable in this.EffectVariables)
        {
            if (variableChanges.ContainsKey(effectVariable.Key))
            {
                description = description.Replace("{" + effectVariable.Key + "}",
                    "<color=#" + ColorUtility.ToHtmlStringRGB(Color.green) + ">" + effectVariable.Value.ToString() + "</color>");
            }
            else
            {
                description = description.Replace("{" + effectVariable.Key + "}", effectVariable.Value.ToString());
            }
        }
        return description;
    }

    public TreasureCard(string name, int level) : base(name, "Treasure", level)
    {
        TreasureData baseStats = GameManager.instance.gameData.GetTreasureData(name);
        CoinGeneration = baseStats.CoinGeneration;
        ManaGeneration = baseStats.ManaGeneration;
        _description = baseStats.Description;
    }

}
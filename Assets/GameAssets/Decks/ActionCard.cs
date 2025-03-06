using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActionCard : Card
{
    private string _description;
    public string Description {
        get
        {
            string description = _description;
            List<ActionCardLevelData> levelData = _gameManager.GameData.GetActionData(Name).levelData;
            for (int i = 0; i < level - 1; i++)
            {
                if (!string.IsNullOrEmpty(levelData[i].description))
                {
                    description = levelData[i].description;
                }
            }
            
            if (this.EffectVariables != null)
            {
                foreach (var effectVariable in this.EffectVariables)
                {
                    description = description.Replace("{" + effectVariable.Key + "}", effectVariable.Value.ToString());
                }
            }
            return description;
        }
        set
        {
            _description = value;
        }
    }

    public string GetColoredDescription()
    {
        string description = _description;
        List<ActionCardLevelData> levelData = _gameManager.GameData.GetActionData(Name).levelData;
        for (int i = 0; i < level - 1; i++)
        {
            if (!string.IsNullOrEmpty(levelData[i].description))
            {
                description = levelData[i].description;
            }
        }

        Dictionary<string, string> variableChanges = new Dictionary<string, string>();
        if (level > 1)
        {
            variableChanges = _gameManager.GameData.GetActionData(Name).levelData[level - 2].effectVariableChanges;
        }
        if (this.EffectVariables != null)
        {
            foreach (var effectVariable in this.EffectVariables)
            {
                if (variableChanges != null && variableChanges.ContainsKey(effectVariable.Key))
                {
                    description = description.Replace("{" + effectVariable.Key + "}",
                        "<color=#" + ColorUtility.ToHtmlStringRGB(Color.green) + ">" + effectVariable.Value.ToString() + "</color>");
                }
                else
                {
                    description = description.Replace("{" + effectVariable.Key + "}", effectVariable.Value.ToString());
                }
            }
        }
        return description;
    }

    public bool StartsWithTarget()
    {
        return Effects[0].StartsWith("Target");
    }
}
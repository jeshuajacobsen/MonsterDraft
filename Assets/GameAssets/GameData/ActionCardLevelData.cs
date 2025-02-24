using System.Collections.Generic;


public class ActionCardLevelData
{
    public Dictionary<int, string> effectChanges = new Dictionary<int, string>();
    public Dictionary<string, string> effectVariableChanges = new Dictionary<string, string>();
    public int coinCostChange;
    public string description;

    public ActionCardLevelData(int coinCost, 
        Dictionary<int, string> effectChanges, 
        Dictionary<string, string> effectVariableChanges, 
        string description)
    {
        this.effectChanges = effectChanges;
        this.effectVariableChanges = effectVariableChanges;
        this.coinCostChange = coinCost;
        this.description = description;
    }
}
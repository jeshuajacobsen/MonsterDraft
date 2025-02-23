using System.Collections.Generic;


public class ActionCardLevelData
{
    public Dictionary<int, string> effectChanges = new Dictionary<int, string>();
    public Dictionary<string, string> effectVariableChanges = new Dictionary<string, string>();
    public int coinCost;

    public ActionCardLevelData(Dictionary<int, string> effectChanges, Dictionary<string, string> effectVariableChanges, int coinCost)
    {
        this.effectChanges = effectChanges;
        this.effectVariableChanges = effectVariableChanges;
        this.coinCost = coinCost;
    }
}
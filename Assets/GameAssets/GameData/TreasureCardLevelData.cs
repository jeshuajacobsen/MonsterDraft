
using System.Collections.Generic;

public class TreasureCardLevelData
{
    public int coinGeneration;
    public int manaGeneration;
    public int coinCost;
    public Dictionary<int, string> effectChanges = new Dictionary<int, string>();
    public Dictionary<string, string> effectVariableChanges = new Dictionary<string, string>();
    public string description;

    public TreasureCardLevelData(int coinGeneration, 
                                 int manaGeneration, 
                                 int coinCost, 
                                 Dictionary<int, string> effectChanges, 
                                 Dictionary<string, string> effectVariableChanges, 
                                 string description)
    {
        this.coinGeneration = coinGeneration;
        this.manaGeneration = manaGeneration;
        this.coinCost = coinCost;
        this.effectChanges = effectChanges;
        this.effectVariableChanges = effectVariableChanges;
        this.description = description;
    }

    public TreasureCardLevelData(int coinGeneration, int manaGeneration, int coinCost)
    {
        this.coinGeneration = coinGeneration;
        this.manaGeneration = manaGeneration;
        this.coinCost = coinCost;
        this.effectVariableChanges = new Dictionary<string, string>();
    }
}
using System.Collections.Generic;

public class Card
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int PrestigeCost { get; set; }
    public List<string> OnGainEffects { get; set; }

    public Card(string name, string type)
    {
        OnGainEffects = new List<string>();
        Name = name;
        Type = type;
        if (type == "Monster")
        {
            BaseMonsterData baseStats = GameManager.instance.gameData.GetBaseMonsterData(name);
            Description = baseStats.Description;
            Cost = baseStats.Cost;
            PrestigeCost = baseStats.PrestigeCost;
        }
        else if (type == "Treasure")
        {
            TreasureData baseStats = GameManager.instance.gameData.GetTreasureData(name);
            Description = baseStats.Description;
            Cost = baseStats.Cost;
            PrestigeCost = baseStats.PrestigeCost;
        }
        else if (type == "Action")
        {
            BaseActionData baseStats = GameManager.instance.gameData.GetActionData(name);
            Description = baseStats.Description;
            Cost = baseStats.Cost;
            PrestigeCost = baseStats.PrestigeCost;
            OnGainEffects = baseStats.OnGainEffects;
        }

    }
}
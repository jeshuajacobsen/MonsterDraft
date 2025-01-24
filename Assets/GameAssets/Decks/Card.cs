public class Card
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int PrestigeCost { get; set; }

    public Card(string name, string type)
    {
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
        }

    }
}
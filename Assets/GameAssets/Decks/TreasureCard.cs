public class TreasureCard : Card
{
    public int GoldGeneration { get; set; }
    public int ManaGeneration { get; set; }

    public TreasureCard(string name) : base(name, "Treasure")
    {
        TreasureData baseStats = GameManager.instance.gameData.GetTreasureData(name);
        GoldGeneration = baseStats.GoldGeneration;
        ManaGeneration = baseStats.ManaGeneration;
    }

}
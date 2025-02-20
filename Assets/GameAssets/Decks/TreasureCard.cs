public class TreasureCard : Card
{
    public int CoinGeneration { get; set; }
    public int ManaGeneration { get; set; }

    public TreasureCard(string name, int level) : base(name, "Treasure", level)
    {
        TreasureData baseStats = GameManager.instance.gameData.GetTreasureData(name);
        CoinGeneration = baseStats.CoinGeneration;
        ManaGeneration = baseStats.ManaGeneration;
    }

}
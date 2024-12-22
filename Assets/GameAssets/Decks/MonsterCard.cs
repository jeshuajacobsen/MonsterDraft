public class MonsterCard : Card
{
    public int Attack { get; set; }
    public int Health { get; set; }
    public int Defense { get; set; }
    public int Movement { get; set; }
    public int ManaCost { get; set; }

    public MonsterCard(string name) : base(name, "Monster")
    {
        BaseStatsData baseStats = GameManager.instance.gameData.GetBaseStatsData(name);
        Attack = baseStats.Attack;
        Health = baseStats.Health;
        Defense = baseStats.Defense;
        Movement = baseStats.Movement;
        ManaCost = baseStats.ManaCost;
    }

}
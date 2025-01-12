public class MonsterCard : Card
{
    public int Attack { get; set; }
    public int Health { get; set; }
    public int Defense { get; set; }
    public int Movement { get; set; }
    public int ManaCost { get; set; }
    public SkillData skill1;
    public SkillData skill2;

    public string evolvesFrom;
    public string evolvesTo;
    public int experienceGiven;
    public int experienceRequired;

    public MonsterCard(string name) : base(name, "Monster")
    {
        BaseMonsterData baseStats = GameManager.instance.gameData.GetBaseMonsterData(name);
        Attack = baseStats.Attack;
        Health = baseStats.Health;
        Defense = baseStats.Defense;
        Movement = baseStats.Movement;
        ManaCost = baseStats.ManaCost;
        skill1 = GameManager.instance.gameData.GetSkill(baseStats.skill1Name);
        skill2 = GameManager.instance.gameData.GetSkill(baseStats.skill2Name);
        evolvesFrom = baseStats.evolvesFrom;
        evolvesTo = baseStats.evolvesTo;
        experienceGiven = baseStats.experienceGiven;
        experienceRequired = baseStats.experienceRequired;
    }

}
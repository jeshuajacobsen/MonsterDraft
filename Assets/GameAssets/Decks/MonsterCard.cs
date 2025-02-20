public class MonsterCard : Card
{
    private int _attack;
    public int Attack {
        get
        {
            int addedAttack = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedAttack += GameManager.instance.gameData.GetBaseMonsterData(Name).levelData[i].attack;
            }
            return _attack + addedAttack;
        }
        set
        {
            _attack = value;
            
        }
    }

    private int _health;
    public int Health {
        get
        {
            int addedHealth = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedHealth += GameManager.instance.gameData.GetBaseMonsterData(Name).levelData[i].health;
            }
            return _health + addedHealth;
        }
        set
        {
            _health = value;
        }
    }

    private int _defense;
    public int Defense {
        get
        {
            int addedDefense = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedDefense += GameManager.instance.gameData.GetBaseMonsterData(Name).levelData[i].defense;
            }
            return _defense + addedDefense;
        }
        set
        {
            _defense = value;
        }
    }

    private int _movement;
    public int Movement {
        get
        {
            int addedMovement = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedMovement += GameManager.instance.gameData.GetBaseMonsterData(Name).levelData[i].movement;
            }
            return _movement + addedMovement;
        }
        set
        {
            _movement = value;
        }
    }

    private int _manaCost;
    public int ManaCost {
        get
        {
            int addedManaCost = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedManaCost += GameManager.instance.gameData.GetBaseMonsterData(Name).levelData[i].manaCost;
            }
            return _manaCost + addedManaCost;
        }
        set
        {
            _manaCost = value;
        }
    }
    
    public SkillData skill1;
    public SkillData skill2;

    public string evolvesFrom;
    public string evolvesTo;
    public int experienceGiven;
    public int experienceRequired;

    public MonsterCard(string name, int level) : base(name, "Monster", level)
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
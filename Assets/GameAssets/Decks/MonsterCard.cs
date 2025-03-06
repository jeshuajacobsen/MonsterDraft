using Zenject;

public class MonsterCard : Card
{
    private int _attack;
    public int Attack {
        get
        {
            int addedAttack = 0;
            for(int i = 0; i < level - 1; i++)
            {
                addedAttack += _gameManager.GameData.GetBaseMonsterData(Name).levelData[i].attack;
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
                addedHealth += _gameManager.GameData.GetBaseMonsterData(Name).levelData[i].health;
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
                addedDefense += _gameManager.GameData.GetBaseMonsterData(Name).levelData[i].defense;
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
                addedMovement += _gameManager.GameData.GetBaseMonsterData(Name).levelData[i].movement;
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
                addedManaCost += _gameManager.GameData.GetBaseMonsterData(Name).levelData[i].manaCost;
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
}
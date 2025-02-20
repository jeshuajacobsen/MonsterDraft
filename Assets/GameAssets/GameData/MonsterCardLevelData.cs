

public class MonsterCardLevelData
{
    public int attack;
    public int health;
    public int defense;
    public int movement;
    public int manaCost;
    public int coinCost;

    public MonsterCardLevelData(int attack, int health, int defense, int movement, int manaCost, int coinCost)
    {
        this.attack = attack;
        this.health = health;
        this.defense = defense;
        this.movement = movement;
        this.manaCost = manaCost;
        this.coinCost = coinCost;
    }
}
public class SkillData
{
    public int ManaCost { get; set; }
    public int Damage { get; set; }
    public int Range { get; set; }
    public string name;

    public SkillData(string name)
    {
        this.name = name;
        switch (name)
        {
            case "Zap":
                ManaCost = 1;
                Damage = 2;
                Range = 2;
                break;
            case "Bubble":
                ManaCost = 1;
                Damage = 3;
                Range = 1;
                break;
            case "Leaf":
                ManaCost = 2;
                Damage = 5;
                Range = 1;
                break;
            case "Spark":
                ManaCost = 2;
                Damage = 3;
                Range = 2;
                break;
            case "Shock":
                ManaCost = 3;
                Damage = 4;
                Range = 2;
                break;
            case "Burn":
                ManaCost = 3;
                Damage = 5;
                Range = 1;
                break;
            case "Growth":
                ManaCost = 4;
                Damage = 6;
                Range = 1;
                break;
            case "Wave":
                ManaCost = 4;
                Damage = 7;
                Range = 1;
                break;
        }
    }
}
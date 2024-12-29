public class SkillData
{
    public int ManaCost { get; set; }
    public int Damage { get; set; }
    public int Range { get; set; }
    public string name;
    public string Description { get; set; }

    public SkillData(string name)
    {
        this.name = name;
        this.Description = "";
        switch (name)
        {
            case "Zap":
                ManaCost = 1;
                Damage = 3;
                Range = 2;
                break;
            case "Bubble":
                ManaCost = 1;
                Damage = 4;
                Range = 1;
                break;
            case "Leaf":
                ManaCost = 2;
                Damage = 6;
                Range = 1;
                break;
            case "Spark":
                ManaCost = 1;
                Damage = 3;
                Range = 1;
                break;
            case "Shock":
                ManaCost = 2;
                Damage = 5;
                Range = 1;
                break;
            case "Burn":
                ManaCost = 3;
                Damage = 6;
                Range = 1;
                break;
            case "Growth":
                ManaCost = 3;
                Damage = 8;
                Range = 1;
                break;
            case "Wave":
                ManaCost = 4;
                Damage = 10;
                Range = 1;
                break;
        }
    }
}
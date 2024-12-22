using System.Collections.Generic;

public class BaseActionData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Effects { get; set; }
    public int Cost { get; set; }

    public BaseActionData(string name)
    {
        Name = name;
        Description = "This is the description for " + name;
        Effects = new List<string>();

        switch (name)
        {
            case "Fireball":
                Effects.Add("Deal 3 damage to target");
                Cost = 2;
                break;
            case "Heal":
                Effects.Add("Restore 3 health to target");
                Cost = 2;
                break;
            case "Shield":
                Effects.Add("Give target 3 defense");
                Cost = 2;
                break;
            default:
                Effects.Add("No effect");
                Cost = 0;
                break;
        }
    }
}
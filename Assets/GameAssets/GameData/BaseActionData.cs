using System.Collections.Generic;

public class BaseActionData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Effects { get; set; }
    public List<string> Requirements { get; set; }
    public int Cost { get; set; }

    public BaseActionData(string name)
    {
        Name = name;
        Description = "This is the description for " + name;
        Effects = new List<string>();
        Requirements = new List<string>();

        switch (name)
        {
            case "Fireball":
                Requirements.Add("Target Enemy");
                Effects.Add("Target Enemy");
                Effects.Add("Damage 3");
                Cost = 2;
                break;
            case "Heal":
                Requirements.Add("Target Ally");
                Effects.Add("Target Ally");
                Effects.Add("Heal 4");
                Cost = 3;
                break;
            case "Shield":
                Requirements.Add("Target Ally");
                Effects.Add("Target Ally");
                Effects.Add("Buff Defense Plus 3 Duration 3");
                Description = "Gives an ally +3 defense for 3 turns";
                Cost = 3;
                break;
            case "Preparation":
                Effects.Add("Actions 2");
                Effects.Add("Draw 1");
                Cost = 5;
                break;
            case "Research":
                Effects.Add("Draw 3");
                Cost = 7;
                break;
            case "Storage":
                Effects.Add("Discard x");
                Effects.Add("Draw x");
                Effects.Add("Actions 1");
                Cost = 2;
                break;
            default:
                Effects.Add("No effect");
                break;
        }
    }
}
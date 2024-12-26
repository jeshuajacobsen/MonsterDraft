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
                Requirements.Add("Enemy Target");
                Effects.Add("Target Enemy");
                Effects.Add("Damage 3");
                
                break;
            case "Heal":
                Requirements.Add("Ally Target");
                Effects.Add("Target Ally");
                Effects.Add("Heal 3");
                
                break;
            default:
                Effects.Add("No effect");
                break;
        }
    }
}
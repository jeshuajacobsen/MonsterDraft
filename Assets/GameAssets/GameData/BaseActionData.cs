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
                Description = "Deals 3 damage to an enemy";
                Cost = 2;
                break;
            case "Heal":
                Requirements.Add("Target Ally");
                Effects.Add("Target Ally");
                Effects.Add("Heal 4");
                Description = "Heals an ally for 4 health";
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
                Description = "+2 actions\n +1 card";
                Cost = 6;
                break;
            case "Research":
                Effects.Add("Draw 3");
                Description = "+3 cards";
                Cost = 8;
                break;
            case "Storage":
                Effects.Add("Select Cards x");
                Effects.Add("Discard x");
                Effects.Add("Draw x");
                Effects.Add("Actions 1");
                Description = "Discard any number of cards then draw that many cards\n +1 action";
                Cost = 2;
                break;
            case "Alchemist":
                Effects.Add("Select Treasure 1");
                Effects.Add("Trash Selected");
                Effects.Add("Gain Treasure Costing Selected Cost Plus 4");
                Description = "Trash a treasure card to gain a treasure card costing up to 4 more";
                Cost = 6;
                break;
            case "Merchant":
                Effects.Add("Coins 2");
                Effects.Add("Actions 1");
                Effects.Add("Draw 1");
                Description = "+2 coins\n +1 action\n +1 card";
                Cost = 5;
                break;
            case "Throne Room":
                Effects.Add("Select Action 1");
                Effects.Add("Play Selected 2");
                Description = "Play an action card twice";
                Cost = 4;
                break;
            default:
                Effects.Add("No effect");
                break;
        }
    }
}
using System.Collections.Generic;

public class BaseActionData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Effects { get; set; }
    public List<string> Requirements { get; set; }
    public int Cost { get; set; }
    public int PrestigeCost { get; set; }

    public BaseActionData(string name)
    {
        Name = name;
        Description = "This is the description for " + name;
        Effects = new List<string>();
        Requirements = new List<string>();
        PrestigeCost = 0;

        switch (name)
        {
            case "Fireball":
                Requirements.Add("Target Enemy");
                Effects.Add("Target Enemy");
                Effects.Add("Animate Fireball");
                Effects.Add("Damage 3");
                Description = "Deals 3 damage to an enemy.";
                Cost = 2;
                PrestigeCost = 20;
                break;
            case "Heal":
                Requirements.Add("Target Ally");
                Effects.Add("Target Ally");
                Effects.Add("Heal 4");
                Description = "Heals an ally for 4 health.";
                Cost = 3;
                PrestigeCost = 20;
                break;
            case "Shield":
                Requirements.Add("Target Ally");
                Effects.Add("Target Ally");
                Effects.Add("Buff Defense Plus 6 Duration 3");
                Description = "Gives an ally +6 defense for 3 turns.";
                Cost = 3;
                PrestigeCost = 20;
                break;
            case "Preparation":
                Effects.Add("Actions 2");
                Effects.Add("Draw 1");
                Description = "+2 actions\n +1 card";
                Cost = 4;
                break;
            case "Research":
                Effects.Add("Draw 3");
                Description = "+3 cards";
                Cost = 6;
                break;
            case "Storage":
                Effects.Add("Select Cards x");
                Effects.Add("Discard x");
                Effects.Add("Draw x");
                Effects.Add("Actions 1");
                Description = "Discard any number of cards then draw that many cards\n +1 action.";
                Cost = 2;
                break;
            case "Alchemist":
                Effects.Add("Select Treasure 1");
                Effects.Add("Trash Selected");
                Effects.Add("Gain Treasure Costing Selected Cost Plus 6");
                Description = "Trash a treasure card to gain a treasure card costing up to 4 more.";
                Cost = 6;
                break;
            case "Merchant":
                Effects.Add("Coins 2");
                Effects.Add("Actions 1");
                Effects.Add("Draw 1");
                Description = "+2 coins\n +1 action\n +1 card.";
                Cost = 8;
                break;
            case "Throne Room":
                Effects.Add("Select Action 1");
                Effects.Add("Play Selected 2");
                Description = "Play an action card twice.";
                Cost = 4;
                break;
            case "Forge":
                Effects.Add("Select Cards x");
                Effects.Add("Save x Sum Costs");
                Effects.Add("Trash Selected");
                Effects.Add("Gain Card Costing Saved");
                Description = "Trash any number of cards. Gain a card costing up to the sum of the costs of the trashed cards.";
                Cost = 12;
                break;
            case "Vault":
                Effects.Add("Draw 2");
                Effects.Add("Select Cards x");
                Effects.Add("Discard x");
                Effects.Add("Coins x Times 2");
                Description = "+2 cards\n Discard any number of cards then gain 2 coins per card discarded.";
                Cost = 8;
                break;
            case "Bank":
                Effects.Add("Search Discard Copper");
                Effects.Add("Found Into Hand");
                Description = "Put all copper cards from your discard pile into your hand.";
                Cost = 4;
                break;
            case "Development":
                Effects.Add("Select Cards 1");
                Effects.Add("Trash Selected");
                Effects.Add("Gain Card Costing Selected Cost Plus 4");
                Description = "Trash a card. Gain a card costing up to 4 more.";
                Cost = 5;
                break;
            case "Inspiration":
                Effects.Add("Mana 2 Per Ally Monster");
                Effects.Add("Buff Attack Plus 3 Duration 1 All Ally");
                Description = "Gain 2 mana for each Ally monster in play. All Ally monsters gain +3 attack this turn.";
                Cost = 12;
                break;
            default:
                Effects.Add("No effect");
                break;
        }
    }
}
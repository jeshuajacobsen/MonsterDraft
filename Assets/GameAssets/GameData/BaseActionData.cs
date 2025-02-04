using System.Collections.Generic;

public class BaseActionData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> effects { get; set; }
    public int Cost { get; set; }
    public int PrestigeCost { get; set; }
    public List<string> onGainEffects { get; set; }

    public BaseActionData(string name)
    {
        Name = name;
        Description = "This is the description for " + name;
        effects = new List<string>();
        onGainEffects = new List<string>();
        PrestigeCost = 0;

        switch (name)
        {
            case "Fireball":
                effects.Add("Target Enemy");
                effects.Add("Animate Fireball");
                effects.Add("Damage 3");
                Description = "Deals 3 damage to an enemy.";
                Cost = 2;
                PrestigeCost = 20;
                break;
            case "Heal":
                effects.Add("Target Ally");
                effects.Add("Heal 4");
                Description = "Heals an ally for 4 health.";
                Cost = 3;
                PrestigeCost = 20;
                break;
            case "Shield":
                effects.Add("Target Ally");
                effects.Add("Buff Defense Plus 6 Duration 3");
                Description = "Gives an ally +6 defense for 3 turns.";
                Cost = 3;
                PrestigeCost = 20;
                break;
            case "Preparation":
                effects.Add("Actions 2");
                effects.Add("Draw 1");
                Description = "+2 actions\n +1 card";
                Cost = 4;
                break;
            case "Research":
                effects.Add("Draw 3");
                Description = "+3 cards";
                Cost = 6;
                break;
            case "Storage":
                effects.Add("Select Cards x");
                effects.Add("Discard x");
                effects.Add("Draw x");
                effects.Add("Actions 1");
                Description = "Discard any number of cards then draw that many cards\n +1 action.";
                Cost = 2;
                break;
            case "Alchemist":
                effects.Add("Select Treasure 1");
                effects.Add("Trash Selected");
                effects.Add("Gain Treasure Costing Selected Cost Plus 6");
                Description = "Trash a treasure card to gain a treasure card costing up to 4 more.";
                Cost = 6;
                break;
            case "Merchant":
                effects.Add("Coins 2");
                effects.Add("Actions 1");
                effects.Add("Draw 1");
                Description = "+2 coins\n +1 action\n +1 card.";
                Cost = 8;
                break;
            case "Throne Room":
                effects.Add("Select Action 1");
                effects.Add("Play Selected 2");
                Description = "Play an action card twice.";
                Cost = 4;
                break;
            case "Forge":
                effects.Add("Select Cards x");
                effects.Add("Save x Sum Costs");
                effects.Add("Trash Selected");
                effects.Add("Gain Card Costing Saved");
                Description = "Trash any number of cards. Gain a card costing up to the sum of the costs of the trashed cards.";
                Cost = 12;
                break;
            case "Vault":
                effects.Add("Draw 2");
                effects.Add("Select Cards x");
                effects.Add("Discard x");
                effects.Add("Coins x Times 2");
                Description = "+2 cards\n Discard any number of cards then gain 2 coins per card discarded.";
                Cost = 8;
                break;
            case "Bank":
                effects.Add("Search Discard Copper");
                effects.Add("Found Into Hand");
                Description = "Put all copper cards from your discard pile into your hand.";
                Cost = 4;
                break;
            case "Development":
                effects.Add("Select Cards 1");
                effects.Add("Trash Selected");
                effects.Add("Gain Card Costing Selected Cost Plus 4");
                Description = "Trash a card. Gain a card costing up to 4 more.";
                Cost = 5;
                break;
            case "Inspiration":
                effects.Add("Mana 2 Per Ally Monster");
                effects.Add("Buff Attack Plus 3 Duration 1 All Ally");
                Description = "Gain 2 mana for each Ally monster in play. All Ally monsters gain +3 attack this turn.";
                Cost = 12;
                break;
            case "Greater Fireball":
                effects.Add("Target Enemy");
                effects.Add("Animate Fireball");
                effects.Add("Damage 6");
                Description = "Deals 6 damage to an enemy.";
                Cost = 6;
                break;
            case "Mana Burst":
                effects.Add("Mana 2");
                effects.Add("Actions 2");
                Description = "Gain 2 mana and 2 actions.";
                Cost = 4;
                break;
            case "Provisions":
                effects.Add("Nothing");
                onGainEffects.Add("Option Mana 2 Draw 1 Coins 2");
                Description = "When gained, choose one: +2 mana, +1 card, or +2 coins. This card does nothing when played.";
                Cost = 0;
                break;
            case "Fury":
                this.Description = "For the rest of the turn, monster skills cost 2 less but no less than 0. Your monsters can use an additional skill.";
                this.effects.Add("PersistentEffect SkillsCost -2 Duration 1");
                this.effects.Add("PersistentEffect AdditionalSkills 1 Duration 1");
                Cost = 15;
                break;
            case "Annihilate":
                this.Description = "Destroy an enemy.";
                this.effects.Add("Target Enemy");
                this.effects.Add("Destroy Target");
                Cost = 15;
                break;
            case "Summon":
                this.Description = "When you play this, reveal the next monster card in your deck." 
                    + "If it costs 4 or less mana, you may put it into play otherwise put it into your hand.";
                this.effects.Add("Search Deck Next Monster");
                this.effects.Add("Found Option If Cost 4 Mana Less Play Else Hand");
                Cost = 8;
                break;
            case "Recycle":
                this.Description = "Trash a card from your hand. If you do, draw a card.";
                this.effects.Add("Select Cards 1");
                this.effects.Add("Trash Selected");
                this.effects.Add("Draw 1");
                break;
            default:
                effects.Add("No effect");
                break;
        }
    }
}
using System.Collections.Generic;

public class BaseActionData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> effects { get; set; }
    public int CoinCost { get; set; }
    public int PrestigeCost { get; set; }
    public List<string> onGainEffects { get; set; }
    public int maxLevel;
    public int baseLevelUpCost;
    public List<ActionCardLevelData> levelData = new List<ActionCardLevelData>();
    public Dictionary<string, string> effectVariables = new Dictionary<string, string>();

    public BaseActionData(string name)
    {
        Name = name;
        Description = "This is the description for " + name;
        effects = new List<string>();
        onGainEffects = new List<string>();
        PrestigeCost = 0;
        baseLevelUpCost = 100;
        switch (name)
        {
            case "Fireball":
                effects.Add("Target Enemy");
                effects.Add("Animate Fireball");
                effects.Add("Damage 30");
                Description = "Deals 30 damage to an enemy.";
                CoinCost = 20;
                PrestigeCost = 200;
                break;
            case "Heal":
                effects.Add("Target Ally");
                effects.Add("Heal 40");
                Description = "Heals an ally for 40 health.";
                CoinCost = 30;
                PrestigeCost = 200;
                break;
            case "Shield":
                effects.Add("Target Ally");
                effects.Add("Buff Defense Plus 60 Duration 3");
                Description = "Gives an ally +60 defense for 3 turns.";
                CoinCost = 30;
                PrestigeCost = 200;
                break;
            case "Preparation":
                effects.Add("Actions 2");
                effects.Add("Draw 1");
                Description = "+2 actions\n +1 card";
                CoinCost = 40;
                break;
            case "Research":
                effects.Add("Draw 3");
                Description = "+3 cards";
                CoinCost = 60;
                break;
            case "Storage":
                effects.Add("Select Cards x");
                effects.Add("Discard x");
                effects.Add("Draw x");
                effects.Add("Actions 1");
                Description = "Discard any number of cards then draw that many cards\n +1 action.";
                CoinCost = 20;
                break;
            case "Alchemist":
                effects.Add("Select Treasure 1");
                effects.Add("Trash Selected");
                effects.Add("Gain Treasure Costing Selected Cost Plus 6");
                Description = "Trash a treasure card to gain a treasure card costing up to 4 more.";
                CoinCost = 60;
                break;
            case "Merchant":
                effects.Add("Coins 20");
                effects.Add("Actions 1");
                effects.Add("Draw 1");
                Description = "+20 coins\n +1 action\n +1 card.";
                CoinCost = 80;
                break;
            case "Throne Room":
                effects.Add("Select Action 1");
                effects.Add("Play Selected 2");
                Description = "Play an action card twice.";
                CoinCost = 40;
                break;
            case "Forge":
                effects.Add("Select Cards x");
                effects.Add("Save x Sum Costs");
                effects.Add("Trash Selected");
                effects.Add("Gain Card Costing Saved");
                Description = "Trash any number of cards. Gain a card costing up to the sum of the costs of the trashed cards.";
                CoinCost = 120;
                break;
            case "Vault":
                effects.Add("Draw 2");
                effects.Add("Select Cards x");
                effects.Add("Discard x");
                effects.Add("Coins x Times 20");
                Description = "+2 cards\n Discard any number of cards then gain 2 coins per card discarded.";
                CoinCost = 80;
                break;
            case "Bank":
                effects.Add("Search Discard Copper");
                effects.Add("Found Into Hand");
                Description = "Put all copper cards from your discard pile into your hand.";
                CoinCost = 40;
                break;
            case "Development":
                effects.Add("Select Cards 1");
                effects.Add("Trash Selected");
                effects.Add("Gain Card Costing Selected Cost Plus 40");
                Description = "Trash a card. Gain a card costing up to 40 more.";
                CoinCost = 50;
                break;
            case "Inspiration":
                effects.Add("Mana 20 Per Ally Monster");
                effects.Add("Buff Attack Plus 30 Duration 1 All Ally");
                Description = "Gain 20 mana for each Ally monster in play. All Ally monsters gain +30 attack this turn.";
                CoinCost = 120;
                break;
            case "Greater Fireball":
                effects.Add("Target Enemy");
                effects.Add("Animate Fireball");
                effects.Add("Damage 60");
                Description = "Deals 60 damage to an enemy.";
                CoinCost = 60;
                break;
            case "Mana Burst":
                effects.Add("Mana 20");
                effects.Add("Actions 2");
                Description = "Gain 20 mana and 2 actions.";
                CoinCost = 40;
                break;
            case "Provisions":
                effects.Add("Nothing");
                onGainEffects.Add("Option Mana 20 Draw 1 Coins 20");
                Description = "When gained, choose one: +20 mana, +1 card, or +20 coins. This card does nothing when played.";
                CoinCost = 0;
                break;
            case "Fury":
                this.Description = "For the rest of the turn, monster skills cost 2 less but no less than 0. Your monsters can use an additional skill.";
                this.effects.Add("PersistentEffect SkillsCost -20 Duration 1");
                this.effects.Add("PersistentEffect AdditionalSkills 1 Duration 1");
                CoinCost = 150;
                break;
            case "Annihilate":
                this.Description = "Destroy an enemy.";
                this.effects.Add("Target Enemy");
                this.effects.Add("Destroy Target");
                CoinCost = 150;
                break;
            case "Summon":
                this.Description = "When you play this, reveal the next monster card in your deck." 
                    + "If it costs 40 or less mana, you may put it into play otherwise put it into your hand.";
                this.effects.Add("Search Deck Next Monster");
                this.effects.Add("Found Option If Cost 40 Mana Less Play Else Hand");
                CoinCost = 80;
                break;
            case "Recycle":
                this.Description = "Trash a card from your hand. If you do, draw a card.";
                this.effects.Add("Select Cards 1");
                this.effects.Add("Trash Selected");
                this.effects.Add("Draw 1");
                CoinCost = 50;
                break;
            default:
                effects.Add("No effect");
                break;
        }
        maxLevel = levelData.Count + 1;
    }
}
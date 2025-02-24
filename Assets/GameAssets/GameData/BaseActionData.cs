using System.Collections.Generic;

public class BaseActionData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> effects { get; set; }
    public int CoinCost { get; set; }
    public int LevelUpPrestigeCost { get; set; }
    public int BuyCardPrestigeCost { get; set; }
    public List<string> onGainEffects { get; set; }
    public int maxLevel;
    public List<ActionCardLevelData> levelData = new List<ActionCardLevelData>();
    public Dictionary<string, string> effectVariables = new Dictionary<string, string>();

    public BaseActionData(string name)
    {
        Name = name;
        Description = "This is the description for " + name;
        effects = new List<string>();
        onGainEffects = new List<string>();
        LevelUpPrestigeCost = 0;
        BuyCardPrestigeCost = 0;

        Dictionary<int, string> effectChanges = new Dictionary<int, string>();
        Dictionary<string, string> effectVariableChanges = new Dictionary<string, string>();

        switch (name)
        {
            case "Fireball":
                effects.Add("Target Enemy");
                effects.Add("Animate Fireball");
                effects.Add("Damage {DamageDealt}");
                Description = "Deals {DamageDealt} damage to an enemy.";
                CoinCost = 20;
                LevelUpPrestigeCost = 200;
                BuyCardPrestigeCost = 200;
                effectVariables.Add("DamageDealt", "30");

                effectVariableChanges.Add("DamageDealt", "40");
                levelData.Add(new ActionCardLevelData(5, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("DamageDealt", "50");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("DamageDealt", "60");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("DamageDealt", "70");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("DamageDealt", "80");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("DamageDealt", "90");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));
                break;
            case "Heal":
                effects.Add("Target Ally");
                effects.Add("Heal {HealthHealed}");
                Description = "Heals an ally for {HealthHealed} health.";
                CoinCost = 30;
                LevelUpPrestigeCost = 200;
                BuyCardPrestigeCost = 200;
                effectVariables.Add("HealthHealed", "40");

                effectVariableChanges.Add("HealthHealed", "50");
                levelData.Add(new ActionCardLevelData(5, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("HealthHealed", "60");
                levelData.Add(new ActionCardLevelData(5, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("HealthHealed", "70");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("HealthHealed", "80");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("HealthHealed", "90");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("HealthHealed", "100");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("HealthHealed", "110");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("HealthHealed", "120");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("HealthHealed", "130");
                levelData.Add(new ActionCardLevelData(25, null, effectVariableChanges, ""));
                break;
            case "Shield":
                effects.Add("Target Ally");
                effects.Add("Buff Defense Plus {defenseAmount} Duration {duration}");
                Description = "Gives an ally +{defenseAmount} defense for {duration} turns.";
                CoinCost = 30;
                LevelUpPrestigeCost = 200;
                BuyCardPrestigeCost = 200;
                effectVariables.Add("defenseAmount", "60");
                effectVariables.Add("duration", "3");

                effectVariableChanges.Add("duration", "4");
                levelData.Add(new ActionCardLevelData(5, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("defenseAmount", "70");
                levelData.Add(new ActionCardLevelData(5, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("defenseAmount", "80");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("duration", "5");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("defenseAmount", "90");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("defenseAmount", "100");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("duration", "6");
                effectVariableChanges.Add("defenseAmount", "110");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));
                break;
            case "Preparation":
                effects.Add("Actions {actionAmount}");
                effects.Add("Draw {drawAmount}");
                Description = "+{actionAmount} actions\n +{drawAmount} card";
                CoinCost = 40;
                LevelUpPrestigeCost = 300;
                effectVariables.Add("actionAmount", "2");
                effectVariables.Add("drawAmount", "1");

                effectVariableChanges.Add("actionAmount", "3");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("drawAmount", "2");
                levelData.Add(new ActionCardLevelData(25, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("actionAmount", "4");
                levelData.Add(new ActionCardLevelData(30, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("drawAmount", "3");
                levelData.Add(new ActionCardLevelData(40, null, effectVariableChanges, ""));
                break;
            case "Research":
                effects.Add("Draw {drawAmount}");
                Description = "+{drawAmount} cards";
                CoinCost = 60;
                LevelUpPrestigeCost = 300;
                effectVariables.Add("drawAmount", "3");

                effectVariableChanges.Add("drawAmount", "4");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("drawAmount", "5");
                levelData.Add(new ActionCardLevelData(25, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));
                break;
            case "Storage":
                effects.Add("Select Cards x");
                effects.Add("Discard x");
                effects.Add("Draw x");
                effects.Add("Actions {actionAmount}");
                Description = "Discard any number of cards then draw that many cards.\n +{actionAmount} actions";
                CoinCost = 20;
                LevelUpPrestigeCost = 200;
                effectVariables.Add("actionAmount", "1");
                effectVariables.Add("coinAmount", "0");

                effectVariableChanges.Add("actionAmount", "2");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "10");
                effectChanges.Add(4, "Coins {coinAmount}");
                levelData.Add(new ActionCardLevelData(10, effectChanges, effectVariableChanges, 
                    "Discard any number of cards then draw that many cards.\n +{actionAmount} actions\n +{coinAmount} coins"));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "20");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));
                
                levelData.Add(new ActionCardLevelData(-10, null, null, ""));
                break;
            case "Alchemist":
                effects.Add("Select Treasure 1");
                effects.Add("Trash Selected");
                effects.Add("Gain Treasure Costing Selected Cost Plus {costIncrease}");
                Description = "Trash a treasure card to gain a treasure card costing up to {costIncrease} more.";
                CoinCost = 60;
                LevelUpPrestigeCost = 300;
                effectVariables.Add("costIncrease", "40");

                effectVariableChanges.Add("costIncrease", "50");
                levelData.Add(new ActionCardLevelData(5, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("costIncrease", "60");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("costIncrease", "70");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("costIncrease", "80");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("costIncrease", "90");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("costIncrease", "100");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));
                break;
            case "Merchant":
                effects.Add("Coins 20");
                effects.Add("Actions 1");
                effects.Add("Draw 1");
                Description = "+{coinAmount} coins\n +{actionAmount} action\n +{drawAmount} card.";
                CoinCost = 60;
                LevelUpPrestigeCost = 400;
                effectVariables.Add("coinAmount", "20");
                effectVariables.Add("actionAmount", "1");
                effectVariables.Add("drawAmount", "1");

                effectVariableChanges.Add("coinAmount", "30");
                levelData.Add(new ActionCardLevelData(5, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("actionAmount", "2");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("drawAmount", "2");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "40");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "50");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));
                break;
            case "Throne Room":
                effects.Add("Select Action 1");
                effects.Add("Play Selected {playTimes}");
                Description = "Play an action card {playTimes} times.";
                CoinCost = 40;
                LevelUpPrestigeCost = 500;
                effectVariables.Add("playTimes", "2");

                effectVariableChanges.Add("playTimes", "3");
                levelData.Add(new ActionCardLevelData(30, null, effectVariableChanges, ""));
                break;
            case "Forge":
                effects.Add("Select Cards x");
                effects.Add("Save x Sum Costs");
                effects.Add("Trash Selected");
                effects.Add("Gain Card Costing Saved Plus {coinAmount}");
                Description = "Trash any number of cards. Gain a card costing up to the sum of the costs of the trashed cards.";
                CoinCost = 120;
                LevelUpPrestigeCost = 500;
                effectVariables.Add("coinAmount", "0");

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                effectVariableChanges.Add("coinAmount", "10");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, 
                    "Trash any number of cards. Gain a card costing up to the sum of the costs of the trashed cards plus {coinAmount}."));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "20");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));
                break;
            case "Vault":
                effects.Add("Draw {drawAmount}");
                effects.Add("Select Cards x");
                effects.Add("Discard x");
                effects.Add("Coins x Times {coinAmount}");
                Description = "+{drawAmount} cards\n Discard any number of cards then gain {coinAmount} coins per card discarded.";
                CoinCost = 80;
                LevelUpPrestigeCost = 500;
                effectVariables.Add("drawAmount", "2");
                effectVariables.Add("coinAmount", "20");

                effectVariableChanges.Add("coinAmount", "25");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("drawAmount", "3");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "30");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, effectVariableChanges, ""));
                break;
            case "Bank":
                effects.Add("Search Discard {coinType}");
                effects.Add("Found Into Hand");
                Description = "Put all {coinType} cards from your discard pile into your hand.";
                CoinCost = 40;
                LevelUpPrestigeCost = 600;
                effectVariables.Add("coinType", "Copper");

                effectVariableChanges.Add("coinType", "Silver");
                levelData.Add(new ActionCardLevelData(30, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinType", "Gold");
                levelData.Add(new ActionCardLevelData(30, null, effectVariableChanges, ""));
                break;
            case "Development":
                effects.Add("Select Cards 1");
                effects.Add("Trash Selected");
                effects.Add("Gain Card Costing Selected Cost Plus {coinAmount}");
                Description = "Trash a card. Gain a card costing up to {coinAmount} more.";
                CoinCost = 50;
                LevelUpPrestigeCost = 500;
                effectVariables.Add("coinAmount", "40");

                effectVariableChanges.Add("coinAmount", "50");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "60");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "70");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "80");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));
                break;
            case "Inspiration":
                effects.Add("Mana {manaAmount} Per Ally Monster");
                effects.Add("Buff Attack Plus {attackAmount} Duration 1 All Ally");
                Description = "Gain {manaAmount} mana for each Ally monster in play. All Ally monsters gain +{attackAmount} attack this turn.";
                CoinCost = 120;
                LevelUpPrestigeCost = 600;
                effectVariables.Add("manaAmount", "20");
                effectVariables.Add("attackAmount", "30");

                effectVariableChanges.Add("manaAmount", "25");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("attackAmount", "40");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("manaAmount", "30");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("attackAmount", "50");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));
                break;
            case "Greater Fireball":
                effects.Add("Target Enemy");
                effects.Add("Animate Fireball");
                effects.Add("Damage {damageDealt}");
                Description = "Deals {damageDealt} damage to an enemy.";
                CoinCost = 60;
                LevelUpPrestigeCost = 300;
                effectVariables.Add("damageDealt", "60");

                effectVariableChanges.Add("damageDealt", "70");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("damageDealt", "80");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("damageDealt", "90");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("damageDealt", "100");
                levelData.Add(new ActionCardLevelData(15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("damageDealt", "110");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("damageDealt", "120");
                levelData.Add(new ActionCardLevelData(20, null, effectVariableChanges, ""));
                break;
            case "Mana Burst":
                effects.Add("Mana {manaAmount}");
                effects.Add("Actions {actionAmount}");
                Description = "+{manaAmount} mana\n +{actionAmount} actions.";
                CoinCost = 40;
                LevelUpPrestigeCost = 200;
                effectVariables.Add("manaAmount", "20");
                effectVariables.Add("actionAmount", "2");

                effectVariableChanges.Add("manaAmount", "30");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("actionAmount", "3");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("manaAmount", "40");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("manaAmount", "50");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));
                break;
            case "Provisions":
                effects.Add("Nothing");
                onGainEffects.Add("Option Mana {manaAmount} Draw {drawAmount} Coins {coinAmount}");
                Description = "When gained, choose one: +{manaAmount} mana, +{drawAmount} card, or +{coinAmount} coins. This card does nothing when played.";
                CoinCost = 0;
                LevelUpPrestigeCost = 500;
                effectVariables.Add("manaAmount", "20");
                effectVariables.Add("drawAmount", "1");
                effectVariables.Add("coinAmount", "20");

                effectVariableChanges.Add("manaAmount", "30");
                levelData.Add(new ActionCardLevelData(0, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("drawAmount", "2");
                levelData.Add(new ActionCardLevelData(0, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "30");
                levelData.Add(new ActionCardLevelData(0, null, effectVariableChanges, ""));
                break;
            case "Fury":
                this.Description = "For the rest of the turn, monster skills cost {costReduction} less but no less than 0. Your monsters can use {additionalSkills} additional skills.";
                this.effects.Add("PersistentEffect SkillsCost -{costReduction} Duration 1");
                this.effects.Add("PersistentEffect AdditionalSkills {additionalSkills} Duration 1");
                CoinCost = 150;
                LevelUpPrestigeCost = 600;
                effectVariables.Add("costReduction", "20");
                effectVariables.Add("additionalSkills", "1");

                effectVariableChanges.Add("costReduction", "30");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("additionalSkills", "2");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("costReduction", "40");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("additionalSkills", "3");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));
                break;
            case "Annihilate":
                this.Description = "Destroy an enemy.";
                this.effects.Add("Target Enemy");
                this.effects.Add("Destroy Target");
                CoinCost = 170;
                LevelUpPrestigeCost = 700;

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));
                break;
            case "Summon":
                this.Description = "When you play this, reveal the next monster card in your deck." 
                    + "If it costs {costLimit} or less mana, you may put it into play otherwise put it into your hand.";
                this.effects.Add("Search Deck Next Monster");
                this.effects.Add("Found Option If Cost {costLimit} Mana Less Play Else Hand");
                CoinCost = 80;
                LevelUpPrestigeCost = 600;
                effectVariables.Add("costLimit", "40");

                effectVariableChanges.Add("costLimit", "50");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("costLimit", "60");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));

                levelData.Add(new ActionCardLevelData(-10, null, null, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("costLimit", "70");
                levelData.Add(new ActionCardLevelData(10, null, effectVariableChanges, ""));
                break;
            case "Recycle":
                this.Description = "Trash {trashAmount} card from your hand. If you do, draw {drawAmount} card.";
                this.effects.Add("Select Cards {trashAmount}");
                this.effects.Add("Trash Selected");
                this.effects.Add("Draw {drawAmount}");
                CoinCost = 50;
                LevelUpPrestigeCost = 500;
                effectVariables.Add("trashAmount", "1");
                effectVariables.Add("drawAmount", "1");

                effectVariableChanges.Add("trashAmount", "2");
                effectVariableChanges.Add("drawAmount", "2");
                levelData.Add(new ActionCardLevelData(30, null, effectVariableChanges, ""));
                break;
            default:
                effects.Add("No effect");
                break;
        }
        maxLevel = levelData.Count + 1;
    }
}
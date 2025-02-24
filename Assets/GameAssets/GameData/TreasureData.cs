using System.Collections.Generic;
using UnityEngine;

public class TreasureData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CoinCost { get; set; }
    public int CoinGeneration { get; set; }
    public int ManaGeneration { get; set; }
    public int LevelUpPrestigeCost { get; set; }
    public int BuyCardPrestigeCost { get; set; }
    public List<string> effects { get; set; }
    public List<string> onGainEffects;
    public int maxLevel;
    public List<TreasureCardLevelData> levelData = new List<TreasureCardLevelData>();
    public Dictionary<string, string> effectVariables = new Dictionary<string, string>();

    public TreasureData(string name)
    {
        this.effects = new List<string>();
        this.onGainEffects = new List<string>();
        LevelUpPrestigeCost = 500;
        BuyCardPrestigeCost = 0;
        this.Name = name;

        Dictionary<int, string> effectChanges = new Dictionary<int, string>();
        Dictionary<string, string> effectVariableChanges = new Dictionary<string, string>();
        switch (name)
        {
            case "Copper":
                this.Description = "+{CoinGeneration} Coins";
                this.CoinGeneration = 20;
                this.CoinCost = 10;
                LevelUpPrestigeCost = 200;
                BuyCardPrestigeCost = 200;
                levelData.Add(new TreasureCardLevelData(2, 0, 1));
                levelData.Add(new TreasureCardLevelData(2, 0, 1));
                levelData.Add(new TreasureCardLevelData(2, 0, 1));
                levelData.Add(new TreasureCardLevelData(3, 0, 2));
                levelData.Add(new TreasureCardLevelData(3, 0, 2));
                levelData.Add(new TreasureCardLevelData(3, 0, 2));
                levelData.Add(new TreasureCardLevelData(4, 0, 3));
                levelData.Add(new TreasureCardLevelData(4, 0, 3));
                levelData.Add(new TreasureCardLevelData(5, 0, 4));
                break;
            case "Silver":
                this.Description = "+{CoinGeneration} Coins";
                this.CoinGeneration = 40;
                this.CoinCost = 40;
                LevelUpPrestigeCost = 500;
                BuyCardPrestigeCost = 500;
                levelData.Add(new TreasureCardLevelData(4, 0, 2));
                levelData.Add(new TreasureCardLevelData(4, 0, 2));
                levelData.Add(new TreasureCardLevelData(4, 0, 2));
                levelData.Add(new TreasureCardLevelData(6, 0, 3));
                levelData.Add(new TreasureCardLevelData(6, 0, 3));
                levelData.Add(new TreasureCardLevelData(6, 0, 3));
                levelData.Add(new TreasureCardLevelData(7, 0, 4));
                levelData.Add(new TreasureCardLevelData(7, 0, 4));
                levelData.Add(new TreasureCardLevelData(8, 0, 5));
                break;
            case "Gold":
                this.Description = "+{CoinGeneration} Coins";
                this.CoinGeneration = 70;
                this.CoinCost = 100;
                LevelUpPrestigeCost = 2000;
                BuyCardPrestigeCost = 2000;
                levelData.Add(new TreasureCardLevelData(7, 0, 3));
                levelData.Add(new TreasureCardLevelData(7, 0, 3));
                levelData.Add(new TreasureCardLevelData(7, 0, 3));
                levelData.Add(new TreasureCardLevelData(8, 0, 4));
                levelData.Add(new TreasureCardLevelData(8, 0, 4));
                levelData.Add(new TreasureCardLevelData(8, 0, 4));
                levelData.Add(new TreasureCardLevelData(9, 0, 5));
                levelData.Add(new TreasureCardLevelData(9, 0, 5));
                levelData.Add(new TreasureCardLevelData(10, 0, 6));
                break;
            case "Platinum":
                this.Description = "+{CoinGeneration} Coins";
                this.CoinGeneration = 130;
                this.CoinCost = 170;
                LevelUpPrestigeCost = 10000;
                BuyCardPrestigeCost = 10000;
                levelData.Add(new TreasureCardLevelData(13, 0, 5));
                levelData.Add(new TreasureCardLevelData(13, 0, 5));
                levelData.Add(new TreasureCardLevelData(13, 0, 5));
                levelData.Add(new TreasureCardLevelData(15, 0, 7));
                levelData.Add(new TreasureCardLevelData(15, 0, 7));
                levelData.Add(new TreasureCardLevelData(15, 0, 7));
                levelData.Add(new TreasureCardLevelData(17, 0, 9));
                levelData.Add(new TreasureCardLevelData(17, 0, 9));
                levelData.Add(new TreasureCardLevelData(19, 0, 11));
                break;
            case "Mana Vial":
                this.Description = "+{ManaGeneration} Mana";
                this.ManaGeneration = 20;
                this.CoinCost = 10;
                LevelUpPrestigeCost = 200;
                BuyCardPrestigeCost = 200;
                levelData.Add(new TreasureCardLevelData(0, 2, 1));
                levelData.Add(new TreasureCardLevelData(0, 2, 1));
                levelData.Add(new TreasureCardLevelData(0, 2, 1));
                levelData.Add(new TreasureCardLevelData(0, 3, 2));
                levelData.Add(new TreasureCardLevelData(0, 3, 2));
                levelData.Add(new TreasureCardLevelData(0, 3, 2));
                levelData.Add(new TreasureCardLevelData(0, 4, 3));
                levelData.Add(new TreasureCardLevelData(0, 4, 3));
                levelData.Add(new TreasureCardLevelData(0, 5, 4));
                break;
            case "Mana Potion":
                this.Description = "+{ManaGeneration} Mana";
                this.ManaGeneration = 50;
                this.CoinCost = 50;
                LevelUpPrestigeCost = 900;
                BuyCardPrestigeCost = 900;
                levelData.Add(new TreasureCardLevelData(0, 5, 3));
                levelData.Add(new TreasureCardLevelData(0, 5, 3));
                levelData.Add(new TreasureCardLevelData(0, 5, 3));
                levelData.Add(new TreasureCardLevelData(0, 6, 4));
                levelData.Add(new TreasureCardLevelData(0, 6, 4));
                levelData.Add(new TreasureCardLevelData(0, 6, 4));
                levelData.Add(new TreasureCardLevelData(0, 7, 5));
                levelData.Add(new TreasureCardLevelData(0, 7, 5));
                levelData.Add(new TreasureCardLevelData(0, 8, 6));
                break;
            case "Mana Crystal":
                this.Description = "+{ManaGeneration} Mana";
                this.ManaGeneration = 100;
                this.CoinCost = 130;
                LevelUpPrestigeCost = 3000;
                BuyCardPrestigeCost = 3000;
                levelData.Add(new TreasureCardLevelData(0, 10, 4));
                levelData.Add(new TreasureCardLevelData(0, 10, 4));
                levelData.Add(new TreasureCardLevelData(0, 10, 4));
                levelData.Add(new TreasureCardLevelData(0, 12, 6));
                levelData.Add(new TreasureCardLevelData(0, 12, 6));
                levelData.Add(new TreasureCardLevelData(0, 12, 6));
                levelData.Add(new TreasureCardLevelData(0, 14, 8));
                levelData.Add(new TreasureCardLevelData(0, 14, 8));
                levelData.Add(new TreasureCardLevelData(0, 16, 10));
                break;
            case "Mana Gem":
                this.Description = "+{ManaGeneration} Mana";
                this.ManaGeneration = 180;
                this.CoinCost = 240;
                LevelUpPrestigeCost = 15000;
                BuyCardPrestigeCost = 15000;
                levelData.Add(new TreasureCardLevelData(0, 18, 8));
                levelData.Add(new TreasureCardLevelData(0, 18, 8));
                levelData.Add(new TreasureCardLevelData(0, 18, 8));
                levelData.Add(new TreasureCardLevelData(0, 20, 12));
                levelData.Add(new TreasureCardLevelData(0, 20, 12));
                levelData.Add(new TreasureCardLevelData(0, 20, 12));
                levelData.Add(new TreasureCardLevelData(0, 24, 16));
                levelData.Add(new TreasureCardLevelData(0, 24, 16));
                levelData.Add(new TreasureCardLevelData(0, 30, 20));
                break;
            case "Bauble":
                this.Description = "Gain mana equal to half your current coins.";
                this.effects.Add("Mana 10 Per Coins/2");
                this.CoinCost = 60;
                this.LevelUpPrestigeCost = 200;
                levelData.Add(new TreasureCardLevelData(0, 4, 3, null, null, "Gain mana equal to half your current coins. \n +{ManaGeneration} Mana"));
                levelData.Add(new TreasureCardLevelData(0, 4, 3));
                levelData.Add(new TreasureCardLevelData(0, 4, 3));
                levelData.Add(new TreasureCardLevelData(0, 5, 4));
                levelData.Add(new TreasureCardLevelData(0, 5, 4));
                levelData.Add(new TreasureCardLevelData(0, 5, 4));
                levelData.Add(new TreasureCardLevelData(0, 6, 5));
                levelData.Add(new TreasureCardLevelData(0, 6, 5));
                levelData.Add(new TreasureCardLevelData(0, 7, 6));
                break;
            case "Loan":
                this.Description = "+{CoinGeneration} Coins \nWhen you play this, reveal the next {numberOfTimes} in your deck. Trash it or discard it.";
                this.CoinGeneration = 20;
                this.effects.Add("Search Deck Next Treasure");
                this.effects.Add("Found Option Trash Discard");
                this.CoinCost = 40;
                this.LevelUpPrestigeCost = 200;
                this.effectVariables.Add("numberOfTimes", "treasure card");

                effectChanges.Add(2, "Search Deck Next Treasure");
                effectChanges.Add(3, "Found Option Trash Discard");
                
                effectVariableChanges.Add("numberOfTimes", "two treasure cards");
                levelData.Add(new TreasureCardLevelData(0, 0, 30, effectChanges, effectVariableChanges, ""));

                effectChanges = new Dictionary<int, string>();
                effectChanges.Add(4, "Search Deck Next Treasure");
                effectChanges.Add(5, "Found Option Trash Discard");
                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("numberOfTimes", "three treasure cards");
                levelData.Add(new TreasureCardLevelData(0, 0, 40, effectChanges, effectVariableChanges, ""));
                break;
            case "Investments":
                this.Description = "Gain {coinAmount} coins for each treasure card you've played this turn including this card.";
                this.effects.Add("Coins {coinAmount} Per Treasure Played");
                this.CoinCost = 50;
                this.LevelUpPrestigeCost = 200;
                this.effectVariables.Add("coinAmount", "10");

                effectVariableChanges.Add("coinAmount", "15");
                levelData.Add(new TreasureCardLevelData(0, 0, 10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "20");
                levelData.Add(new TreasureCardLevelData(0, 0, 20, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "25");
                levelData.Add(new TreasureCardLevelData(0, 0, 20, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "30");
                levelData.Add(new TreasureCardLevelData(0, 0, 30, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "35");
                levelData.Add(new TreasureCardLevelData(0, 0, 30, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "40");
                levelData.Add(new TreasureCardLevelData(0, 0, 40, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "45");
                levelData.Add(new TreasureCardLevelData(0, 0, 40, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "50");
                levelData.Add(new TreasureCardLevelData(0, 0, 50, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("coinAmount", "60");
                levelData.Add(new TreasureCardLevelData(0, 0, 60, null, effectVariableChanges, ""));
                break;
            case "Evolution Stone":
                this.Description = "Gain {expAmount} experience points.";
                this.effects.Add("Experience {expAmount}");
                this.CoinCost = 40;
                this.LevelUpPrestigeCost = 100;
                this.effectVariables.Add("expAmount", "10");

                effectVariableChanges.Add("expAmount", "15");
                levelData.Add(new TreasureCardLevelData(0, 0, 5, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("expAmount", "20");
                levelData.Add(new TreasureCardLevelData(0, 0, 10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("expAmount", "25");
                levelData.Add(new TreasureCardLevelData(0, 0, 10, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("expAmount", "30");
                levelData.Add(new TreasureCardLevelData(0, 0, 15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("expAmount", "35");
                levelData.Add(new TreasureCardLevelData(0, 0, 15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("expAmount", "40");
                levelData.Add(new TreasureCardLevelData(0, 0, 15, null, effectVariableChanges, ""));

                effectVariableChanges = new Dictionary<string, string>();
                effectVariableChanges.Add("expAmount", "45");
                levelData.Add(new TreasureCardLevelData(0, 0, 20, null, effectVariableChanges, ""));
                break;
            default:
                this.Description = "+10 Coins";
                this.CoinGeneration = 0;
                this.CoinCost = 0;
                break;
            //TODO interest - 20% of coins
            //investments - coins and mana don't empty
            //flanking - skills can be any direction
            //teleport - move to any empty space
        }
        maxLevel = levelData.Count + 1;
    }
}

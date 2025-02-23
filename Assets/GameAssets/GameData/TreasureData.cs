using System.Collections.Generic;
using UnityEngine;

public class TreasureData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CoinCost { get; set; }
    public int CoinGeneration { get; set; }
    public int ManaGeneration { get; set; }
    public int PrestigeCost { get; set; }
    public List<string> effects { get; set; }
    public List<string> onGainEffects;
    public int maxLevel;
    public int baseLevelUpCost;
    public List<TreasureCardLevelData> levelData = new List<TreasureCardLevelData>();
    public Dictionary<string, string> effectVariables = new Dictionary<string, string>();

    public TreasureData(string name)
    {
        this.effects = new List<string>();
        this.onGainEffects = new List<string>();
        PrestigeCost = 500;
        this.Name = name;
        baseLevelUpCost = 100;

        switch (name)
        {
            case "Copper":
                this.Description = "+{CoinGeneration} Coins";
                this.CoinGeneration = 20;
                this.CoinCost = 10;
                PrestigeCost = 200;
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
                PrestigeCost = 500;
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
                PrestigeCost = 2000;
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
                PrestigeCost = 10000;
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
                PrestigeCost = 200;
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
                PrestigeCost = 900;
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
                PrestigeCost = 3000;
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
                PrestigeCost = 15000;
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
                this.PrestigeCost = 200;
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
                this.PrestigeCost = 200;
                this.effectVariables.Add("numberOfTimes", "treasure card");

                Dictionary<int, string> effectChanges = new Dictionary<int, string>();
                effectChanges.Add(2, "Search Deck Next Treasure");
                effectChanges.Add(3, "Found Option Trash Discard");
                Dictionary<string, string> effectVariableChanges = new Dictionary<string, string>();
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
                this.Description = "Gain 10 coins for each treasure card you've played this turn including this card.";
                this.effects.Add("Coins 10 Per Treasure Played");
                this.CoinCost = 50;
                levelData.Add(new TreasureCardLevelData(0, 0, 1));
                levelData.Add(new TreasureCardLevelData(0, 0, 1));
                levelData.Add(new TreasureCardLevelData(0, 0, 1));
                levelData.Add(new TreasureCardLevelData(0, 0, 2));
                levelData.Add(new TreasureCardLevelData(0, 0, 2));
                levelData.Add(new TreasureCardLevelData(0, 0, 2));
                levelData.Add(new TreasureCardLevelData(0, 0, 3));
                levelData.Add(new TreasureCardLevelData(0, 0, 3));
                levelData.Add(new TreasureCardLevelData(0, 0, 4));
                break;
            case "Evolution Stone":
                this.Description = "Gain 200 experience points.";
                this.effects.Add("Experience 200");
                this.CoinCost = 120;
                break;
            default:
                this.Description = "+10 Coins";
                this.CoinGeneration = 0;
                this.CoinCost = 0;
                break;
        }
        maxLevel = levelData.Count + 1;
    }
}

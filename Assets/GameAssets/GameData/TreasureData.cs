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
                this.Description = "+20 Coins";
                this.CoinGeneration = 20;
                this.CoinCost = 10;
                PrestigeCost = 200;
                break;
            case "Silver":
                this.Description = "+40 Coins";
                this.CoinGeneration = 40;
                this.CoinCost = 40;
                PrestigeCost = 500;
                break;
            case "Gold":
                this.Description = "+70 Coins";
                this.CoinGeneration = 70;
                this.CoinCost = 100;
                PrestigeCost = 2000;
                break;
            case "Platinum":
                this.Description = "+130 Coins";
                this.CoinGeneration = 130;
                this.CoinCost = 170;
                PrestigeCost = 10000;
                break;
            case "Mana Vial":
                this.Description = "+20 Mana";
                this.ManaGeneration = 20;
                this.CoinCost = 10;
                PrestigeCost = 200;
                break;
            case "Mana Potion":
                this.Description = "+50 Mana";
                this.ManaGeneration = 50;
                this.CoinCost = 50;
                PrestigeCost = 900;
                break;
            case "Mana Crystal":
                this.Description = "+100 Mana";
                this.ManaGeneration = 100;
                this.CoinCost = 130;
                PrestigeCost = 3000;
                break;
            case "Mana Gem":
                this.Description = "+180 Mana";
                this.ManaGeneration = 180;
                this.CoinCost = 240;
                PrestigeCost = 15000;
                break;
            case "Bauble":
                this.Description = "Gain mana equal to half your current coins.";
                this.effects.Add("Mana 10 Per Coins/2");
                this.CoinCost = 60;
                break;
            case "Loan":
                this.Description = "+20 Coins \nWhen you play this, reveal the next treasure card in your deck. Trash it or discard it.";
                this.CoinGeneration = 20;
                this.effects.Add("Search Deck Next Treasure");
                this.effects.Add("Found Option Trash Discard");
                this.CoinCost = 40;
                break;
            case "Investments":
                this.Description = "Gain coins equal to the number of treasure cards you've played this turn including this card.";
                this.effects.Add("Coins 10 Per Treasure Played");
                this.CoinCost = 50;
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
    }
}

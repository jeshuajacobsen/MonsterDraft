using System.Collections.Generic;
using UnityEngine;
public class TreasureData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int GoldGeneration { get; set; }
    public int ManaGeneration { get; set; }
    public int PrestigeCost { get; set; }
    public List<string> effects { get; set; }
    public List<string> onGainEffects;

    public TreasureData(string name)
    {
        this.effects = new List<string>();
        this.onGainEffects = new List<string>();
        PrestigeCost = 50;
        this.Name = name;
        switch (name)
        {
            case "Copper":
                this.Description = "+2 Coins";
                this.GoldGeneration = 2;
                this.Cost = 1;
                PrestigeCost = 20;
                break;
            case "Silver":
                this.Description = "+4 Coins";
                this.GoldGeneration = 4;
                this.Cost = 4;
                PrestigeCost = 50;
                break;
            case "Gold":
                this.Description = "+7 Coins";
                this.GoldGeneration = 7;
                this.Cost = 10;
                PrestigeCost = 200;
                break;
            case "Platinum":
                this.Description = "+13 Coins";
                this.GoldGeneration = 13;
                this.Cost = 17;
                PrestigeCost = 1000;
                break;
            case "Mana Vial":
                this.Description = "+2 Mana";
                this.ManaGeneration = 2;
                this.Cost = 1;
                PrestigeCost = 20;
                break;
            case "Mana Potion":
                this.Description = "+5 Mana";
                this.ManaGeneration = 5;
                this.Cost = 5;
                PrestigeCost = 90;
                break;
            case "Mana Crystal":
                this.Description = "+10 Mana";
                this.ManaGeneration = 10;
                this.Cost = 13;
                PrestigeCost = 300;
                break;
            case "Mana Gem":
                this.Description = "+18 Mana";
                this.ManaGeneration = 18;
                this.Cost = 24;
                PrestigeCost = 1500;
                break;
            case "Bauble":
                this.Description = "Gain mana equal to half your current coins.";
                this.effects.Add("Mana 1 Per Coins/2");
                this.Cost = 6;
                break;
            case "Loan":
                this.Description = "When you play this, reveal the next treasure card in your deck. Trash it or discard it.";
                this.GoldGeneration = 2;
                this.effects.Add("Search Deck Next Treasure");
                this.effects.Add("Found Option Trash Discard");
                this.Cost = 4;
                break;
            case "Investments":
                this.Description = "Gain coins equal to the number of treasure cards you've played this turn including this card.";
                this.effects.Add("Coins 1 Per Treasure Played");
                this.Cost = 5;
                break;
            default:
                this.Description = "+1 Coins";
                this.GoldGeneration = 0;
                this.Cost = 0;
                break;
        }
        
    }
}
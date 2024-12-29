using UnityEngine;
public class TreasureData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int GoldGeneration { get; set; }
    public int ManaGeneration { get; set; }
    public TreasureData(string name)
    {
        this.Name = name;
        switch (name)
        {
            case "Copper":
                this.Description = "+2 Coins";
                this.GoldGeneration = 2;
                this.Cost = 1;
                break;
            case "Silver":
                this.Description = "+4 Coins";
                this.GoldGeneration = 4;
                this.Cost = 4;
                break;
            case "Gold":
                this.Description = "+7 Coins";
                this.GoldGeneration = 7;
                this.Cost = 8;
                break;
            case "Platinum":
                this.Description = "+13 Coins";
                this.GoldGeneration = 13;
                this.Cost = 17;
                break;
            case "Mana Vial":
                this.Description = "+2 Mana";
                this.ManaGeneration = 2;
                this.Cost = 1;
                break;
            case "Mana Potion":
                this.Description = "+5 Mana";
                this.ManaGeneration = 5;
                this.Cost = 5;
                break;
            case "Mana Crystal":
                this.Description = "+11 Mana";
                this.ManaGeneration = 11;
                this.Cost = 11;
                break;
            case "Mana Gem":
                this.Description = "+24 Mana";
                this.ManaGeneration = 24;
                this.Cost = 24;
                break;
            default:
                this.Description = "+1 Coins";
                this.GoldGeneration = 0;
                this.Cost = 0;
                break;
        }
        
    }
}
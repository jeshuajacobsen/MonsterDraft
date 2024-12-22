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
                this.Description = "+1 Coins";
                this.GoldGeneration = 1;
                this.Cost = 1;
                break;
            case "Silver":
                this.Description = "+2 Coins";
                this.GoldGeneration = 2;
                this.Cost = 3;
                break;
            case "Gold":
                this.Description = "+3 Coins";
                this.GoldGeneration = 3;
                this.Cost = 6;
                break;
            case "Mana Vial":
                this.Description = "+1 Mana";
                this.ManaGeneration = 1;
                this.Cost = 1;
                break;
            case "Mana Crystal":
                this.Description = "+2 Mana";
                this.ManaGeneration = 2;
                this.Cost = 4;
                break;
            case "Mana Gem":
                this.Description = "+3 Mana";
                this.ManaGeneration = 3;
                this.Cost = 8;
                break;
            default:
                this.Description = "+1 Coins";
                this.GoldGeneration = 0;
                this.Cost = 0;
                break;
        }
        
    }
}
using System.Collections;
using System.Collections.Generic;

public class DungeonData
{
    public string name;
    public Dictionary<string, int> cardProbabilities = new Dictionary<string, int>();
    public string guaranteedMonster;
    public int PrestigeReward { get; set; }

    public DungeonData(string name)
    {
        this.name = name;
        switch (name)
        {
            case "Forest1":
                cardProbabilities.Add("Pass", 45);
                cardProbabilities.Add("Fireball", 5);
                cardProbabilities.Add("Zaple", 20);
                cardProbabilities.Add("Owisp", 5);
                cardProbabilities.Add("Heal", 5);
                cardProbabilities.Add("Slimy", 20);
                guaranteedMonster = "Slimy";
                PrestigeReward = 100;
                break;
            case "Forest2":
                cardProbabilities.Add("Pass", 50);
                cardProbabilities.Add("Fireball", 5);
                cardProbabilities.Add("Zaple", 20);
                cardProbabilities.Add("Owisp", 10);
                cardProbabilities.Add("Heal", 10);
                cardProbabilities.Add("Slimy", 20);
                guaranteedMonster = "Slimy";
                PrestigeReward = 120;
                break;
            case "Forest3":
                cardProbabilities.Add("Pass", 50);
                cardProbabilities.Add("Fireball", 10);
                cardProbabilities.Add("Zaple", 20);
                cardProbabilities.Add("Owisp", 20);
                cardProbabilities.Add("Heal", 10);
                cardProbabilities.Add("Slimy", 20);
                guaranteedMonster = "Slimy";
                PrestigeReward = 130;
                break;
            case "Forest4":
                cardProbabilities.Add("Pass", 40);
                cardProbabilities.Add("Fireball", 10);
                cardProbabilities.Add("Zaple", 20);
                cardProbabilities.Add("Owisp", 20);
                cardProbabilities.Add("Heal", 10);
                cardProbabilities.Add("Slimy", 20);
                guaranteedMonster = "Slimy";
                PrestigeReward = 150;
                break;
            case "Forest5":
                cardProbabilities.Add("Pass", 30);
                cardProbabilities.Add("Fireball", 10);
                cardProbabilities.Add("Zaple", 20);
                cardProbabilities.Add("Owisp", 20);
                cardProbabilities.Add("Heal", 10);
                cardProbabilities.Add("Slimy", 20);
                guaranteedMonster = "Slimy";
                PrestigeReward = 300;
                break;
        }
    }
}
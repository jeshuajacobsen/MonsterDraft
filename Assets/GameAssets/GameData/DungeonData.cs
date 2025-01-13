using System.Collections;
using System.Collections.Generic;

public class DungeonData
{
    public string name;
    public Dictionary<string, float> cardProbabilities = new Dictionary<string, float>();

    public DungeonData(string name)
    {
        this.name = name;
        switch (name)
        {
            case "Forest1":
                cardProbabilities.Add("Pass", 50f);
                cardProbabilities.Add("Fireball", 5f);
                cardProbabilities.Add("Zaple", 10f);
                cardProbabilities.Add("Owisp", 10f);
                cardProbabilities.Add("Heal", 5f);
                cardProbabilities.Add("Slimy", 10f);
                break;
            case "Forest2":
                cardProbabilities.Add("Pass", 50f);
                cardProbabilities.Add("Fireball", 5f);
                cardProbabilities.Add("Zaple", 10f);
                cardProbabilities.Add("Owisp", 5f);
                cardProbabilities.Add("Heal", 10f);
                cardProbabilities.Add("Slimy", 10f);
                break;
            case "Forest3":
                cardProbabilities.Add("Pass", 40f);
                cardProbabilities.Add("Fireball", 10f);
                cardProbabilities.Add("Zaple", 10f);
                cardProbabilities.Add("Owisp", 10f);
                cardProbabilities.Add("Heal", 10f);
                cardProbabilities.Add("Slimy", 10f);
                break;
        }
    }
}
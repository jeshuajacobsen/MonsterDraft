using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    private string name;
    private Dictionary<string, int> cardProbabilities = new Dictionary<string, int>();

    public Dungeon(string name, int roundNumber)
    {
        this.name = name;
        cardProbabilities = GameManager.instance.gameData.DungeonData(name).GetDungeonData(roundNumber).cardProbabilities;
    }

    public Card DrawCard()
    {
        int totalProbability = 0;

        foreach (var probability in cardProbabilities.Values)
        {
            totalProbability += probability;
        }

        float randomPoint = Random.value * totalProbability;

        foreach (var kvp in cardProbabilities)
        {
            randomPoint -= kvp.Value;

            if (randomPoint <= 0f)
            {
                string cardName = kvp.Key;
                string type = GameManager.instance.gameData.GetCardType(cardName);
                Card card = null;

                switch (type)
                {
                    case "Monster":
                        card = new MonsterCard(cardName);
                        break;
                    case "Action":
                        card = new ActionCard(cardName);
                        break;
                    case "Treasure":
                        card = new TreasureCard(cardName);
                        break;
                    case "Pass":
                        break;
                    default:
                        Debug.LogWarning($"Unknown card type: {type}");
                        break;
                }

                return card;
            }
        }

        Debug.LogWarning("No card drawn. Check your card probabilities.");
        return null;
    }
}

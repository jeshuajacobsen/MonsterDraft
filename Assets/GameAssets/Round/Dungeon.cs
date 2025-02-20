using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    private DungeonLevelData currentDungeonLevel;
    private Dictionary<string, int> cardProbabilities = new Dictionary<string, int>();
    private int guaranteedMonsterTimer = 5;
    private string guaranteedMonster;

    public Dungeon(DungeonLevelData currentDungeonLevel, int roundNumber)
    {
        this.currentDungeonLevel = currentDungeonLevel;
        cardProbabilities = currentDungeonLevel.GetDungeonData(roundNumber).cardProbabilities;
        guaranteedMonster = currentDungeonLevel.GetDungeonData(roundNumber).guaranteedMonster;
    }

    public Card DrawCard()
    {
        guaranteedMonsterTimer--;
        if (guaranteedMonsterTimer == 0)
        {
            guaranteedMonsterTimer = 4;
            return new MonsterCard(guaranteedMonster, GameManager.instance.cardLevels[guaranteedMonster]);
        }
        
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
                        card = new MonsterCard(cardName, GameManager.instance.cardLevels[cardName]);
                        break;
                    case "Action":
                        card = new ActionCard(cardName, GameManager.instance.cardLevels[cardName]);
                        break;
                    case "Treasure":
                        card = new TreasureCard(cardName, GameManager.instance.cardLevels[cardName]);
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

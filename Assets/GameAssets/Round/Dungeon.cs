using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Dungeon
{
    private Dictionary<string, int> cardProbabilities = new Dictionary<string, int>();
    private int guaranteedMonsterTimer = 3;
    private string guaranteedMonster;

    private GameManager _gameManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, DiContainer container)
    {
        _gameManager = gameManager;
        _container = container;
    }

    public void Initialize(DungeonLevelData currentDungeonLevel, int roundNumber)
    {
        cardProbabilities = currentDungeonLevel.GetDungeonData(roundNumber).cardProbabilities;
        guaranteedMonster = currentDungeonLevel.GetDungeonData(roundNumber).guaranteedMonster;
    }

    public Card DrawCard()
    {
        guaranteedMonsterTimer--;
        if (guaranteedMonsterTimer == 0)
        {
            guaranteedMonsterTimer = 4;
            var guaranteedMonsterCard = _container.Instantiate<MonsterCard>();
            guaranteedMonsterCard.Initialize(guaranteedMonster, 1);
            return guaranteedMonsterCard;
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
                string type = _gameManager.gameData.GetCardType(cardName);
                if (type == "Monster")
                {
                    MonsterCard card = _container.Instantiate<MonsterCard>();
                    card.Initialize(cardName, 1);
                    return card;
                }
                else if (type == "Action")
                {
                    ActionCard card = _container.Instantiate<ActionCard>();
                    card.Initialize(cardName, 1);
                    return card;
                }
                else if (type == "Treasure")
                {
                    TreasureCard card = _container.Instantiate<TreasureCard>();
                    card.Initialize(cardName, 1);
                    return card;
                }
                else if (type == "Pass")
                {
                    return null;
                }
                else
                {
                    Debug.LogWarning($"Unknown card type: {type}");
                }
            }
        }

        Debug.LogWarning("No card drawn. Check your card probabilities.");
        return null;
    }
}

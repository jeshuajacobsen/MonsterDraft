using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Dungeon
{
    private Dictionary<string, int> cardProbabilities = new Dictionary<string, int>();
    private int guaranteedMonsterTimer = 3;
    private string guaranteedMonster;

    private GameManager _gameManager;
    private CardFactory _cardFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, CardFactory cardFactory, DiContainer container)
    {
        _gameManager = gameManager;
        _cardFactory = cardFactory;
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
            Card guaranteedMonsterCard = _cardFactory.CreateCard(guaranteedMonster, 1);
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
                if (cardName == "Pass")
                {
                    return null;
                }
                Card card = _cardFactory.CreateCard(cardName, 1);
            }
        }

        Debug.LogWarning("No card drawn. Check your card probabilities.");
        return null;
    }
}

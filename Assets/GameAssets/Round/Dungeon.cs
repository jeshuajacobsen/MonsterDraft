using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Dungeon
{
    private DungeonLevelData currentDungeonLevel;
    private Dictionary<string, int> cardProbabilities = new Dictionary<string, int>();
    private int guaranteedMonsterTimer = 5;
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
            var guaranteedMonsterCard = _container.Instantiate<MonsterCard>();
            guaranteedMonsterCard.Initialize(guaranteedMonster, _gameManager.cardLevels[guaranteedMonster]);
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
                Card card = null;

                switch (type)
                {
                    case "Monster":
                        card = _container.Instantiate<MonsterCard>();
                        card.Initialize(cardName, "Monster", _gameManager.cardLevels[cardName]);
                        break;
                    case "Action":
                        card = _container.Instantiate<ActionCard>();
                        card.Initialize(cardName, "Action", _gameManager.cardLevels[cardName]);
                        break;
                    case "Treasure":
                        card = _container.Instantiate<TreasureCard>();
                        card.Initialize(cardName, "Treasure", _gameManager.cardLevels[cardName]);
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

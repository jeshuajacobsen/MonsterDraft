using System.Collections.Generic;
using Zenject;

public class InitialDeck : Deck, IInitializable
{
    private GameManager _gameManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, DiContainer container)
    {
        _gameManager = gameManager;
        _container = container;
    }

    // Runs after injection is complete
    public void Initialize()
    {
        cards = new List<Card>();

        AddTreasureCard("Copper", 7);
        AddTreasureCard("Mana Vial", 5);
        AddMonsterCard("Zaple", 1);
        AddActionCard("Fireball", 1);
        AddActionCard("Heal", 1);
    }

    private void AddTreasureCard(string cardName, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var treasureCard = _container.Instantiate<TreasureCard>();
            treasureCard.Initialize(cardName, _gameManager.cardLevels[cardName]);
            cards.Add(treasureCard);
        }
    }

    private void AddMonsterCard(string cardName, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var monsterCard = _container.Instantiate<MonsterCard>();
            monsterCard.Initialize(cardName, _gameManager.cardLevels[cardName]);
            cards.Add(monsterCard);
        }
    }

    private void AddActionCard(string cardName, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var actionCard = _container.Instantiate<ActionCard>();
            actionCard.Initialize(cardName, _gameManager.cardLevels[cardName]);
            cards.Add(actionCard);
        }
    }

    public void LoadCards(List<string> cards)
    {
        this.cards = new List<Card>();

        foreach (string card in cards)
        {
            string type = _gameManager.gameData.GetCardType(card);

            if (type == "Treasure")
            {
                var treasureCard = _container.Instantiate<TreasureCard>();
                treasureCard.Initialize(card, _gameManager.cardLevels[card]);
                this.cards.Add(treasureCard);
            }
            else if (type == "Monster")
            {
                var monsterCard = _container.Instantiate<MonsterCard>();
                monsterCard.Initialize(card, _gameManager.cardLevels[card]);
                this.cards.Add(monsterCard);
            }
            else if (type == "Action")
            {
                var actionCard = _container.Instantiate<ActionCard>();
                actionCard.Initialize(card, _gameManager.cardLevels[card]);
                this.cards.Add(actionCard);
            }
        }
    }

    public void ResetLevels()
    {
        foreach (Card card in cards)
        {
            card.level = _gameManager.cardLevels[card.Name];
        }
    }
}

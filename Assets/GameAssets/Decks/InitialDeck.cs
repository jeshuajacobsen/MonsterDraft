using System.Collections.Generic;
using Zenject;

public class InitialDeck : Deck, IInitializable
{
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

    // Runs after injection is complete
    public void Initialize()
    {
        cards = new List<Card>();

        AddCard("Copper", 7);
        AddCard("Mana Vial", 5);
        AddCard("Zaple", 1);
        AddCard("Fireball", 1);
        AddCard("Heal", 1);
    }

    private void AddCard(string cardName, int count)
    {
        for (int i = 0; i < count; i++)
        {
            cards.Add(_cardFactory.CreateCard(cardName, _gameManager.cardLevels[cardName]));
        }
    }

    public void LoadCards(List<string> cards)
    {
        this.cards = new List<Card>();

        foreach (string card in cards)
        {
            this.cards.Add(_cardFactory.CreateCard(card, _gameManager.cardLevels[card]));
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

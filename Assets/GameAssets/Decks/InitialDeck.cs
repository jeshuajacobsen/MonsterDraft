using System.Collections.Generic;
using Zenject;

public class InitialDeck : Deck, IInitializable
{
    private IGameManager _gameManager;
    private CardFactory _cardFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(IGameManager gameManager, CardFactory cardFactory, DiContainer container)
    {
        _gameManager = gameManager;
        _cardFactory = cardFactory;
        _container = container;
    }

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
            cards.Add(_cardFactory.CreateCard(cardName, _gameManager.CardLevels[cardName]));
        }
    }

    public void LoadCards(List<string> cards)
    {
        this.cards = new List<Card>();

        foreach (string card in cards)
        {
            this.cards.Add(_cardFactory.CreateCard(card, _gameManager.CardLevels[card]));
        }
    }

    public void ResetLevels()
    {
        foreach (Card card in cards)
        {
            card.level = _gameManager.CardLevels[card.Name];
        }
    }
}

using System.Collections.Generic;
using Zenject;

public class RoundDeck : Deck
{

    private RunManager _runManager;
    private CardManager _cardManager;

    [Inject]
    public void Construct(RunManager runManager, CardManager cardManager)
    {
        _runManager = runManager;
        _cardManager = cardManager;
    }

    public RoundDeck Initialize()
    {
        cards = new List<Card>(_runManager.runDeck.cards);
        return this;
    }

    public List<Card> DrawHand()
    {
        List<Card> hand = new List<Card>();
        for (int i = 0; i < 5; i++)
        {
            hand.Add(DrawCard());
        }
        return hand;
    }

    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            ShuffleDiscardIntoDeck();
        }
        if (cards.Count == 0)
        {
            return null;
        }
        int index = new System.Random().Next(cards.Count);
        Card card = cards[index];
        cards.RemoveAt(index);
        return card;
    }

    public void ShuffleDiscardIntoDeck()
    {
        foreach (Card card in _cardManager.discardPile.cards)
        {
            cards.Add(card);
        }
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = new System.Random().Next(i + 1);
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
        _cardManager.discardPile.cards.Clear();
    }

    public void Discard(Card card)
    {
        Card cardToRemove = cards.Find(c => c.Name == card.Name);
        if (cardToRemove != null)
        {
            cards.Remove(cardToRemove);
        }
        _cardManager.discardPile.cards.Add(card);
    }

    public void Trash(Card card)
    {
        Card cardToRemove = cards.Find(c => c.Name == card.Name);
        if (cardToRemove != null)
        {
            cards.Remove(cardToRemove);
        }
    }
}
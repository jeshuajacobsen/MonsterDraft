using System.Collections.Generic;

public class RoundDeck : Deck
{

    public RoundDeck(RunDeck runDeck)
    {
        cards = runDeck.cards;
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
        foreach (Card card in RoundManager.instance.discardPile.cards)
        {
            cards.Add(card);
        }
        RoundManager.instance.discardPile.cards.Clear();
    }
}
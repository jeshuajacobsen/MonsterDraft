using System.Collections.Generic;

public class RunDeck : Deck
{
    

    public RunDeck(InitialDeck initialDeck)
    {
        cards = new List<Card>(initialDeck.cards);
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }
}
public class RunDeck : Deck
{
    

    public RunDeck(InitialDeck initialDeck)
    {
        cards = initialDeck.cards;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }
}
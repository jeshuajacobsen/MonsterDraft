using System.Collections.Generic;

public class Deck
{
    public List<Card> cards;
    public Deck()
    {
        // Add cards to the deck
        for (int i = 0; i < 10; i++)
        {
            //AddCard(new Card("Card " + i));
        }
    }
}
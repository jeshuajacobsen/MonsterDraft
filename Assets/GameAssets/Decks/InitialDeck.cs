using System.Collections.Generic;

public class InitialDeck : Deck
{
    public InitialDeck()
    {
        cards = new List<Card>();
        cards.Add(new TreasureCard("Copper"));
        cards.Add(new TreasureCard("Copper"));
        cards.Add(new TreasureCard("Copper"));
        cards.Add(new TreasureCard("Copper"));
        cards.Add(new TreasureCard("Mana Vial"));
        cards.Add(new TreasureCard("Mana Vial"));
        cards.Add(new TreasureCard("Mana Vial"));
        cards.Add(new TreasureCard("Mana Vial"));
        cards.Add(new MonsterCard("Zaple"));
        cards.Add(new MonsterCard("Zaple"));
    }
}
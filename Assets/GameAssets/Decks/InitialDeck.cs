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
        cards.Add(new TreasureCard("Copper"));
        cards.Add(new TreasureCard("Copper"));
        cards.Add(new TreasureCard("Copper"));
        cards.Add(new TreasureCard("Copper"));
        cards.Add(new TreasureCard("Mana Vial"));
        cards.Add(new TreasureCard("Mana Vial"));
        cards.Add(new TreasureCard("Mana Vial"));
        cards.Add(new TreasureCard("Mana Vial"));
        cards.Add(new MonsterCard("Zaple"));
        //cards.Add(new MonsterCard("Zaple"));
        cards.Add(new ActionCard("Fireball"));
        //cards.Add(new ActionCard("Shield"));
        cards.Add(new ActionCard("Heal"));
        //cards.Add(new ActionCard("Preparation"));
        //cards.Add(new ActionCard("Research"));
        //cards.Add(new ActionCard("Throne Room"));
        cards.Add(new ActionCard("Forge"));
        cards.Add(new ActionCard("Vault"));
        cards.Add(new ActionCard("Bank"));
    }
}
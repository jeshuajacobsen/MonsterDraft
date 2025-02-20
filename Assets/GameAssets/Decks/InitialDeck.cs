using System.Collections.Generic;

public class InitialDeck : Deck
{
    public InitialDeck()
    {
        cards = new List<Card>();
        cards.Add(new TreasureCard("Copper", GameManager.instance.cardLevels["Copper"]));
        cards.Add(new TreasureCard("Copper", GameManager.instance.cardLevels["Copper"]));
        cards.Add(new TreasureCard("Copper", GameManager.instance.cardLevels["Copper"]));
        cards.Add(new TreasureCard("Copper", GameManager.instance.cardLevels["Copper"]));
        cards.Add(new TreasureCard("Copper", GameManager.instance.cardLevels["Copper"]));
        cards.Add(new TreasureCard("Copper", GameManager.instance.cardLevels["Copper"]));
        cards.Add(new TreasureCard("Copper", GameManager.instance.cardLevels["Copper"]));
        cards.Add(new TreasureCard("Mana Vial", GameManager.instance.cardLevels["Mana Vial"]));
        cards.Add(new TreasureCard("Mana Vial", GameManager.instance.cardLevels["Mana Vial"]));
        cards.Add(new TreasureCard("Mana Vial", GameManager.instance.cardLevels["Mana Vial"]));
        cards.Add(new TreasureCard("Mana Vial", GameManager.instance.cardLevels["Mana Vial"]));
        cards.Add(new TreasureCard("Mana Vial", GameManager.instance.cardLevels["Mana Vial"]));
        cards.Add(new MonsterCard("Zaple", GameManager.instance.cardLevels["Zaple"]));
        //cards.Add(new MonsterCard("Zaple"));
        cards.Add(new ActionCard("Fireball", GameManager.instance.cardLevels["Fireball"]));
        //cards.Add(new ActionCard("Shield"));
        cards.Add(new ActionCard("Heal", GameManager.instance.cardLevels["Heal"]));
        //cards.Add(new ActionCard("Preparation"));
        //cards.Add(new ActionCard("Research"));
        //cards.Add(new ActionCard("Throne Room"));
        //cards.Add(new ActionCard("Forge"));
        //cards.Add(new ActionCard("Vault"));
        //cards.Add(new ActionCard("Bank"));
        //cards.Add(new ActionCard("Development"));
        //cards.Add(new MonsterCard("Borble"));
        //cards.Add(new MonsterCard("Owisp"));
        //cards.Add(new MonsterCard("Leafree"));
        //cards.Add(new MonsterCard("Squrl"));
    }

    public void LoadCards(List<string> cards)
    {
        this.cards = new List<Card>();
        foreach(string card in cards)
        {
            string type = GameManager.instance.gameData.GetCardType(card);
            if(type == "Treasure")
            {
                this.cards.Add(new TreasureCard(card, GameManager.instance.cardLevels[card]));
            }
            else if(type == "Monster")
            {
                this.cards.Add(new MonsterCard(card, GameManager.instance.cardLevels[card]));
            }
            else if(type == "Action")
            {
                this.cards.Add(new ActionCard(card, GameManager.instance.cardLevels[card]));
            }
        }
    }

    public void ResetLevels()
    {
        foreach(Card card in cards)
        {
            card.level = GameManager.instance.cardLevels[card.Name];
        }
    }
}
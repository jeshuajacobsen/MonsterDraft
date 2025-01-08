using System.Collections.Generic;

public class Dungeon
{
    public List<Card> Cards { get; set; }
    private string name;

    public Dungeon(string name, int roundNumber)
    {
        this.name = name;
        Cards = new List<Card>();
        GameManager.instance.gameData.DungeonData(name).GetDungeonData(roundNumber).cards.ForEach(cardName =>
        {
            if (cardName == "Pass")
            {
                return;
            }
            string type = GameManager.instance.gameData.GetCardType(cardName);
            if (type == "Monster")
            {
                Cards.Add(new MonsterCard(cardName));
            }
            else if (type == "Action")
            {
                Cards.Add(new ActionCard(cardName));
            }
            else if (type == "Treasure")
            {
                Cards.Add(new TreasureCard(cardName));
            }
        });
    }

    public Card DrawCard()
    {
        if (Cards.Count == 0)
        {
            return null;
        }
        var card = Cards[0];
        if (card != null)
        {
            Cards.Remove(card);
        }
        return card;
    }

    public void PostponeCard(Card card)
    {
        Cards.Add(card);
    }
}

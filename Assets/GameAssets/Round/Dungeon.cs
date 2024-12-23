using System.Collections.Generic;

public class Dungeon
{
    public List<MonsterCard> MonsterCards { get; set; }
    private string name;

    public Dungeon(string name)
    {
        this.name = name;
        MonsterCards = new List<MonsterCard>();
        GameManager.instance.gameData.GetDungeonMonsters(name).ForEach(monsterName =>
        {
            MonsterCards.Add(new MonsterCard(monsterName));
        });
    }

    public Card DrawCard()
    {
        if (MonsterCards.Count == 0)
        {
            return null;
        }
        var card = MonsterCards[0];
        if (card != null)
        {
            MonsterCards.Remove(card);
        }
        return card;
    }
}

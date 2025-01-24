using System.Collections.Generic;

public class SaveData
{
    public List<string> initialDeck { get; set; }
    public List<string> unlockedDungeonLevels { get; set; }
    public int prestigePoints { get; set; }

    public Dictionary<string, int> cardsUsed { get; set; } = new Dictionary<string, int>();
    public Dictionary<string, int> cardsBought { get; set; } = new Dictionary<string, int>();

    public SaveData(InitialDeck initialDeck, List<string> unlockedDungeonLevels, int prestigePoints)
    {
        this.initialDeck = new List<string>();
        foreach (var card in initialDeck.cards)
        {
            this.initialDeck.Add(card.Name);
        }

        this.unlockedDungeonLevels = unlockedDungeonLevels ?? new List<string>();
        this.prestigePoints = prestigePoints;
    }

    public SaveData() { }

    public void AddCardsForSaving(List<DeckEditorCardView> unlockedCards)
    {
        foreach (DeckEditorCardView card in unlockedCards)
        {
            if (cardsUsed.ContainsKey(card.card.Name))
            {
                cardsUsed[card.card.Name] = card.usingCardCount;
                cardsBought[card.card.Name] = card.boughtCardCount;
            }
            else
            {
                cardsUsed.Add(card.card.Name, card.usingCardCount);
                cardsBought.Add(card.card.Name, card.boughtCardCount);
            }
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DeckEditorPanel : MonoBehaviour
{
    public List<DeckEditorCardView> unlockedCards;
    public DeckEditorCardView cardViewPrefab;
    void Start()
    {
        transform.parent.parent.Find("CloseButton").GetComponent<UnityEngine.UI.Button>()
                .onClick.AddListener(GameManager.instance.CloseDeckEditor);
    }

    void Update()
    {
        
    }

    public void FirstTimeSetup()
    {
        foreach (var cardName in GameManager.instance.gameData.availableDeckEditorCards)
        {
            DeckEditorCardView cardView = Instantiate(cardViewPrefab, transform);
            string type = GameManager.instance.gameData.GetCardType(cardName.Key);
            if(type == "Treasure")
            {
                cardView.InitValues(new TreasureCard(cardName.Key), cardName.Value);
            }
            else if(type == "Monster")
            {
                cardView.InitValues(new MonsterCard(cardName.Key), cardName.Value);
            }
            else if(type == "Action")
            {
                cardView.InitValues(new ActionCard(cardName.Key), cardName.Value);
            }
            int count = GameManager.instance.selectedInitialDeck.cards.Count(card => card.Name == cardName.Key);
            cardView.LoadValues(count, count);
            unlockedCards.Add(cardView);
        }
    }

    public void LoadCards(SaveData saveData)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        unlockedCards.Clear();
        foreach (string card in saveData.cardsUsed.Keys)
        {
            DeckEditorCardView cardView = Instantiate(cardViewPrefab, transform);
            string type = GameManager.instance.gameData.GetCardType(card);
            if(type == "Treasure")
            {
                cardView.InitValues(new TreasureCard(card), GameManager.instance.gameData.availableDeckEditorCards[card]);
            }
            else if(type == "Monster")
            {
                cardView.InitValues(new MonsterCard(card), GameManager.instance.gameData.availableDeckEditorCards[card]);
            }
            else if(type == "Action")
            {
                cardView.InitValues(new ActionCard(card), GameManager.instance.gameData.availableDeckEditorCards[card]);
            }
            cardView.LoadValues(saveData.cardsUsed[card], saveData.cardsBought[card]);
            unlockedCards.Add(cardView);
        }
    }

    public InitialDeck GetSelectedInitialDeck()
    {
        InitialDeck initialDeck = new InitialDeck();
        List<string> selectedCards = new List<string>();
        foreach (var cardView in unlockedCards)
        {
            for (int i = 0; i < cardView.usingCardCount; i++)
            {
                selectedCards.Add(cardView.card.Name);
            }
        }
        initialDeck.LoadCards(selectedCards);
        return initialDeck;
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Zenject;

public class DeckEditorPanel : MonoBehaviour
{
    public List<DeckEditorCardView> unlockedCards;
    public DeckEditorCardView cardViewPrefab;

    private GameManager _gameManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, DiContainer container)
    {
        _gameManager = gameManager;
        _container = container;
    }

    void Start()
    {
        transform.parent.parent.Find("CloseButton").GetComponent<UnityEngine.UI.Button>()
                .onClick.AddListener(_gameManager.CloseDeckEditor);
    }

    void Update()
    {
        
    }

    public void OnOpen()
    {
        transform.parent.parent.Find("PrestigePanel/Text").GetComponent<TextMeshProUGUI>().text = _gameManager.PrestigePoints.ToString();
    }

    public void FirstTimeSetup()
    {
        foreach (var cardName in _gameManager.gameData.availableDeckEditorCards)
        {
            DeckEditorCardView cardView = _container.InstantiatePrefabForComponent<DeckEditorCardView>(cardViewPrefab, transform);

            string type = _gameManager.gameData.GetCardType(cardName.Key);

            if (type == "Treasure")
            {
                var treasureCard = _container.Instantiate<TreasureCard>();
                treasureCard.Initialize(cardName.Key, _gameManager.cardLevels[cardName.Key]);
                cardView.Initialize(treasureCard, cardName.Value);
            }
            else if (type == "Monster")
            {
                var monsterCard = _container.Instantiate<MonsterCard>();
                monsterCard.Initialize(cardName.Key, _gameManager.cardLevels[cardName.Key]);
                cardView.Initialize(monsterCard, cardName.Value);
            }
            else if (type == "Action")
            {
                var actionCard = _container.Instantiate<ActionCard>();
                actionCard.Initialize(cardName.Key, _gameManager.cardLevels[cardName.Key]);
                cardView.Initialize(actionCard, cardName.Value);
            }

            int count = _gameManager.selectedInitialDeck.cards.Count(card => card.Name == cardName.Key);
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
        DeckEditorCardView cardView = _container.InstantiatePrefabForComponent<DeckEditorCardView>(cardViewPrefab, transform);
        string type = _gameManager.gameData.GetCardType(card);

        if (type == "Treasure")
        {
            var treasureCard = _container.Instantiate<TreasureCard>();
            treasureCard.Initialize(card, _gameManager.cardLevels[card]);
            cardView.Initialize(treasureCard, _gameManager.gameData.availableDeckEditorCards[card]);
        }
        else if (type == "Monster")
        {
            var monsterCard = _container.Instantiate<MonsterCard>();
            monsterCard.Initialize(card, _gameManager.cardLevels[card]);
            cardView.Initialize(monsterCard, _gameManager.gameData.availableDeckEditorCards[card]);
        }
        else if (type == "Action")
        {
            var actionCard = _container.Instantiate<ActionCard>();
            actionCard.Initialize(card, _gameManager.cardLevels[card]);
            cardView.Initialize(actionCard, _gameManager.gameData.availableDeckEditorCards[card]);
        }

        cardView.LoadValues(saveData.cardsUsed[card], saveData.cardsBought[card]);
        unlockedCards.Add(cardView);
    }
}


    public InitialDeck GetSelectedInitialDeck()
    {
        InitialDeck initialDeck = _container.Instantiate<InitialDeck>();
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

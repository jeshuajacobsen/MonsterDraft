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
    private CardFactory _cardFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, CardFactory cardFactory, DiContainer container)
    {
        _gameManager = gameManager;
        _cardFactory = cardFactory;
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

            cardView.Initialize(_cardFactory.CreateCard(cardName.Key, _gameManager.cardLevels[cardName.Key]), cardName.Value);

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

            cardView.Initialize(_cardFactory.CreateCard(card, _gameManager.cardLevels[card]), 
                                _gameManager.gameData.availableDeckEditorCards[card]);

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

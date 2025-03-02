using Zenject;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private SmallCardView SmallCardViewPrefab;
    public GameObject handContent;
    public List<SmallCardView> hand = new List<SmallCardView>();
    public RoundDeck roundDeck;
    public DiscardPile discardPile;

    private GameManager _gameManager;
    private RunManager _runManager;
    private DungeonManager _dungeonManager;
    private PlayerStats _playerStats;
    private DiContainer _container;

    private SkillVisualEffect.Factory _skillVisualEffectFactory;
    private CardVisualEffect.Factory _cardVisualEffectFactory;

    [Inject]
    public void Construct(GameManager gameManager, 
                          RunManager runManager, 
                          DungeonManager dungeonManager,
                          PlayerStats playerStats,
                          SkillVisualEffect.Factory skillVisualEffectFactory,
                          CardVisualEffect.Factory cardVisualEffectFactory,
                          DiContainer container)
    {
        _gameManager = gameManager;
        _runManager = runManager;
        _dungeonManager = dungeonManager;
        _playerStats = playerStats;
        _skillVisualEffectFactory = skillVisualEffectFactory;
        _cardVisualEffectFactory = cardVisualEffectFactory;
        _container = container;
    }

    public void Start()
    {
        _runManager.startRoundEvent.AddListener(StartRound);
    }

    public void StartRound()
    {
        DiscardHand();
        discardPile.cards.Clear();
        roundDeck = _container.Instantiate<RoundDeck>().Initialize();
        List<Card> newHand = roundDeck.DrawHand();
        foreach (Card card in newHand)
        {
            AddCardToHand(card);
        }
    }

    public void DiscardHand()
    {
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            if (hand[i] != null)
            {
                discardPile.AddCard(hand[i].card);
                Destroy(hand[i].gameObject);
            }
        }
        hand.Clear();

        foreach (Transform child in handContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RemoveCardFromHand(SmallCardView cardView)
    {
        hand.Remove(cardView);
        Destroy(cardView.gameObject);
        ResizeHand();
    }

    public void AddCardToHand(Card card)
    {
        SmallCardView newCard = _container.InstantiatePrefabForComponent<SmallCardView>(SmallCardViewPrefab, handContent.transform);
        newCard.Initialize(card);
        hand.Add(newCard);
        RectTransform newItemRect = newCard.GetComponent<RectTransform>();

        ResizeHand();
    }

    public void ResizeHand()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            RectTransform newItemRect = hand[i].GetComponent<RectTransform>();
            float itemWidth = newItemRect.rect.width;
            newItemRect.anchoredPosition = new Vector2(i * (itemWidth + 10) + itemWidth/2, newItemRect.rect.height / 2 + 20);
        }
        float totalWidth = hand.Count * SmallCardViewPrefab.GetComponent<RectTransform>().rect.width + 10;
        handContent.GetComponent<RectTransform>().sizeDelta = new Vector2(totalWidth, handContent.GetComponent<RectTransform>().rect.height);
    }

    public void TrashCardsFromDeck(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            roundDeck.Trash(card);
        }
    }

    public void DiscardCardsFromDeck(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            roundDeck.Discard(card);
        }
    }

    public void DestroyCard(SmallCardView card)
    {
        Destroy(card.gameObject);
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class StockPile : MonoBehaviour
{
    public string Name { get; set; }
    public int StockLeft { get; set; }
    public int Cost { get; set; }

    private string cardType;
    public Card card;

    private GameManager _gameManager;
    private RoundManager _roundManager;
    private PlayerStats _playerStats;
    private SpriteManager _spriteManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, 
                          RoundManager roundManager, 
                          PlayerStats playerStats,
                          SpriteManager spriteManager, 
                          DiContainer container)
    {
        _gameManager = gameManager;
        _roundManager = roundManager;
        _playerStats = playerStats;
        _spriteManager = spriteManager;
        _container = container;
    }

    void Start()
    {
        transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(BuyCard);
    }

    public void Initialize(string name, int stockLeft, string cardType)
    {
        this.cardType = cardType;
        switch (this.cardType)
        {
            case "Monster":
                card = _container.Instantiate<MonsterCard>();
                ((MonsterCard)card).Initialize(name, _gameManager.cardLevels[name]);
                break;
            case "Treasure":
                card = _container.Instantiate<TreasureCard>();
                ((TreasureCard)card).Initialize(name, _gameManager.cardLevels[name]);
                break;
            case "Action":
                card = _container.Instantiate<ActionCard>();
                ((ActionCard)card).Initialize(name, _gameManager.cardLevels[name]);
                break;
        }

        this.Name = name;

        if (transform.Find("NameText") != null)
        {
            transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        }

        this.StockLeft = stockLeft;
        transform.Find("QuantityBackgroundImage").Find("QuantityText").GetComponent<TextMeshProUGUI>().text = stockLeft.ToString();
        this.Cost = this.card.CoinCost;
        transform.Find("CostBackgroundImage").Find("CostText").GetComponent<TextMeshProUGUI>().text = this.Cost.ToString();
        transform.Find("CardImage").GetComponent<Image>().sprite = _spriteManager.GetSprite(name);
    }

    private void BuyCard()
    {
        if (_roundManager.gameState is MainPhase && ((MainPhase)_roundManager.gameState).currentState is GainingCardState)
        {
            MainPhase mainPhase = (MainPhase)_roundManager.gameState;
            if ((mainPhase.currentState as GainingCardState).MeetsRestrictions(card))
            {
                (mainPhase.currentState as GainingCardState).GainCard(card);
                _roundManager.cardsGainedThisRound.Add(card);
                StockLeft--;
                return;
            }
        }

        if (_playerStats.Coins >= Cost)
        {
            _playerStats.Coins -= Cost;

            Card newCard = null;

            switch (cardType)
            {
                case "Monster":
                    newCard = _container.Instantiate<MonsterCard>();
                    ((MonsterCard)newCard).Initialize(Name, _gameManager.cardLevels[Name]);
                    break;
                case "Treasure":
                    newCard = _container.Instantiate<TreasureCard>();
                    ((TreasureCard)newCard).Initialize(Name, _gameManager.cardLevels[Name]);
                    break;
                case "Action":
                    newCard = _container.Instantiate<ActionCard>();
                    ((ActionCard)newCard).Initialize(Name, _gameManager.cardLevels[Name]);
                    break;
            }

            _roundManager.discardPile.AddCard(newCard);
            _roundManager.cardsGainedThisRound.Add(newCard);

            if (newCard is ActionCard actionCard && actionCard.OnGainEffects.Count > 0)
            {
                MainPhase mainPhase = (MainPhase)_roundManager.gameState;
                mainPhase.gainedCard = actionCard;
                mainPhase.SwitchPhaseState(_container.Instantiate<ResolvingOnGainEffectState>());
            }

            StockLeft--;
            transform.Find("QuantityBackgroundImage").Find("QuantityText").GetComponent<TextMeshProUGUI>().text = StockLeft.ToString();
        }
    }
}

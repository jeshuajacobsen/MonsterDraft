using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StockPile : MonoBehaviour
{
    public string Name { get; set; }
    public int StockLeft { get; set; }
    public int Cost { get; set; }

    private string cardType;
    public Card card;

    void Start()
    {
        transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(BuyCard);
    }

    
    void Update()
    {
        
    }

    public void InitValues(string name, int stockLeft, string cardType)
    {
        this.cardType = cardType;
        if (this.cardType == "Monster")
        {
            card = new MonsterCard(name);
        }
        else if (this.cardType == "Treasure")
        {
            card = new TreasureCard(name);
        }
        else if (this.cardType == "Action")
        {
            card = new ActionCard(name);
        }
        this.Name = name;
        if (transform.Find("NameText") != null)
        {
            transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        }
        this.StockLeft = stockLeft;
        transform.Find("QuantityBackgroundImage").Find("QuantityText").GetComponent<TextMeshProUGUI>().text = stockLeft.ToString();
        this.Cost = this.card.Cost;
        transform.Find("CostBackgroundImage").Find("CostText").GetComponent<TextMeshProUGUI>().text = this.Cost.ToString();
        transform.Find("CardImage").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite(name);
    }

    private void BuyCard()
    {
        if (RoundManager.instance.isGainingCard)
        {
            MainPhase mainPhase = (MainPhase)RoundManager.instance.gameState;
            if ((mainPhase.currentState as GainingCardState).MeetsRestrictions(card))
            {
                (mainPhase.currentState as GainingCardState).GainCard(card);
                RoundManager.instance.cardsGainedThisRound.Add(card);
                return;
            }
        }
        if (RoundManager.instance.Coins >= Cost)
        {
            RoundManager.instance.Coins -= Cost;

            if (cardType == "Monster")
            {
                Card newCard = new MonsterCard(Name);
                RoundManager.instance.discardPile.AddCard(newCard);
                RoundManager.instance.cardsGainedThisRound.Add(newCard);
            }
            else if (cardType == "Treasure")
            {
                Card newCard = new TreasureCard(Name);
                RoundManager.instance.discardPile.AddCard(newCard);
                RoundManager.instance.cardsGainedThisRound.Add(newCard);
            }
            else if (cardType == "Action")
            {
                ActionCard newCard = new ActionCard(Name);
                RoundManager.instance.discardPile.AddCard(newCard);
                RoundManager.instance.cardsGainedThisRound.Add(newCard);

                if (newCard.onGainEffects.Count > 0)
                {
                    MainPhase mainPhase = (MainPhase)RoundManager.instance.gameState;
                    mainPhase.gainedCard = newCard;
                    mainPhase.SetState(new ResolvingOnGainEffectState(mainPhase));
                }
            }
            StockLeft--;
            transform.Find("QuantityBackgroundImage").Find("QuantityText").GetComponent<TextMeshProUGUI>().text = StockLeft.ToString();
        }
        
    }
}

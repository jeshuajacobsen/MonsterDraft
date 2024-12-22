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
    private Card card;

    void Start()
    {
        transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(BuyCard);
    }

    
    void Update()
    {
        
    }

    public void InitValues(string name, int stockLeft,string cardType)
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
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        this.StockLeft = stockLeft;
        transform.Find("QuantityBackgroundImage").Find("QuantityText").GetComponent<TextMeshProUGUI>().text = stockLeft.ToString();
        this.Cost = card.Cost;
        transform.Find("CostBackgroundImage").Find("CostText").GetComponent<TextMeshProUGUI>().text = this.Cost.ToString();
        transform.Find("CardImage").GetComponent<Image>().sprite = SpriteManager.instance.GetCardSprite(name);
    }

    private void BuyCard()
    {
        if (RoundManager.instance.Coins >= Cost)
        {
            RoundManager.instance.Coins -= Cost;

            if (cardType == "Monster")
            {
                RoundManager.instance.discardPile.AddCard(new MonsterCard(Name));
            }
            else if (cardType == "Treasure")
            {
                RoundManager.instance.discardPile.AddCard(new TreasureCard(Name));
            }
            else if (cardType == "Action")
            {
                RoundManager.instance.discardPile.AddCard(new ActionCard(Name));
            }
            StockLeft--;
            transform.Find("QuantityBackgroundImage").Find("QuantityText").GetComponent<TextMeshProUGUI>().text = StockLeft.ToString();
        }
    }
}

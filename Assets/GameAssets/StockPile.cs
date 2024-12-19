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

    void Start()
    {
        transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(BuyCard);
    }

    
    void Update()
    {
        
    }

    public void InitValues(string name, int stockLeft, int cost)
    {
        this.Name = name;
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;
        this.StockLeft = stockLeft;
        transform.Find("QuantityBackgroundImage").Find("QuantityText").GetComponent<TextMeshProUGUI>().text = stockLeft.ToString();
        this.Cost = cost;
        transform.Find("CostBackgroundImage").Find("CostText").GetComponent<TextMeshProUGUI>().text = cost.ToString();
        transform.Find("CardImage").GetComponent<Image>().sprite = SpriteManager.instance.GetCardSprite(name);
    }

    private void BuyCard()
    {
        if (RoundManager.instance.Coins >= Cost)
        {
            RoundManager.instance.Coins -= Cost;
            RoundManager.instance.discardPile.AddCard(new Card(Name));
            StockLeft--;
            transform.Find("QuantityBackgroundImage").Find("QuantityText").GetComponent<TextMeshProUGUI>().text = StockLeft.ToString();
        }
    }
}

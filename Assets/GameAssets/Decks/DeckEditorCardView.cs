using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DeckEditorCardView : MonoBehaviour
{
    public Card card;
    private GameObject LockedPanel;
    public int usingCardCount = 0;
    public int boughtCardCount = 0;
    private int cardLimit = 0;

    void Start()
    {
        transform.Find("AddButton").GetComponent<Button>().onClick.AddListener(IncreaseCardCount);
        transform.Find("RemoveButton").GetComponent<Button>().onClick.AddListener(DecreaseCardCount);
        transform.Find("BuyCardButton").GetComponent<Button>().onClick.AddListener(BuyCard);
    }

    void Update()
    {

    }

    public void InitValues(Card card, int cardLimit)
    {
        this.card = card;
        this.cardLimit = cardLimit;
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = card.Name;
        transform.Find("CardImage").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite(card.Name);
        transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = usingCardCount.ToString() + "/" + boughtCardCount.ToString();
        transform.Find("PrestigeCostText").GetComponent<TextMeshProUGUI>().text = card.PrestigeCost.ToString();
    }

    private void IncreaseCardCount()
    {
        if (usingCardCount < boughtCardCount)
        {
            usingCardCount++;
            transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = usingCardCount.ToString() + "/" + boughtCardCount.ToString();
        }
    }

    private void DecreaseCardCount()
    {
        if (usingCardCount > 0)
        {
            usingCardCount--;
            transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = usingCardCount.ToString() + "/ " + boughtCardCount.ToString();
        }
    }

    public void BuyCard()
    {
        if (boughtCardCount < cardLimit && GameManager.instance.PrestigePoints >= card.PrestigeCost)
        {
            boughtCardCount++;
            GameManager.instance.PrestigePoints -= card.PrestigeCost;
            transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = usingCardCount.ToString() + "/" + boughtCardCount.ToString();
        }
    }

    public void LoadValues(int usingCardCount, int boughtCardCount)
    {
        this.usingCardCount = usingCardCount;
        this.boughtCardCount = boughtCardCount;
        transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = usingCardCount.ToString() + "/" + boughtCardCount.ToString();
    }
}

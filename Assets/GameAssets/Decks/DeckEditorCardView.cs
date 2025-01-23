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
    private int UsingCardCount = 0;
    private int boughtCardCount = 0;
    private int cardLimit = 0;

    void Start()
    {
        transform.Find("AddButton").GetComponent<Button>().onClick.AddListener(IncreaseCardCount);
        transform.Find("RemoveButton").GetComponent<Button>().onClick.AddListener(DecreaseCardCount);
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
        transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = UsingCardCount.ToString() + "/" + boughtCardCount.ToString();
    }

    private void IncreaseCardCount()
    {
        if (UsingCardCount < boughtCardCount)
        {
            UsingCardCount++;
            transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = UsingCardCount.ToString() + "/" + boughtCardCount.ToString();
        }
    }

    private void DecreaseCardCount()
    {
        if (UsingCardCount > 0)
        {
            UsingCardCount--;
            transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = UsingCardCount.ToString() + "/ " + boughtCardCount.ToString();
        }
    }
}

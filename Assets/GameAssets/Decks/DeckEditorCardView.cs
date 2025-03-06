using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Zenject;

public class DeckEditorCardView : MonoBehaviour
{
    public Card card;
    private GameObject LockedPanel;
    public int usingCardCount = 0;
    public int boughtCardCount = 0;
    private int cardLimit = 0;

    private IGameManager _gameManager;
    private SpriteManager _spriteManager;

    [Inject]
    public void Construct(IGameManager gameManager, SpriteManager spriteManager)
    {
        _gameManager = gameManager;
        _spriteManager = spriteManager;
    }

    void Start()
    {
        transform.Find("AddButton").GetComponent<Button>().onClick.AddListener(IncreaseCardCount);
        transform.Find("RemoveButton").GetComponent<Button>().onClick.AddListener(DecreaseCardCount);
        transform.Find("BuyCardButton").GetComponent<Button>().onClick.AddListener(BuyCard);
    }

    void Update()
    {

    }

    public void Initialize(Card card, int cardLimit)
    {
        this.card = card;
        this.cardLimit = cardLimit;
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = card.Name;
        transform.Find("CardImage").GetComponent<Image>().sprite = _spriteManager.GetSprite(card.Name);
        transform.Find("CardCountText").GetComponent<TextMeshProUGUI>().text = usingCardCount.ToString() + "/" + boughtCardCount.ToString();
        transform.Find("PrestigeCostText").GetComponent<TextMeshProUGUI>().text = card.BuyCardPrestigeCost.ToString();
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
        if (boughtCardCount < cardLimit && _gameManager.PrestigePoints >= card.BuyCardPrestigeCost)
        {
            boughtCardCount++;
            _gameManager.PrestigePoints -= card.BuyCardPrestigeCost;
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

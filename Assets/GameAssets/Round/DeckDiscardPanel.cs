using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class DeckDiscardPanel : MonoBehaviour
{

    private RoundManager _roundManager;
    private CardManager _cardManager;

    [Inject]
    public void Construct(RoundManager roundManager, CardManager cardManager)
    {
        _roundManager = roundManager;
        _cardManager = cardManager;
    }

    void Start()
    {
        transform.Find("EndTurnButton").GetComponent<Button>().onClick.AddListener(_roundManager.EndTurn);
    }

    void Update()
    {
        if (_cardManager.roundDeck != null)
        {
            transform.Find("DeckImage").Find("DeckSizeText").GetComponent<TextMeshProUGUI>().text = _cardManager.roundDeck.cards.Count.ToString();
            transform.Find("DiscardImage").Find("DiscardSizeText").GetComponent<TextMeshProUGUI>().text = _cardManager.discardPile.cards.Count.ToString();
        }
    }
}

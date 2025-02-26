using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class DeckDiscardPanel : MonoBehaviour
{

    private RoundManager _roundManager;

    [Inject]
    public void Construct(RoundManager roundManager)
    {
        _roundManager = roundManager;
    }

    void Start()
    {
        transform.Find("EndTurnButton").GetComponent<Button>().onClick.AddListener(_roundManager.EndTurn);
    }

    void Update()
    {
        if (_roundManager.roundDeck != null)
        {
            transform.Find("DeckImage").Find("DeckSizeText").GetComponent<TextMeshProUGUI>().text = _roundManager.roundDeck.cards.Count.ToString();
            transform.Find("DiscardImage").Find("DiscardSizeText").GetComponent<TextMeshProUGUI>().text = _roundManager.discardPile.cards.Count.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckDiscardPanel : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (RoundManager.instance.roundDeck != null)
        {
            transform.Find("DeckImage").Find("DeckSizeText").GetComponent<TextMeshProUGUI>().text = RoundManager.instance.roundDeck.cards.Count.ToString();
            transform.Find("DiscardImage").Find("DiscardSizeText").GetComponent<TextMeshProUGUI>().text = RoundManager.instance.discardPile.cards.Count.ToString();
        }
    }
}

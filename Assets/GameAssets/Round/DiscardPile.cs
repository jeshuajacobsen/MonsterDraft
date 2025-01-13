using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DiscardPile : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
        gameObject.transform.Find("DiscardSizeText").GetComponent<TextMeshProUGUI>().text = cards.Count.ToString();
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
        gameObject.transform.Find("DiscardSizeText").GetComponent<TextMeshProUGUI>().text = cards.Count.ToString();
    }
}
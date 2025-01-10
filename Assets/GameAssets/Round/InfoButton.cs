using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RoundManager.instance.largeCardView != null)
        {
            SmallCardView smallCard = GetComponentInParent<SmallCardView>();
            StockPile stockPile = GetComponentInParent<StockPile>();

            if (smallCard != null)
            {
                RoundManager.instance.largeCardView.SetCard(smallCard.card, eventData.position);
                RoundManager.instance.largeCardView.gameObject.SetActive(true);
            }
            else if (stockPile != null)
            {
                RoundManager.instance.largeCardView.SetCard(stockPile.card, eventData.position);
                RoundManager.instance.largeCardView.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RoundManager.instance.largeCardView.gameObject.SetActive(false);
    }
}

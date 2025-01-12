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
        if (RoundManager.instance.largeCardView1 != null)
        {
            SmallCardView smallCard = GetComponentInParent<SmallCardView>();
            StockPile stockPile = GetComponentInParent<StockPile>();

            if (smallCard != null && !(smallCard.card is MonsterCard))
            {
                RoundManager.instance.largeCardView1.SetCard(smallCard.card, eventData.position);
                RoundManager.instance.largeCardView1.gameObject.SetActive(true);
                return;
            }
            else if (stockPile != null && !(stockPile.card is MonsterCard))
            {
                RoundManager.instance.largeCardView1.SetCard(stockPile.card, eventData.position);
                RoundManager.instance.largeCardView1.gameObject.SetActive(true);
                return;
            }
            

            MonsterCard currentCard = null;
            string evolvesFrom = null;
            string evolvesTo = null;

            if (smallCard != null)
            {
                currentCard = new MonsterCard(smallCard.card.Name);
                evolvesFrom = currentCard.evolvesFrom;
                evolvesTo = currentCard.evolvesTo;
            }
            else if (stockPile != null)
            {
                currentCard = new MonsterCard(stockPile.card.Name);
                evolvesFrom = currentCard.evolvesFrom;
                evolvesTo = currentCard.evolvesTo;
            }

            Vector2 firstCardPosition = new Vector2(-1100, 100);
            Vector2 secondCardPosition = new Vector2(0, 100);
            Vector2 thirdCardPosition = new Vector2(1100, 100);

            if (currentCard != null)
            {
                if (string.IsNullOrEmpty(evolvesFrom) && string.IsNullOrEmpty(evolvesTo))
                {
                    RoundManager.instance.largeCardView1.SetCard(currentCard, eventData.position);
                    RoundManager.instance.largeCardView1.gameObject.SetActive(true);
                    return;
                }

                if (!string.IsNullOrEmpty(evolvesTo) && !string.IsNullOrEmpty(evolvesFrom))
                {
                    MonsterCard evolvesToCard = new MonsterCard(evolvesTo);
                    MonsterCard evolvesFromCard = new MonsterCard(evolvesFrom);

                    RoundManager.instance.largeCardView1.SetCard(evolvesFromCard, firstCardPosition);
                    RoundManager.instance.largeCardView1.gameObject.SetActive(true);

                    RoundManager.instance.largeCardView2.SetCard(currentCard, secondCardPosition);
                    RoundManager.instance.largeCardView2.gameObject.SetActive(true);

                    RoundManager.instance.largeCardView3.SetCard(evolvesToCard, thirdCardPosition);
                    RoundManager.instance.largeCardView3.gameObject.SetActive(true);
                    return;
                }

                if (!string.IsNullOrEmpty(evolvesTo))
                {
                    MonsterCard evolvesToCard = new MonsterCard(evolvesTo);

                    RoundManager.instance.largeCardView1.SetCard(currentCard, firstCardPosition);
                    RoundManager.instance.largeCardView1.gameObject.SetActive(true);

                    RoundManager.instance.largeCardView2.SetCard(evolvesToCard, secondCardPosition);
                    RoundManager.instance.largeCardView2.gameObject.SetActive(true);

                    if (!string.IsNullOrEmpty(evolvesToCard.evolvesTo))
                    {
                        MonsterCard evolvesTo2Card = new MonsterCard(evolvesToCard.evolvesTo);
                        RoundManager.instance.largeCardView3.SetCard(evolvesTo2Card, thirdCardPosition);
                        RoundManager.instance.largeCardView3.gameObject.SetActive(true);
                    }
                }
                else if (!string.IsNullOrEmpty(evolvesFrom))
                {
                    MonsterCard evolvesFromCard = new MonsterCard(evolvesFrom);

                    RoundManager.instance.largeCardView1.SetCard(evolvesFromCard, firstCardPosition);
                    RoundManager.instance.largeCardView1.gameObject.SetActive(true);

                    RoundManager.instance.largeCardView2.SetCard(currentCard, secondCardPosition);
                    RoundManager.instance.largeCardView2.gameObject.SetActive(true);

                    if (!string.IsNullOrEmpty(evolvesFromCard.evolvesFrom))
                    {
                        MonsterCard evolvesFrom2Card = new MonsterCard(evolvesFromCard.evolvesFrom);
                        RoundManager.instance.largeCardView3.SetCard(evolvesFrom2Card, thirdCardPosition);
                        RoundManager.instance.largeCardView3.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RoundManager.instance.largeCardView1.gameObject.SetActive(false);
        RoundManager.instance.largeCardView2.gameObject.SetActive(false);
        RoundManager.instance.largeCardView3.gameObject.SetActive(false);
    }
}

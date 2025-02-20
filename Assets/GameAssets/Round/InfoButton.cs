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
        if (GameManager.instance.largeCardView1 != null)
        {
            SmallCardView smallCard = GetComponentInParent<SmallCardView>();
            StockPile stockPile = GetComponentInParent<StockPile>();

            if (smallCard != null && !(smallCard.card is MonsterCard))
            {
                GameManager.instance.largeCardView1.SetCard(smallCard.card, eventData.position);
                GameManager.instance.largeCardView1.gameObject.SetActive(true);
                return;
            }
            else if (stockPile != null && !(stockPile.card is MonsterCard))
            {
                GameManager.instance.largeCardView1.SetCard(stockPile.card, eventData.position);
                GameManager.instance.largeCardView1.gameObject.SetActive(true);
                return;
            }
            

            MonsterCard currentCard = null;
            string evolvesFrom = null;
            string evolvesTo = null;

            if (smallCard != null)
            {
                currentCard = new MonsterCard(smallCard.card.Name, GameManager.instance.cardLevels[smallCard.card.Name]);
                evolvesFrom = currentCard.evolvesFrom;
                evolvesTo = currentCard.evolvesTo;
            }
            else if (stockPile != null)
            {
                currentCard = new MonsterCard(stockPile.card.Name, GameManager.instance.cardLevels[stockPile.card.Name]);
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
                    GameManager.instance.largeCardView1.SetCard(currentCard, eventData.position);
                    GameManager.instance.largeCardView1.gameObject.SetActive(true);
                    return;
                }

                if (!string.IsNullOrEmpty(evolvesTo) && !string.IsNullOrEmpty(evolvesFrom))
                {
                    MonsterCard evolvesToCard = new MonsterCard(evolvesTo, GameManager.instance.cardLevels[evolvesTo]);
                    MonsterCard evolvesFromCard = new MonsterCard(evolvesFrom, GameManager.instance.cardLevels[evolvesFrom]);

                    GameManager.instance.largeCardView1.SetCard(evolvesFromCard, firstCardPosition);
                    GameManager.instance.largeCardView1.gameObject.SetActive(true);

                    GameManager.instance.largeCardView2.SetCard(currentCard, secondCardPosition);
                    GameManager.instance.largeCardView2.gameObject.SetActive(true);

                    GameManager.instance.largeCardView3.SetCard(evolvesToCard, thirdCardPosition);
                    GameManager.instance.largeCardView3.gameObject.SetActive(true);
                    return;
                }

                if (!string.IsNullOrEmpty(evolvesTo))
                {
                    MonsterCard evolvesToCard = new MonsterCard(evolvesTo, GameManager.instance.cardLevels[evolvesTo]);

                    GameManager.instance.largeCardView1.SetCard(currentCard, firstCardPosition);
                    GameManager.instance.largeCardView1.gameObject.SetActive(true);

                    GameManager.instance.largeCardView2.SetCard(evolvesToCard, secondCardPosition);
                    GameManager.instance.largeCardView2.gameObject.SetActive(true);

                    if (!string.IsNullOrEmpty(evolvesToCard.evolvesTo))
                    {
                        MonsterCard evolvesTo2Card = new MonsterCard(evolvesToCard.evolvesTo, GameManager.instance.cardLevels[evolvesToCard.evolvesTo]);
                        GameManager.instance.largeCardView3.SetCard(evolvesTo2Card, thirdCardPosition);
                        GameManager.instance.largeCardView3.gameObject.SetActive(true);
                    }
                }
                else if (!string.IsNullOrEmpty(evolvesFrom))
                {
                    MonsterCard evolvesFromCard = new MonsterCard(evolvesFrom, GameManager.instance.cardLevels[evolvesFrom]);

                    GameManager.instance.largeCardView1.SetCard(evolvesFromCard, firstCardPosition);
                    GameManager.instance.largeCardView1.gameObject.SetActive(true);

                    GameManager.instance.largeCardView2.SetCard(currentCard, secondCardPosition);
                    GameManager.instance.largeCardView2.gameObject.SetActive(true);

                    if (!string.IsNullOrEmpty(evolvesFromCard.evolvesFrom))
                    {
                        MonsterCard evolvesFrom2Card = new MonsterCard(evolvesFromCard.evolvesFrom, GameManager.instance.cardLevels[evolvesFromCard.evolvesFrom]);
                        GameManager.instance.largeCardView3.SetCard(evolvesFrom2Card, thirdCardPosition);
                        GameManager.instance.largeCardView3.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.instance.largeCardView1.gameObject.SetActive(false);
        GameManager.instance.largeCardView2.gameObject.SetActive(false);
        GameManager.instance.largeCardView3.gameObject.SetActive(false);
    }
}

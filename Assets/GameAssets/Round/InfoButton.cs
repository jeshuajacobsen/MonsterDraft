using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InfoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameManager _gameManager;
    CardFactory _cardFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, CardFactory cardFactory, DiContainer container)
    {
        _gameManager = gameManager;
        _cardFactory = cardFactory;
        _container = container;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_gameManager.largeCardView1 != null)
        {
            SmallCardView smallCard = GetComponentInParent<SmallCardView>();
            StockPile stockPile = GetComponentInParent<StockPile>();

            if (smallCard != null && !(smallCard.card is MonsterCard))
            {
                _gameManager.largeCardView1.SetCard(smallCard.card, eventData.position);
                _gameManager.largeCardView1.gameObject.SetActive(true);
                return;
            }
            else if (stockPile != null && !(stockPile.card is MonsterCard))
            {
                _gameManager.largeCardView1.SetCard(stockPile.card, eventData.position);
                _gameManager.largeCardView1.gameObject.SetActive(true);
                return;
            }

            MonsterCard currentCard = null;
            string evolvesFrom = null;
            string evolvesTo = null;

            if (smallCard != null)
            {
                currentCard = (MonsterCard)_cardFactory.CreateCard(smallCard.card.Name, _gameManager.cardLevels[smallCard.card.Name]);
                evolvesFrom = currentCard.evolvesFrom;
                evolvesTo = currentCard.evolvesTo;
            }
            else if (stockPile != null)
            {
                currentCard = (MonsterCard)_cardFactory.CreateCard(stockPile.card.Name, _gameManager.cardLevels[stockPile.card.Name]);
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
                    _gameManager.largeCardView1.SetCard(currentCard, eventData.position);
                    _gameManager.largeCardView1.gameObject.SetActive(true);
                    return;
                }

                if (!string.IsNullOrEmpty(evolvesTo) && !string.IsNullOrEmpty(evolvesFrom))
                {
                    MonsterCard evolvesToCard = 
                        _cardFactory.CreateCard(evolvesTo, _gameManager.cardLevels[evolvesTo]) as MonsterCard;

                    MonsterCard evolvesFromCard = 
                        _cardFactory.CreateCard(evolvesFrom, _gameManager.cardLevels[evolvesFrom]) as MonsterCard;

                    _gameManager.largeCardView1.SetCard(evolvesFromCard, firstCardPosition);
                    _gameManager.largeCardView1.gameObject.SetActive(true);

                    _gameManager.largeCardView2.SetCard(currentCard, secondCardPosition);
                    _gameManager.largeCardView2.gameObject.SetActive(true);

                    _gameManager.largeCardView3.SetCard(evolvesToCard, thirdCardPosition);
                    _gameManager.largeCardView3.gameObject.SetActive(true);
                    return;
                }

                if (!string.IsNullOrEmpty(evolvesTo))
                {
                    MonsterCard evolvesToCard = _cardFactory.CreateCard(evolvesTo, _gameManager.cardLevels[evolvesTo]) as MonsterCard;

                    _gameManager.largeCardView1.SetCard(currentCard, firstCardPosition);
                    _gameManager.largeCardView1.gameObject.SetActive(true);

                    _gameManager.largeCardView2.SetCard(evolvesToCard, secondCardPosition);
                    _gameManager.largeCardView2.gameObject.SetActive(true);

                    if (!string.IsNullOrEmpty(evolvesToCard.evolvesTo))
                    {
                        MonsterCard evolvesTo2Card = _cardFactory.CreateCard(
                            evolvesToCard.evolvesTo, 
                            _gameManager.cardLevels[evolvesToCard.evolvesTo]) as MonsterCard;
                        _gameManager.largeCardView3.SetCard(evolvesTo2Card, thirdCardPosition);
                        _gameManager.largeCardView3.gameObject.SetActive(true);
                    }
                }
                else if (!string.IsNullOrEmpty(evolvesFrom))
                {
                    MonsterCard evolvesFromCard = 
                        _cardFactory.CreateCard(evolvesFrom, _gameManager.cardLevels[evolvesFrom]) as MonsterCard;

                    _gameManager.largeCardView1.SetCard(evolvesFromCard, firstCardPosition);
                    _gameManager.largeCardView1.gameObject.SetActive(true);

                    _gameManager.largeCardView2.SetCard(currentCard, secondCardPosition);
                    _gameManager.largeCardView2.gameObject.SetActive(true);

                    if (!string.IsNullOrEmpty(evolvesFromCard.evolvesFrom))
                    {
                        MonsterCard evolvesFrom2Card = _cardFactory.CreateCard(
                            evolvesFromCard.evolvesFrom, 
                            _gameManager.cardLevels[evolvesFromCard.evolvesFrom]) as MonsterCard;
                        _gameManager.largeCardView3.SetCard(evolvesFrom2Card, thirdCardPosition);
                        _gameManager.largeCardView3.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _gameManager.largeCardView1.gameObject.SetActive(false);
        _gameManager.largeCardView2.gameObject.SetActive(false);
        _gameManager.largeCardView3.gameObject.SetActive(false);
    }
}

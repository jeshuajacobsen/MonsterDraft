using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MonsterInfoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RoundManager.instance.largeMonsterView1 != null)
        {
            Monster monster = GetComponentInParent<Monster>();
            BaseMonsterData currentMonster = _gameManager.gameData.GetBaseMonsterData(monster.name);
            Vector2 firstCardPosition = new Vector2(-1100, -250);
            Vector2 secondCardPosition = new Vector2(0, -250);
            Vector2 thirdCardPosition = new Vector2(1100, -250);

            if (string.IsNullOrEmpty(monster.evolvesFrom) && string.IsNullOrEmpty(monster.evolvesTo))
            {
                RoundManager.instance.largeMonsterView1.SetMonster(monster, eventData.position);
                RoundManager.instance.largeMonsterView1.gameObject.SetActive(true);
                return;
            }

            if (!string.IsNullOrEmpty(monster.evolvesTo) && !string.IsNullOrEmpty(monster.evolvesFrom))
            {
                BaseMonsterData evolvesTo = _gameManager.gameData.GetBaseMonsterData(monster.evolvesTo);
                BaseMonsterData evolvesFrom = _gameManager.gameData.GetBaseMonsterData(monster.evolvesFrom);
                
                RoundManager.instance.largeMonsterView1.SetMonsterFromBaseData(evolvesFrom, firstCardPosition);
                RoundManager.instance.largeMonsterView1.gameObject.SetActive(true);
                RoundManager.instance.largeMonsterView2.SetMonster(monster, secondCardPosition, false);
                RoundManager.instance.largeMonsterView2.gameObject.SetActive(true);
                RoundManager.instance.largeMonsterView3.SetMonsterFromBaseData(evolvesTo, thirdCardPosition);
                RoundManager.instance.largeMonsterView3.gameObject.SetActive(true);
                return;
            }
            
            if (!string.IsNullOrEmpty(monster.evolvesTo))
            {
                BaseMonsterData evolvesTo = _gameManager.gameData.GetBaseMonsterData(monster.evolvesTo);
                RoundManager.instance.largeMonsterView1.SetMonster(monster, firstCardPosition, false);
                RoundManager.instance.largeMonsterView1.gameObject.SetActive(true);
                RoundManager.instance.largeMonsterView2.SetMonsterFromBaseData(evolvesTo, secondCardPosition);
                RoundManager.instance.largeMonsterView2.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(evolvesTo.evolvesTo))
                {
                    BaseMonsterData evolvesTo2 = _gameManager.gameData.GetBaseMonsterData(evolvesTo.evolvesTo);
                    RoundManager.instance.largeMonsterView3.SetMonsterFromBaseData(evolvesTo2, thirdCardPosition);
                    RoundManager.instance.largeMonsterView3.gameObject.SetActive(true);
                }
            }
            else if (!string.IsNullOrEmpty(monster.evolvesFrom))
            {
                BaseMonsterData evolvesFrom = _gameManager.gameData.GetBaseMonsterData(monster.evolvesFrom);
                RoundManager.instance.largeMonsterView1.SetMonsterFromBaseData(evolvesFrom, firstCardPosition);
                RoundManager.instance.largeMonsterView1.gameObject.SetActive(true);
                RoundManager.instance.largeMonsterView2.SetMonster(monster, secondCardPosition, false);
                RoundManager.instance.largeMonsterView2.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(evolvesFrom.evolvesFrom))
                {
                    BaseMonsterData evolvesFrom2 = _gameManager.gameData.GetBaseMonsterData(evolvesFrom.evolvesFrom);
                    RoundManager.instance.largeMonsterView3.SetMonsterFromBaseData(evolvesFrom2, thirdCardPosition);
                    RoundManager.instance.largeMonsterView3.gameObject.SetActive(true);
                }
            }
            
            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RoundManager.instance.largeMonsterView1.gameObject.SetActive(false);
        RoundManager.instance.largeMonsterView2.gameObject.SetActive(false);
        RoundManager.instance.largeMonsterView3.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterInfoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        if (RoundManager.instance.largeMonsterView != null)
        {
            Monster monster = GetComponentInParent<Monster>();

            RoundManager.instance.largeMonsterView.SetMonster(monster);
            RoundManager.instance.largeMonsterView.gameObject.SetActive(true);
            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RoundManager.instance.largeMonsterView.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MonsterInfoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RoundUIManager _uiManager;

    [Inject]
    public void Construct(GameManager gameManager, RoundManager roundManager, RoundUIManager uiManager)
    {
        _uiManager = uiManager;
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
        _uiManager.OpenMonsterInfo(GetComponentInParent<Monster>(), eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _uiManager.CloseMonsterInfoPanel();
    }
}

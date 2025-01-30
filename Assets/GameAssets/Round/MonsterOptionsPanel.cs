using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOptionsPanel : MonoBehaviour
{
    private Tile activeTile;

    [SerializeField] private MonsterOptionButton monsterOptionButtonPrefab;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetActiveTile(Tile tile, Vector2 pointerPosition)
    {
        this.activeTile = tile;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        // MonsterOptionButton optionButton = Instantiate(monsterOptionButtonPrefab, transform);
        // optionButton.InitValues(activeTile.monster, "Movement");


        // if (activeTile.monster.team == "Enemy" || activeTile.monster.actionsUsedThisTurn.Contains("Movement") || !RoundManager.instance.CanMoveMonster(activeTile.monster))
        // {
        //     Debug.Log("Cant move");
        //     optionButton.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        // }

        MonsterOptionButton optionButton = Instantiate(monsterOptionButtonPrefab, transform);
        optionButton.InitValues(activeTile.monster, "Skill1");
        if (activeTile.monster.team == "Enemy")
        {
            Debug.Log("Skill1 used");
            optionButton.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        optionButton = Instantiate(monsterOptionButtonPrefab, transform);
        optionButton.InitValues(activeTile.monster, "Skill2");
        if (activeTile.monster.team == "Enemy")
        {
            optionButton.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        float buttonHeight = monsterOptionButtonPrefab.GetComponent<RectTransform>().rect.height;
        float buttonWidth = monsterOptionButtonPrefab.GetComponent<RectTransform>().rect.width;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(buttonWidth, buttonHeight * 2);
        RectTransform parentRect = rectTransform.parent as RectTransform;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            pointerPosition,
            Camera.main, 
            out Vector2 localPoint))
        {
            localPoint.x += buttonWidth / 2;
            localPoint.y -= buttonHeight;
            rectTransform.anchoredPosition = localPoint;
        }
    }
}

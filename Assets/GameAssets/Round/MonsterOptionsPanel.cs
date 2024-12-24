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

    public void SetActiveTile(Tile tile)
    {
        this.activeTile = tile;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        MonsterOptionButton optionButton = Instantiate(monsterOptionButtonPrefab, transform);
        optionButton.InitValues(activeTile.monster, "Movement");


        if (activeTile.monster.team == "Enemy" || activeTile.monster.actionsUsedThisTurn.Contains("Movement") || !RoundManager.instance.CanMoveMonster(activeTile.monster))
        {
            Debug.Log("Cant move");
            optionButton.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        optionButton = Instantiate(monsterOptionButtonPrefab, transform);
        optionButton.InitValues(activeTile.monster, "Skill1");
        if (activeTile.monster.team == "Enemy" || 
            activeTile.monster.actionsUsedThisTurn.Contains(activeTile.monster.skill1.name) || 
            activeTile.monster.actionsUsedThisTurn.Contains(activeTile.monster.skill2.name))
        {
            Debug.Log("Skill1 used");
            optionButton.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        optionButton = Instantiate(monsterOptionButtonPrefab, transform);
        optionButton.InitValues(activeTile.monster, "Skill2");
        if (activeTile.monster.team == "Enemy" || 
            activeTile.monster.actionsUsedThisTurn.Contains(activeTile.monster.skill2.name) ||
            activeTile.monster.actionsUsedThisTurn.Contains(activeTile.monster.skill1.name))
        {
            optionButton.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
}

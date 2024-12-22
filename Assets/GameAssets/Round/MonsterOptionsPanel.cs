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
        if (activeTile.monster.actionsUsedThisTurn.Contains("Movement"))
        {
            optionButton.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
}

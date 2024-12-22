using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownPanel : MonoBehaviour
{

    public StockPile stockPilePrefab;
    public List<StockPile> stockPiles;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void StartRound()
    {
        stockPiles.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        StockPile stockPile = Instantiate(stockPilePrefab, transform);
        stockPile.InitValues("Copper", 10, "Treasure");
        stockPiles.Add(stockPile);

        stockPile = Instantiate(stockPilePrefab, transform);
        stockPile.InitValues("Silver", 10, "Treasure");
        stockPiles.Add(stockPile);

        stockPile = Instantiate(stockPilePrefab, transform);
        stockPile.InitValues("Mana Vial", 10, "Treasure");
        stockPiles.Add(stockPile);

        List<string> treasureNames = new List<string> { "Copper", "Silver", "Mana Vial" };

        stockPile = Instantiate(stockPilePrefab, transform);
        stockPile.InitValues(GameManager.instance.gameData.GetRandomTreasureName(treasureNames), 10, "Treasure");
        stockPiles.Add(stockPile);

        List<string> monsterNames = new List<string>();
        for(int i = 0; i <= 3; i++)
        {
            stockPile = Instantiate(stockPilePrefab, transform);
            stockPile.InitValues(GameManager.instance.gameData.GetRandomMonsterName(monsterNames), 10, "Monster");
            monsterNames.Add(stockPile.Name);
            stockPiles.Add(stockPile);
        }

        List<string> actionNames = new List<string>();
        for(int i = 0; i <= 2; i++)
        {
            stockPile = Instantiate(stockPilePrefab, transform);
            stockPile.InitValues(GameManager.instance.gameData.GetRandomActionName(actionNames), 10, "Action");
            actionNames.Add(stockPile.Name);
            stockPiles.Add(stockPile);
        }

        SortAndReorderStockpiles();
    }

    private void SortAndReorderStockpiles()
    {
        stockPiles.Sort((b1, b2) => b1.Cost.CompareTo(b2.Cost));

        for (int i = 0; i < stockPiles.Count; i++)
        {
            stockPiles[i].transform.SetSiblingIndex(i);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
    }
}

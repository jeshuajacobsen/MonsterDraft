using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownPanel : MonoBehaviour
{

    public StockPile stockPilePrefab;
    public StockPile basicStockPilePrefab;
    public List<StockPile> stockPiles;
    public List<StockPile> basicStockPiles;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void StartRound(List<string> guaranteedCards)
    {
        stockPiles.Clear();
        
        Transform basicGoodsTransform = transform.Find("BasicGoodsPanel");
        foreach (Transform child in basicGoodsTransform)
        {
            Destroy(child.gameObject);
        }
        Transform mainGoodsTransform = transform.Find("MainGoodsPanel");
        foreach (Transform child in mainGoodsTransform)
        {
            Destroy(child.gameObject);
        }
        List<string> treasureNames = new List<string> 
            { "Copper", "Silver", "Gold", "Platinum", "Mana Vial", "Mana Potion", "Mana Crystal", "Mana Gem" };
        StockPile stockPile = Instantiate(basicStockPilePrefab, basicGoodsTransform);
        stockPile.InitValues("Copper", 10, "Treasure");
        basicStockPiles.Add(stockPile);

        stockPile = Instantiate(basicStockPilePrefab, basicGoodsTransform);
        stockPile.InitValues("Mana Vial", 10, "Treasure");
        basicStockPiles.Add(stockPile);

        stockPile = Instantiate(basicStockPilePrefab, basicGoodsTransform);
        stockPile.InitValues("Silver", 10, "Treasure");
        basicStockPiles.Add(stockPile);

        stockPile = Instantiate(basicStockPilePrefab, basicGoodsTransform);
        stockPile.InitValues("Mana Potion", 10, "Treasure");
        basicStockPiles.Add(stockPile);

        stockPile = Instantiate(basicStockPilePrefab, basicGoodsTransform);
        stockPile.InitValues("Gold", 10, "Treasure");
        basicStockPiles.Add(stockPile);

        stockPile = Instantiate(basicStockPilePrefab, basicGoodsTransform);
        stockPile.InitValues("Mana Crystal", 10, "Treasure");
        basicStockPiles.Add(stockPile);

        stockPile = Instantiate(basicStockPilePrefab, basicGoodsTransform);
        stockPile.InitValues("Platinum", 10, "Treasure");
        basicStockPiles.Add(stockPile);

        stockPile = Instantiate(basicStockPilePrefab, basicGoodsTransform);
        stockPile.InitValues("Mana Gem", 10, "Treasure");
        basicStockPiles.Add(stockPile);

        

        //List<string> treasureNames = new List<string> { "Copper", "Silver", "Mana Vial" };

        // stockPile = Instantiate(stockPilePrefab, transform);
        // stockPile.InitValues(GameManager.instance.gameData.GetRandomTreasureName(treasureNames), 10, "Treasure");
        // treasureNames.Add(stockPile.Name);
        // stockPiles.Add(stockPile);

        List<string> monsterNames = new List<string>();

        stockPile = Instantiate(stockPilePrefab, mainGoodsTransform);
        stockPile.InitValues(GameManager.instance.gameData.GetRandomMonsterName(monsterNames, "Common"), 10, "Monster");
        monsterNames.Add(stockPile.Name);
        stockPiles.Add(stockPile);

        stockPile = Instantiate(stockPilePrefab, mainGoodsTransform);
        stockPile.InitValues(GameManager.instance.gameData.GetRandomMonsterName(monsterNames, "Uncommon"), 10, "Monster");
        monsterNames.Add(stockPile.Name);
        stockPiles.Add(stockPile);

        stockPile = Instantiate(stockPilePrefab, mainGoodsTransform);
        stockPile.InitValues(GameManager.instance.gameData.GetRandomMonsterName(monsterNames, "Rare"), 10, "Monster");
        monsterNames.Add(stockPile.Name);
        stockPiles.Add(stockPile);

        stockPile = Instantiate(stockPilePrefab, mainGoodsTransform);
        stockPile.InitValues(GameManager.instance.gameData.GetRandomMonsterName(monsterNames, "Epic"), 10, "Monster");
        monsterNames.Add(stockPile.Name);
        stockPiles.Add(stockPile);

        List<string> actionNames = new List<string>();
        foreach (string cardName in guaranteedCards)
        {
            stockPile = Instantiate(stockPilePrefab, mainGoodsTransform);
            stockPile.InitValues(cardName, 10, GameManager.instance.gameData.GetCardType(cardName));
            actionNames.Add(stockPile.Name);
            stockPiles.Add(stockPile);
        }
        List<string> combinedNames = new List<string>(actionNames);
        combinedNames.AddRange(treasureNames);
        for(int i = 0; i <= 5 - guaranteedCards.Count; i++)
        {
            stockPile = Instantiate(stockPilePrefab, mainGoodsTransform);
            
            string name = GameManager.instance.gameData.GetRandomActionOrTreasureName(combinedNames);
            stockPile.InitValues(
                name, 
                10, 
                GameManager.instance.gameData.GetCardType(name));
            combinedNames.Add(stockPile.Name);
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

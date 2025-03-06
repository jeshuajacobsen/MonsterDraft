using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TownPanel : MonoBehaviour
{
    public StockPile stockPilePrefab;
    public StockPile basicStockPilePrefab;
    public List<StockPile> stockPiles;
    public List<StockPile> basicStockPiles;

    private IGameManager _gameManager;
    private DiContainer _container;

    [Inject]
    public void Construct(IGameManager gameManager, DiContainer container)
    {
        _gameManager = gameManager;
        _container = container;
    }

    void Start() { }

    void Update() { }

    public void StartRound(List<string> guaranteedCards)
    {
        stockPiles.Clear();

        Transform basicGoodsTransform = transform.Find("BasicGoodsPanel");
        foreach (Transform child in basicGoodsTransform) Destroy(child.gameObject);

        Transform mainGoodsTransform = transform.Find("MainGoodsPanel");
        foreach (Transform child in mainGoodsTransform) Destroy(child.gameObject);

        List<string> treasureNames = new List<string> { "Copper", "Silver", "Gold", "Platinum", "Mana Vial", "Mana Potion", "Mana Crystal", "Mana Gem" };
        foreach (string treasure in treasureNames)
        {
            StockPile stockPile = _container.InstantiatePrefabForComponent<StockPile>(basicStockPilePrefab, basicGoodsTransform);
            stockPile.Initialize(treasure, 10);
            basicStockPiles.Add(stockPile);
        }

        List<string> monsterNames = new List<string>();

        stockPiles.Add(GenerateStockPile("Common", mainGoodsTransform, monsterNames, "Monster"));
        stockPiles.Add(GenerateStockPile("Uncommon", mainGoodsTransform, monsterNames, "Monster"));
        stockPiles.Add(GenerateStockPile("Rare", mainGoodsTransform, monsterNames, "Monster"));
        stockPiles.Add(GenerateStockPile("Epic", mainGoodsTransform, monsterNames, "Monster"));

        List<string> actionNames = new List<string>();
        foreach (string cardName in guaranteedCards)
        {
            StockPile stockPile = _container.InstantiatePrefabForComponent<StockPile>(stockPilePrefab, mainGoodsTransform);
            stockPile.Initialize(cardName, 10);
            actionNames.Add(stockPile.Name);
            stockPiles.Add(stockPile);
        }

        List<string> combinedNames = new List<string>(actionNames);
        combinedNames.AddRange(treasureNames);

        for (int i = 0; i <= 5 - guaranteedCards.Count; i++)
        {
            StockPile stockPile = _container.InstantiatePrefabForComponent<StockPile>(stockPilePrefab, mainGoodsTransform);
            string name = _gameManager.GameData.GetRandomActionOrTreasureName(combinedNames);
            stockPile.Initialize(name, 10);
            combinedNames.Add(stockPile.Name);
            stockPiles.Add(stockPile);
        }

        SortAndReorderStockpiles();
    }

    private StockPile GenerateStockPile(string rarity, Transform parent, List<string> existingNames, string type)
    {
        string name = _gameManager.GameData.GetRandomMonsterName(existingNames, rarity);
        StockPile stockPile = _container.InstantiatePrefabForComponent<StockPile>(stockPilePrefab, parent);
        stockPile.Initialize(name, 10);
        existingNames.Add(name);
        return stockPile;
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

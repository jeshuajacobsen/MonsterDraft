using System.Collections.Generic;

using UnityEngine;

public class GameData 
{
    private Dictionary<string, BaseStatsData> _baseStatsData;
    private Dictionary<string, BaseActionData> _actionData;
    private Dictionary<string, TreasureData> _treasureData;

    public GameData()
    {
        _baseStatsData = new Dictionary<string, BaseStatsData>();
        _baseStatsData.Add("Borble", new BaseStatsData("Borble"));
        _baseStatsData.Add("Leafree", new BaseStatsData("Leafree"));
        _baseStatsData.Add("Owisp", new BaseStatsData("Owisp"));
        _baseStatsData.Add("Zaple", new BaseStatsData("Zaple"));

        _actionData = new Dictionary<string, BaseActionData>();
        _actionData.Add("Fireball", new BaseActionData("Fireball"));
        _actionData.Add("Heal", new BaseActionData("Heal"));
        _actionData.Add("Shield", new BaseActionData("Shield"));

        _treasureData = new Dictionary<string, TreasureData>();
        _treasureData.Add("Copper", new TreasureData("Copper"));
        _treasureData.Add("Silver", new TreasureData("Silver"));
        _treasureData.Add("Gold", new TreasureData("Gold"));
        _treasureData.Add("Mana Vial", new TreasureData("Mana Vial"));
        _treasureData.Add("Mana Crystal", new TreasureData("Mana Crystal"));
        _treasureData.Add("Mana Gem", new TreasureData("Mana Gem"));
    }

    public BaseStatsData GetBaseStatsData(string name)
    {
        return _baseStatsData[name];
    }

    public BaseActionData GetActionData(string name)
    {
        return _actionData[name];
    }

    public TreasureData GetTreasureData(string name)
    {
        return _treasureData[name];
    }

    public string GetRandomTreasureName(List<string> exclude)
    {
        List<string> treasureNames = new List<string> { "Copper", "Silver", "Gold", "Mana Vial", "Mana Crystal", "Mana Gem" };
        treasureNames.RemoveAll(exclude.Contains);
        return treasureNames[Random.Range(0, treasureNames.Count)];
    }

    public string GetRandomMonsterName(List<string> exclude)
    {
        List<string> monsterNames = new List<string> { "Borble", "Leafree", "Owisp", "Zaple" };
        monsterNames.RemoveAll(exclude.Contains);
        return monsterNames[Random.Range(0, monsterNames.Count)];
    }

    public string GetRandomActionName(List<string> exclude)
    {
        List<string> actionNames = new List<string> { "Fireball", "Heal", "Shield" };
        actionNames.RemoveAll(exclude.Contains);
        if (actionNames.Count == 0)
        {
            return "";
        }
        return actionNames[Random.Range(0, actionNames.Count)];
    }
}
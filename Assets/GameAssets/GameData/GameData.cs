using System.Collections.Generic;

using UnityEngine;

public class GameData 
{
    private Dictionary<string, BaseStatsData> _baseStatsData;
    private Dictionary<string, BaseActionData> _actionData;
    private Dictionary<string, TreasureData> _treasureData;

    private Dictionary<string, SkillData> _skills;
    private Dictionary<string, List<string>> DungeonData;

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
        _actionData.Add("Preparation", new BaseActionData("Preparation"));
        _actionData.Add("Research", new BaseActionData("Research"));
        _actionData.Add("Storage", new BaseActionData("Storage"));
        _actionData.Add("Alchemist", new BaseActionData("Alchemist"));
        _actionData.Add("Merchant", new BaseActionData("Merchant"));
        _actionData.Add("Throne Room", new BaseActionData("Throne Room"));

        _treasureData = new Dictionary<string, TreasureData>();
        _treasureData.Add("Copper", new TreasureData("Copper"));
        _treasureData.Add("Silver", new TreasureData("Silver"));
        _treasureData.Add("Gold", new TreasureData("Gold"));
        _treasureData.Add("Platinum", new TreasureData("Platinum"));
        _treasureData.Add("Mana Vial", new TreasureData("Mana Vial"));
        _treasureData.Add("Mana Potion", new TreasureData("Mana Potion"));
        _treasureData.Add("Mana Crystal", new TreasureData("Mana Crystal"));
        _treasureData.Add("Mana Gem", new TreasureData("Mana Gem"));

        _skills = new Dictionary<string, SkillData>();
        _skills.Add("Zap", new SkillData("Zap"));
        _skills.Add("Bubble", new SkillData("Bubble"));
        _skills.Add("Leaf", new SkillData("Leaf"));
        _skills.Add("Spark", new SkillData("Spark"));
        _skills.Add("Shock", new SkillData("Shock"));
        _skills.Add("Burn", new SkillData("Burn"));
        _skills.Add("Wave", new SkillData("Wave"));
        _skills.Add("Growth", new SkillData("Growth"));

        DungeonData = new Dictionary<string, List<string>>();
        DungeonData.Add("Dungeon1", new List<string> { "Zaple", "Owisp", "Leafree", "Borble" });
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

    public SkillData GetSkill(string name)
    {
        return _skills[name];
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
        List<string> actionNames = new List<string> { "Fireball", "Heal", "Shield", "Preparation", 
                                                      "Research", "Storage", "Alchemist", "Merchant", 
                                                      "Throne Room" };
        actionNames.RemoveAll(exclude.Contains);
        if (actionNames.Count == 0)
        {
            return "";
        }
        return actionNames[Random.Range(0, actionNames.Count)];
    }

    public List<string> GetDungeonMonsters(string name)
    {
        return DungeonData[name];
    }
}
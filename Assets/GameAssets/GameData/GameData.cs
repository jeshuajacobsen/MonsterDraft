using System.Collections.Generic;

using UnityEngine;
using System.Linq;

public class GameData 
{
    private Dictionary<string, BaseMonsterData> _baseMonsterData;
    private Dictionary<string, BaseActionData> _actionData;
    private Dictionary<string, TreasureData> _treasureData;

    private Dictionary<string, SkillData> _skills;
    private Dictionary<string, DungeonLevelData> _dungeonData;

    public GameData()
    {
        _baseMonsterData = new Dictionary<string, BaseMonsterData>
        {
            { "Borble", new BaseMonsterData("Borble") },
            { "Pupal", new BaseMonsterData("Pupal") },
            { "Aquafly", new BaseMonsterData("Aquafly") },
            { "Leafree", new BaseMonsterData("Leafree") },
            { "Leafear", new BaseMonsterData("Leafear") },
            { "Olla", new BaseMonsterData("Olla") },
            { "Owisp", new BaseMonsterData("Owisp") },
            { "Wallowisp", new BaseMonsterData("Wallowisp") },
            { "Slimy", new BaseMonsterData("Slimy") },
            { "Slimier", new BaseMonsterData("Slimier") },
            { "Slimiest", new BaseMonsterData("Slimiest") },
            { "Snowbug", new BaseMonsterData("Snowbug") },
            { "Snant", new BaseMonsterData("Snant") },
            { "Snowpede", new BaseMonsterData("Snowpede") },
            { "Squrl", new BaseMonsterData("Squrl") },
            { "Squrile", new BaseMonsterData("Squrile") },
            { "Zaple", new BaseMonsterData("Zaple") },
            { "Lightna", new BaseMonsterData("Lightna") },
            { "Thunda", new BaseMonsterData("Thunda") }
        };


        _actionData = new Dictionary<string, BaseActionData>
        {
            { "Fireball", new BaseActionData("Fireball") },
            { "Heal", new BaseActionData("Heal") },
            { "Shield", new BaseActionData("Shield") },
            { "Preparation", new BaseActionData("Preparation") },
            { "Research", new BaseActionData("Research") },
            { "Storage", new BaseActionData("Storage") },
            { "Alchemist", new BaseActionData("Alchemist") },
            { "Merchant", new BaseActionData("Merchant") },
            { "Throne Room", new BaseActionData("Throne Room") },
            { "Forge", new BaseActionData("Forge") },
            { "Vault", new BaseActionData("Vault") },
            { "Bank", new BaseActionData("Bank") }
        };

        _treasureData = new Dictionary<string, TreasureData>
        {
            { "Copper", new TreasureData("Copper") },
            { "Silver", new TreasureData("Silver") },
            { "Gold", new TreasureData("Gold") },
            { "Platinum", new TreasureData("Platinum") },
            { "Mana Vial", new TreasureData("Mana Vial") },
            { "Mana Potion", new TreasureData("Mana Potion") },
            { "Mana Crystal", new TreasureData("Mana Crystal") },
            { "Mana Gem", new TreasureData("Mana Gem") }
        };

        _skills = new Dictionary<string, SkillData>
        {
            { "Zap", new SkillData("Zap") },
            { "Bubble", new SkillData("Bubble") },
            { "Leaf", new SkillData("Leaf") },
            { "Spark", new SkillData("Spark") },
            { "Shock", new SkillData("Shock") },
            { "Burn", new SkillData("Burn") },
            { "Wave", new SkillData("Wave") },
            { "Growth", new SkillData("Growth") }
        };

        _dungeonData = new Dictionary<string, DungeonLevelData>
        {
            { "Forest", new DungeonLevelData("Forest") }
        };
    }

    public BaseMonsterData GetBaseMonsterData(string name)
    {
        return _baseMonsterData[name];
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

    public string GetCardType(string name)
    {
        if (_baseMonsterData.ContainsKey(name))
        {
            return "Monster";
        }
        else if (_actionData.ContainsKey(name))
        {
            return "Action";
        }
        else if (_treasureData.ContainsKey(name))
        {
            return "Treasure";
        }
        return "";
    }

    public DungeonLevelData DungeonData(string name)
    {
        return _dungeonData[name];
    }

    public string GetRandomTreasureName(List<string> exclude)
    {
        List<string> treasureNames = _treasureData.Keys.ToList();
        treasureNames.RemoveAll(exclude.Contains);
        return treasureNames[Random.Range(0, treasureNames.Count)];
    }

    public string GetRandomMonsterName(List<string> exclude, string rarity)
    {
        List<string> monsterNames = _baseMonsterData.Keys.ToList();
        monsterNames.RemoveAll(exclude.Contains);
        monsterNames.RemoveAll(name => !string.IsNullOrEmpty(_baseMonsterData[name].evolvesFrom));
        monsterNames.RemoveAll(name => _baseMonsterData[name].rarity != rarity);
        return monsterNames[Random.Range(0, monsterNames.Count)];
    }

    public string GetRandomActionName(List<string> exclude)
    {
        List<string> actionNames = _actionData.Keys.ToList();
        actionNames.RemoveAll(exclude.Contains);
        if (actionNames.Count == 0)
        {
            return "";
        }
        return actionNames[Random.Range(0, actionNames.Count)];
    }
}
using System.Collections.Generic;

using UnityEngine;
using System.Linq;
using Zenject;

public class GameData : IGameData
{
    private Dictionary<string, BaseMonsterData> _baseMonsterData;
    private Dictionary<string, BaseActionData> _actionData;
    private Dictionary<string, TreasureData> _treasureData;

    private Dictionary<string, SkillData> _skills;
    private Dictionary<string, DungeonLevelData> _dungeonData;
    private Dictionary<string, int> _availableDeckEditorCards;
    public Dictionary<string, int> AvailableDeckEditorCards => _availableDeckEditorCards;

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public GameData Initialize()
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
            { "Bank", new BaseActionData("Bank") },
            { "Development", new BaseActionData("Development") },
            { "Inspiration", new BaseActionData("Inspiration") },
            { "Greater Fireball", new BaseActionData("Greater Fireball") },
            { "Mana Burst", new BaseActionData("Mana Burst") },
            { "Provisions", new BaseActionData("Provisions") },
            { "Fury", new BaseActionData("Fury") },
            { "Annihilate", new BaseActionData("Annihilate") },
            { "Summon", new BaseActionData("Summon") }
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
            { "Mana Gem", new TreasureData("Mana Gem") },
            { "Bauble", new TreasureData("Bauble") },
            { "Loan", new TreasureData("Loan") },
            { "Investments", new TreasureData("Investments") },
            { "Evolution Stone", new TreasureData("Evolution Stone") }
        };

        _skills = new Dictionary<string, SkillData>
        {
            //Basic
            { "Zap", _container.Instantiate<SkillData>().Initialize("Zap") },
            { "Bubble", _container.Instantiate<SkillData>().Initialize("Bubble") },
            { "Leaf", _container.Instantiate<SkillData>().Initialize("Leaf") },
            { "Spark", _container.Instantiate<SkillData>().Initialize("Spark") },
            { "Wrap", _container.Instantiate<SkillData>().Initialize("Wrap") },
            { "Goo", _container.Instantiate<SkillData>().Initialize("Goo") },
            { "Chill", _container.Instantiate<SkillData>().Initialize("Chill") },
            { "Bite", _container.Instantiate<SkillData>().Initialize("Bite") },

            //Intermediate
            { "Shock", _container.Instantiate<SkillData>().Initialize("Shock") },
            { "Burn", _container.Instantiate<SkillData>().Initialize("Burn") },
            { "Wave", _container.Instantiate<SkillData>().Initialize("Wave") },
            { "Growth", _container.Instantiate<SkillData>().Initialize("Growth") },
            { "Poison Sting", _container.Instantiate<SkillData>().Initialize("Poison Sting") },
            { "Slime Ball", _container.Instantiate<SkillData>().Initialize("Slime Ball") },
            { "Ice Shard", _container.Instantiate<SkillData>().Initialize("Ice Shard") },
            { "Drain", _container.Instantiate<SkillData>().Initialize("Drain") },

            //Advanced
            { "Lightning", _container.Instantiate<SkillData>().Initialize("Lightning") },
            { "Heat Wave", _container.Instantiate<SkillData>().Initialize("Heat Wave") },
            { "Solar Beam", _container.Instantiate<SkillData>().Initialize("Solar Beam") },
            { "Water Jet", _container.Instantiate<SkillData>().Initialize("Water Jet") },
            { "Multiply", _container.Instantiate<SkillData>().Initialize("Multiply") },
            { "Freeze", _container.Instantiate<SkillData>().Initialize("Freeze") },
            { "Nightmare", _container.Instantiate<SkillData>().Initialize("Nightmare") },

            //Expert
            { "Thunder Bolt", _container.Instantiate<SkillData>().Initialize("Thunder Bolt") },
            { "Inferno", _container.Instantiate<SkillData>().Initialize("Inferno") },
            { "Poison Ivy", _container.Instantiate<SkillData>().Initialize("Poison Ivy") },
            { "Aqua Blast", _container.Instantiate<SkillData>().Initialize("Aqua Blast") },
            { "Slime Storm", _container.Instantiate<SkillData>().Initialize("Slime Storm") },
            { "Blizzard", _container.Instantiate<SkillData>().Initialize("Blizzard") }
        };


        _dungeonData = new Dictionary<string, DungeonLevelData>
        {
            { "Forest", new DungeonLevelData("Forest") },
            { "Cave", new DungeonLevelData("Cave") }
        };

        _availableDeckEditorCards = new Dictionary<string, int>
        {
            { "Copper", 10 },
            { "Silver", 10 },
            { "Gold", 10 },
            { "Platinum", 10 },
            { "Mana Vial", 10 },
            { "Mana Potion", 10 },
            { "Mana Crystal", 10 },
            { "Mana Gem", 10 },
            { "Borble", 2 },
            { "Leafree", 2 },
            { "Olla", 2 },
            { "Owisp", 2 },
            { "Slimy", 2 },
            { "Snowbug", 2 },
            { "Zaple", 2 },
            { "Squrl", 2 },
            { "Fireball", 2 },
            { "Heal", 2 },
            { "Shield", 2 }
        };

        return this;
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

    public string GetNextDungeonLevel(string name)
    {
        var keys = _dungeonData.Keys.ToList();
        int currentIndex = keys.IndexOf(name);
        if (currentIndex == -1 || currentIndex == keys.Count - 1)
        {
            return null;
        }
        return keys[currentIndex + 1];
    }

    public string GetPreviousDungeonLevel(string name)
    {
        var keys = _dungeonData.Keys.ToList();
        int currentIndex = keys.IndexOf(name);
        if (currentIndex == -1 || currentIndex == 0)
        {
            return null;
        }
        return keys[currentIndex - 1];
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

    public string GetRandomActionOrTreasureName(List<string> exclude)
    {
        List<string> names = _actionData.Keys.ToList().Concat(_treasureData.Keys.ToList()).ToList();
        names.RemoveAll(exclude.Contains);
        if (names.Count == 0)
        {
            return "";
        }
        return names[Random.Range(0, names.Count)];
    }

    public List<string> GetAllMonsterNames()
    {
        return _baseMonsterData.Keys.ToList();
    }

    public List<string> GetAllActionNames()
    {
        return _actionData.Keys.ToList();
    }

    public List<string> GetAllTreasureNames()
    {
        return _treasureData.Keys.ToList();
    }
}
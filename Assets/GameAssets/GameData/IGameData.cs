using System.Collections.Generic;

public interface IGameData
{
    Dictionary<string, int> AvailableDeckEditorCards { get; }
    BaseMonsterData GetBaseMonsterData(string name);
    BaseActionData GetActionData(string name);
    TreasureData GetTreasureData(string name);
    SkillData GetSkill(string name);
    string GetCardType(string name);
    DungeonLevelData DungeonData(string name);
    string GetNextDungeonLevel(string name);
    string GetPreviousDungeonLevel(string name);
    string GetRandomTreasureName(List<string> exclude);
    string GetRandomMonsterName(List<string> exclude, string rarity);
    string GetRandomActionName(List<string> exclude);
    string GetRandomActionOrTreasureName(List<string> exclude);
    List<string> GetAllMonsterNames();
    List<string> GetAllActionNames();
    List<string> GetAllTreasureNames();
}
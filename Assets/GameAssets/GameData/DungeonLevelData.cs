using System.Collections.Generic;

public class DungeonLevelData
{
    public string name;
    public List<string> dungeons;

    public DungeonLevelData(string name)
    {
        this.name = name;
        switch (name)
        {
            case "Forest":
                dungeons = new List<string> { "Forest1", "Forest2", "Forest3" };
                break;
            case "Cave":
                dungeons = new List<string> { "Cave1", "Cave2", "Cave3" };
                break;
        }
    }

    public DungeonData GetDungeonData(int roundNumber)
    {
        return new DungeonData(dungeons[roundNumber - 1]);
    }
}
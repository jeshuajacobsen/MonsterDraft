using System.Collections.Generic;
using UnityEngine;

public class DungeonLevelData
{
    public string name;
    public string key;
    public List<string> dungeons;
    public string selectedLevelSprite;


    public DungeonLevelData(string key)
    {
        this.key = key;
        switch (key)
        {
            case "Forest":
                dungeons = new List<string> { "Forest1", "Forest2", "Forest3", "Forest4", "Forest5" };
                this.name = "Dark Forest";
                selectedLevelSprite = "darkForest";
                break;
            case "Cave":
                dungeons = new List<string> { "Cave1", "Cave2", "Cave3", "Cave4", "Cave5" };
                this.name = "Foreboding Cave";
                selectedLevelSprite = "cave";
                break;
        }
    }

    public DungeonData GetDungeonData(int roundNumber)
    {
        return new DungeonData(dungeons[roundNumber - 1]);
    }
}
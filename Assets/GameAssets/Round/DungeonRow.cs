using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRow : MonoBehaviour
{
    public DungeonRow upRow;
    public DungeonRow downRow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile GetTile(int index)
    {
        return transform.Find("Tile" + index)?.GetComponent<Tile>();
    }

    public Tile GetNextTile(Tile tile, int distance, DungeonRow rowToUse)
    {
        int tileIndex = int.Parse(tile.name.Replace("Tile", ""));
        if (tileIndex + distance > 7)
        {
            return null;
        }
        string nextTileName = "Tile" + (tileIndex + distance);
        return rowToUse.transform.Find(nextTileName)?.GetComponent<Tile>();
    }

    public Tile GetPreviousTile(Tile tile, int distance, DungeonRow rowToUse)
    {
        int tileIndex = int.Parse(tile.name.Replace("Tile", ""));
        if (tileIndex - distance < 1)
        {
            return null;
        }
        string nextTileName = "Tile" + (tileIndex - distance);
        return tile.dungeonRow.transform.Find(nextTileName)?.GetComponent<Tile>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile GetNextTile(Tile tile, int distance)
    {
        int tileIndex = int.Parse(tile.name.Replace("Tile", ""));
        if (tileIndex + distance > 7)
        {
            return null;
        }
        string nextTileName = "Tile" + (tileIndex + distance);
        return tile.dungeonRow.transform.Find(nextTileName)?.GetComponent<Tile>();
    }

    public Tile GetPreviousTile(Tile tile, int distance)
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

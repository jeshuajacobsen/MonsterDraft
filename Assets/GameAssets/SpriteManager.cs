using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;

    [System.Serializable]
    public class SpriteEntry
    {
        public string name;
        public Sprite sprite;
    }

    public List<SpriteEntry> spriteEntries;

    public List<SpriteEntry> levelSprites;

    private Dictionary<string, Sprite> spriteDictionary;

    private Dictionary<string, Sprite> levelSpriteDictionary;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSpriteDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSpriteDictionary()
    {
        spriteDictionary = new Dictionary<string, Sprite>();
        foreach (var entry in spriteEntries)
        {
            if (!spriteDictionary.ContainsKey(entry.name))
            {
                spriteDictionary.Add(entry.name, entry.sprite);
            }
            else
            {
                Debug.LogWarning($"Duplicate sprite name: {entry.name}");
            }
        }

        levelSpriteDictionary = new Dictionary<string, Sprite>();
        foreach (var entry in levelSprites)
        {
            if (!levelSpriteDictionary.ContainsKey(entry.name))
            {
                levelSpriteDictionary.Add(entry.name, entry.sprite);
            }
            else
            {
                Debug.LogWarning($"Duplicate sprite name: {entry.name}");
            }
        }
    }

    public Sprite GetSprite(string spriteName)
    {
        if (spriteDictionary.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            Debug.LogWarning($"Sprite not found: {spriteName}");
            return null; // or a default/fallback sprite
        }
    }

    public Sprite GetLevelSprite(string level)
    {
        if (levelSpriteDictionary.TryGetValue(level, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            Debug.LogWarning($"Sprite not found: {level}");
            return null; // or a default/fallback sprite
        }
    }
}

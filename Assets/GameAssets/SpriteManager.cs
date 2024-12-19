using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Globalization;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;

    //card sprites
    public Sprite copper;
    public Sprite silver;
    public Sprite gold;

    public Sprite fireball;

    public Sprite zaple;
    public Sprite owisp;
    public Sprite leafree;
    public Sprite borble;

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sprite GetCardSprite(string spriteName)
    {
        switch (spriteName)
        {
            case "Copper":
                return copper;
            case "Silver":
                return silver;
            case "Gold":
                return gold;
            case "Fireball":
                return fireball;
            case "Zaple":
                return zaple;
            case "Owisp":
                return owisp;
            case "Leafree":
                return leafree;
            case "Borble":
                return borble;
            default:
                return copper;
        }
    }

}
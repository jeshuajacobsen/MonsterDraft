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
    public Sprite platinum;

    public Sprite manaVial;
    public Sprite manaPotion;
    public Sprite manaCrystal;
    public Sprite manaGem;

    public Sprite fireball;
    public Sprite preparation;
    public Sprite research;
    public Sprite shield;
    public Sprite heal;
    public Sprite storage;
    public Sprite alchemist;
    public Sprite merchant;
    public Sprite throneRoom;

    public Sprite zaple;
    public Sprite owisp;
    public Sprite leafree;
    public Sprite borble;


    public Sprite enemyBackground;
    public Sprite playerBackground;

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

    public Sprite GetUISprite(string spriteName)
    {
        switch (spriteName)
        {
            case "EnemyBackground":
                return enemyBackground;
            case "PlayerBackground":
                return playerBackground;
            default:
                return enemyBackground;
            
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
            case "Platinum":
                return platinum;
            case "Mana Vial":
                return manaVial;
            case "Mana Potion":
                return manaPotion;
            case "Mana Crystal":
                return manaCrystal;
            case "Mana Gem":
                return manaGem;
            case "Fireball":
                return fireball;
            case "Preparation":
                return preparation;
            case "Research":
                return research;
            case "Shield":
                return shield;
            case "Heal":
                return heal;
            case "Zaple":
                return zaple;
            case "Owisp":
                return owisp;
            case "Leafree":
                return leafree;
            case "Borble":
                return borble;
            case "Storage":
                return storage;
            case "Alchemist":
                return alchemist;
            case "Merchant":
                return merchant;
            case "Throne Room":
                return throneRoom;
            default:
                return copper;
        }
    }

}
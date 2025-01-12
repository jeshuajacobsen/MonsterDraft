using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseMonsterData
{
    public string name;
    public int Attack { get; set; }
    public int Health { get; set; }
    public int Defense { get; set; }
    public int Movement { get; set; }
    public int ManaCost { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }

    public string rarity = "Common";

    public string skill1Name;
    public string skill2Name;

    public string evolvesFrom = "";
    public string evolvesTo = "";

    public BaseMonsterData(string name)
    {
        this.name = name;
        Description = "This is the description for " + name;
        switch (name)
        {
            case "Borble":
                Attack = 10;
                Health = 26;
                Defense = 8;
                Movement = 1;
                ManaCost = 8;
                Cost = 10;
                skill1Name = "Bubble";
                skill2Name = "Wave";
                rarity = "Epic";
                evolvesTo = "Pupal";
                break;
            case "Pupal":
                Attack = 16;
                Health = 38;
                Defense = 12;
                Movement = 1;
                ManaCost = 10;
                Cost = 12;
                skill1Name = "Bubble";
                skill2Name = "Wave";
                rarity = "Epic";
                evolvesFrom = "Borble";
                evolvesTo = "Aquafly";
                break;
            case "Aquafly":
                Attack = 10;
                Health = 51;
                Defense = 10;
                Movement = 2;
                ManaCost = 12;
                Cost = 15;
                skill1Name = "Bubble";
                skill2Name = "Wave";
                rarity = "Epic";
                evolvesFrom = "Pupal";
                break;
            case "Leafree":
                Attack = 6;
                Health = 22;
                Defense = 7;
                Movement = 2;
                ManaCost = 6;
                Cost = 6;
                skill1Name = "Leaf";
                skill2Name = "Growth";
                rarity = "Rare";
                evolvesTo = "Leafear";
                break;
            case "Leafear":
                Attack = 12;
                Health = 35;
                Defense = 13;
                Movement = 2;
                ManaCost = 8;
                Cost = 10;
                skill1Name = "Leaf";
                skill2Name = "Growth";
                rarity = "Rare";
                evolvesFrom = "Leafree";
                break;
            case "Olla":
                Attack = 15;
                Health = 35;
                Defense = 14;
                Movement = 1;
                ManaCost = 7;
                Cost = 9;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Rare";
                break;
            case "Owisp":
                Attack = 5;
                Health = 15;
                Defense = 4;
                Movement = 1;
                ManaCost = 4;
                Cost = 5;
                skill1Name = "Spark";
                skill2Name = "Burn";
                rarity = "Uncommon";
                evolvesTo = "Wallowisp";
                break;
            case "Wallowisp":
                Attack = 9;
                Health = 22;
                Defense = 7;
                Movement = 1;
                ManaCost = 4;
                Cost = 6;
                skill1Name = "Spark";
                skill2Name = "Burn";
                rarity = "Uncommon";
                evolvesFrom = "Owisp";
                break;
            case "Slimy":
                Attack = 2;
                Health = 12;
                Defense = 1;
                Movement = 1;
                ManaCost = 2;
                Cost = 3;
                skill1Name = "Spark";
                skill2Name = "Burn";
                rarity = "Common";
                evolvesTo = "Slimier";
                break;
            case "Slimier":
                Attack = 3;
                Health = 18;
                Defense = 2;
                Movement = 1;
                ManaCost = 4;
                Cost = 5;
                skill1Name = "Spark";
                skill2Name = "Burn";
                rarity = "Common";
                evolvesFrom = "Slimy";
                evolvesTo = "Slimiest";
                break;
            case "Slimiest":
                Attack = 6;
                Health = 35;
                Defense = 4;
                Movement = 1;
                ManaCost = 6;
                Cost = 7;
                skill1Name = "Spark";
                skill2Name = "Burn";
                rarity = "Common";
                evolvesFrom = "Slimier";
                break;
            case "Snowbug":
                Attack = 8;
                Health = 20;
                Defense = 5;
                Movement = 1;
                ManaCost = 5;
                Cost = 5;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Uncommon";
                evolvesTo = "Snant";
                break;
            case "Snant":
                Attack = 12;
                Health = 30;
                Defense = 8;
                Movement = 1;
                ManaCost = 6;
                Cost = 7;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Uncommon";
                evolvesFrom = "Snowbug";
                evolvesTo = "Snowpede";
                break;
            case "Snowpede":
                Attack = 17;
                Health = 45;
                Defense = 12;
                Movement = 1;
                ManaCost = 8;
                Cost = 9;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Uncommon";
                evolvesFrom = "Snant";
                break;
            case "Squrl":
                Attack = 11;
                Health = 35;
                Defense = 8;
                Movement = 1;
                ManaCost = 8;
                Cost = 10;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Epic";
                evolvesTo = "Squrile";
                break;
            case "Squrile":
                Attack = 15;
                Health = 45;
                Defense = 12;
                Movement = 1;
                ManaCost = 10;
                Cost = 12;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Epic";
                evolvesFrom = "Squrl";
                break;
            case "Zaple":
                Attack = 3;
                Health = 10;
                Defense = 1;
                Movement = 1;
                ManaCost = 2;
                Cost = 4;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Common";
                evolvesTo = "Lightna";
                break;
            case "Lightna":
                Attack = 5;
                Health = 15;
                Defense = 2;
                Movement = 2;
                ManaCost = 3;
                Cost = 6;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Common";
                evolvesFrom = "Zaple";
                evolvesTo = "Thunda";
                break;
            case "Thunda":
                Attack = 8;
                Health = 23;
                Defense = 3;
                Movement = 2;
                ManaCost = 5;
                Cost = 10;
                skill1Name = "Zap";
                skill2Name = "Shock";
                rarity = "Common";
                evolvesFrom = "Lightna";
                break;
            default:
                Debug.Log("Invalid name: " + name);
                Attack = 0;
                Health = 0;
                Defense = 0;
                ManaCost = 0;
                Cost = 0;
                skill1Name = "Zap";
                skill2Name = "Spark";
                break;
        }
    }
}
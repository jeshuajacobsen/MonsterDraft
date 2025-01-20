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

    public int experienceGiven = 10;
    public int experienceRequired = 40;

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
                experienceGiven = 40;
                break;
            case "Pupal":
                Attack = 16;
                Health = 38;
                Defense = 12;
                Movement = 1;
                ManaCost = 10;
                Cost = 12;
                skill1Name = "Wave";
                skill2Name = "Water Jet";
                rarity = "Epic";
                evolvesFrom = "Borble";
                evolvesTo = "Aquafly";
                experienceGiven = 60;
                break;
            case "Aquafly":
                Attack = 10;
                Health = 51;
                Defense = 10;
                Movement = 2;
                ManaCost = 12;
                Cost = 15;
                skill1Name = "Water Jet";
                skill2Name = "Aqua Blast";
                rarity = "Epic";
                evolvesFrom = "Pupal";
                experienceGiven = 90;
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
                experienceGiven = 30;
                break;
            case "Leafear":
                Attack = 12;
                Health = 35;
                Defense = 13;
                Movement = 2;
                ManaCost = 8;
                Cost = 10;
                skill1Name = "Growth";
                skill2Name = "Solar Beam";
                rarity = "Rare";
                evolvesFrom = "Leafree";
                experienceGiven = 50;
                break;
            case "Olla":
                Attack = 15;
                Health = 35;
                Defense = 14;
                Movement = 1;
                ManaCost = 7;
                Cost = 9;
                skill1Name = "Wrap";
                skill2Name = "Poison Sting";
                rarity = "Rare";
                experienceGiven = 50;
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
                experienceGiven = 20;
                break;
            case "Wallowisp":
                Attack = 9;
                Health = 22;
                Defense = 7;
                Movement = 1;
                ManaCost = 4;
                Cost = 6;
                skill1Name = "Burn";
                skill2Name = "Heat Wave";
                rarity = "Uncommon";
                evolvesFrom = "Owisp";
                experienceGiven = 30;
                break;
            case "Slimy":
                Attack = 2;
                Health = 12;
                Defense = 1;
                Movement = 1;
                ManaCost = 2;
                Cost = 3;
                skill1Name = "Goo";
                skill2Name = "Slime Ball";
                rarity = "Common";
                evolvesTo = "Slimier";
                experienceGiven = 10;
                break;
            case "Slimier":
                Attack = 3;
                Health = 18;
                Defense = 2;
                Movement = 1;
                ManaCost = 4;
                Cost = 5;
                skill1Name = "Slime Ball";
                skill2Name = "Multiply";
                rarity = "Common";
                evolvesFrom = "Slimy";
                evolvesTo = "Slimiest";
                experienceGiven = 20;
                break;
            case "Slimiest":
                Attack = 6;
                Health = 35;
                Defense = 4;
                Movement = 1;
                ManaCost = 6;
                Cost = 7;
                skill1Name = "Multiply";
                skill2Name = "Slime Storm";
                rarity = "Common";
                evolvesFrom = "Slimier";
                experienceGiven = 30;
                break;
            case "Snowbug":
                Attack = 8;
                Health = 20;
                Defense = 5;
                Movement = 1;
                ManaCost = 5;
                Cost = 5;
                skill1Name = "Chill";
                skill2Name = "Ice Shard";
                rarity = "Uncommon";
                evolvesTo = "Snant";
                experienceGiven = 20;
                break;
            case "Snant":
                Attack = 12;
                Health = 30;
                Defense = 8;
                Movement = 1;
                ManaCost = 6;
                Cost = 7;
                skill1Name = "Ice Shard";
                skill2Name = "Freeze";
                rarity = "Uncommon";
                evolvesFrom = "Snowbug";
                evolvesTo = "Snowpede";
                experienceGiven = 30;
                break;
            case "Snowpede":
                Attack = 17;
                Health = 45;
                Defense = 12;
                Movement = 1;
                ManaCost = 8;
                Cost = 9;
                skill1Name = "Freeze";
                skill2Name = "Blizzard";
                rarity = "Uncommon";
                evolvesFrom = "Snant";
                experienceGiven = 50;
                break;
            case "Squrl":
                Attack = 11;
                Health = 35;
                Defense = 8;
                Movement = 1;
                ManaCost = 8;
                Cost = 10;
                skill1Name = "Bite";
                skill2Name = "Drain";
                rarity = "Epic";
                evolvesTo = "Squrile";
                experienceGiven = 40;
                break;
            case "Squrile":
                Attack = 15;
                Health = 45;
                Defense = 12;
                Movement = 1;
                ManaCost = 10;
                Cost = 12;
                skill1Name = "Drain";
                skill2Name = "Nightmare";
                rarity = "Epic";
                evolvesFrom = "Squrl";
                experienceGiven = 70;
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
                experienceGiven = 10;
                break;
            case "Lightna":
                Attack = 5;
                Health = 15;
                Defense = 2;
                Movement = 2;
                ManaCost = 3;
                Cost = 6;
                skill1Name = "Shock";
                skill2Name = "Lightning";
                rarity = "Common";
                evolvesFrom = "Zaple";
                evolvesTo = "Thunda";
                experienceGiven = 20;
                break;
            case "Thunda":
                Attack = 8;
                Health = 23;
                Defense = 3;
                Movement = 2;
                ManaCost = 5;
                Cost = 10;
                skill1Name = "Lightning";
                skill2Name = "Thunder Bolt";
                rarity = "Common";
                evolvesFrom = "Lightna";
                experienceGiven = 40;
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
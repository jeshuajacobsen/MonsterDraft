using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseStatsData
{
    public int Attack { get; set; }
    public int Health { get; set; }
    public int Defense { get; set; }
    public int Movement { get; set; }
    public int ManaCost { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }

    public string skill1Name;
    public string skill2Name;

    public BaseStatsData(string name)
    {
        Description = "This is the description for " + name;
        switch (name)
        {
            case "Zaple":
                Attack = 3;
                Health = 10;
                Defense = 0;
                Movement = 1;
                ManaCost = 2;
                Cost = 4;
                skill1Name = "Zap";
                skill2Name = "Shock";
                break;
            case "Owisp":
                Attack = 3;
                Health = 10;
                Defense = 2;
                Movement = 1;
                ManaCost = 2;
                Cost = 4;
                skill1Name = "Spark";
                skill2Name = "Burn";
                break;
            case "Leafree":
                Attack = 6;
                Health = 22;
                Defense = 7;
                Movement = 2;
                ManaCost = 8;
                Cost = 8;
                skill1Name = "Leaf";
                skill2Name = "Growth";
                break;
            case "Borble":
                Attack = 8;
                Health = 23;
                Defense = 3;
                Movement = 1;
                ManaCost = 8;
                Cost = 6;
                skill1Name = "Bubble";
                skill2Name = "Wave";
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
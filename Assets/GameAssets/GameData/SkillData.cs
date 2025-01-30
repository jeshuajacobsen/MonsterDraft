using System.Collections.Generic;
using System;
public class SkillData
{
    private int _manaCost;
    public int ManaCost {
        get
        {
            int costChange = 0;
            foreach (var effect in RoundManager.instance.persistentEffects)
            {
                if (effect.type == "SkillsCost")
                {
                    costChange += effect.amount;
                }
            } return Math.Max(0, _manaCost + costChange);
        }

        set
        {
            _manaCost = value;
        }
    }
    public int Damage { get; set; }
    public int Range { get; set; }
    public string name;
    public string Description { get; set; }

    public string directions;
    public List<string> effects;
    public bool attacksAllInRange = false;

    public string attackVisualEffect = "Lightning";

    public SkillData(string name)
    {
        this.name = name;
        this.Description = "";
        effects = new List<string>();
        switch (name)
        {
            //Basic
            case "Zap":
                ManaCost = 1;
                Damage = 3;
                Range = 2;
                directions = "Forward";
                attackVisualEffect = "Lightning";
                break;
            case "Bubble":
                ManaCost = 1;
                Damage = 5;
                Range = 1;
                directions = "Forward Backward";
                attackVisualEffect = "Water";
                break;
            case "Leaf":
                ManaCost = 2;
                Damage = 6;
                Range = 1;
                directions = "Forward";
                attackVisualEffect = "Leaf";
                break;
            case "Spark":
                ManaCost = 1;
                Damage = 5;
                Range = 1;
                directions = "Forward Backward";
                attackVisualEffect = "Fire";
                break;
            case "Wrap":
                ManaCost = 3;
                Damage = 9;
                Range = 1;
                directions = "Forward";
                break;
            case "Goo":
                ManaCost = 1;
                Damage = 4;
                Range = 1;
                directions = "Forward Backward";
                break;
            case "Chill":
                ManaCost = 2;
                Damage = 6;
                Range = 1;
                directions = "Forward Backward Up Down";
                break;
            case "Bite":
                ManaCost = 2;
                Damage = 7;
                Range = 1;
                directions = "Forward Backward";
                attackVisualEffect = "Bite";
                break;

            //Intermidiate
            case "Shock":
                ManaCost = 2;
                Damage = 5;
                Range = 1;
                directions = "Forward Backward Up Down";
                attackVisualEffect = "Lightning";
                break;
            case "Burn":
                ManaCost = 3;
                Damage = 8;
                Range = 1;
                directions = "Forward Backward Up Down";
                attackVisualEffect = "Fire";
                break;
            case "Growth":
                ManaCost = 3;
                Damage = 0;
                effects.Add("Buff Attack Plus 10 Duration 3");
                Range = 0;
                directions = "";
                Description = "Increase attack by 10 for 3 turns";
                break;
            case "Wave":
                ManaCost = 4;
                Damage = 10;
                Range = 2;
                directions = "Forward DiagonalForward";
                attacksAllInRange = true;
                Description = "Attacks all enemies in range.";
                attackVisualEffect = "Water";
                break;
            case "Poison Sting":
                ManaCost = 5;
                Damage = 12;
                Range = 1;
                directions = "Forward Backward";
                //effects.Add("Poison 3");
                break;
            case "Slime Ball":
                ManaCost = 2;
                Damage = 6;
                Range = 1;
                directions = "Forward Backward";
                //effects.Add("Slow 2");
                break;
            case "Ice Shard":
                ManaCost = 3;
                Damage = 10;
                Range = 1;
                directions = "Forward Backward";
                break;
            case "Drain":
                ManaCost = 4;
                Damage = 8;
                Range = 1;
                directions = "Forward Backward";
                //effects.Add("Heal 4");
                break;

            //Advanced
            case "Lightning":
                ManaCost = 5;
                Damage = 12;
                Range = 2;
                directions = "Forward Up Down";
                attackVisualEffect = "Lightning";
                break;
            case "Heat Wave":
                ManaCost = 6;
                Damage = 10;
                Range = 1;
                attacksAllInRange = true;
                directions = "Forward Backward Up Down";
                Description = "Attacks all enemies in range.";
                attackVisualEffect = "Fire";
                break;
            case "Solar Beam":
                ManaCost = 6;
                Damage = 20;
                Range = 2;
                directions = "Forward";
                break;
            case "Water Jet":
                ManaCost = 5;
                Damage = 10;
                Range = 2;
                directions = "Forward Backward";
                attacksAllInRange = true;
                attackVisualEffect = "Water";
                break;
            case "Multiply":
                ManaCost = 3;
                Damage = 0;
                Range = 1;
                directions = "Forward Backward Up Down";
                //effects.Add("Slow 3");
                break;
            case "Freeze":
                ManaCost = 4;
                Damage = 12;
                Range = 1;
                directions = "Forward Backward Up Down";
                //effects.Add("Freeze 2");
                break;
            case "Nightmare":
                ManaCost = 5;
                Damage = 15;
                Range = 1;
                directions = "Forward Backward Up Down";
                //effects.Add("Sleep 2");
                break;

            //Expert
            case "Thunder Bolt":
                ManaCost = 10;
                Damage = 23;
                Range = 1;
                directions = "Forward Backward Up Down";
                attackVisualEffect = "Lightning";
                break;
            case "Inferno":
                ManaCost = 13;
                Damage = 18;
                Range = 2;
                attacksAllInRange = true;
                directions = "Forward Backward Up Down";
                Description = "Attacks all enemies in range.";
                attackVisualEffect = "Fire";
                break;
            case "Poison Ivy":
                ManaCost = 6;
                Damage = 20;
                Range = 1;
                directions = "Forward DiagonalForward";
                break;
            case "Aqua Blast":
                ManaCost = 12;
                Damage = 25;
                Range = 1;
                directions = "Forward DiagonalForward";
                attacksAllInRange = true;
                attackVisualEffect = "Water";
                break;
            case "Slime Storm":
                ManaCost = 8;
                Damage = 15;
                Range = 1;
                directions = "Forward Backward Up Down";
                attacksAllInRange = true;
                break;
            case "Blizzard":
                ManaCost = 10;
                Damage = 15;
                Range = 2;
                directions = "Forward Backward Up Down DiagonalForward DiagonalBackward";
                attacksAllInRange = true;
                break;
        }
    }
}
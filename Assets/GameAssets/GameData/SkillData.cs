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
            // Basic
            case "Zap":
                ManaCost = 10;
                Damage = 30;
                Range = 2;
                directions = "Forward";
                attackVisualEffect = "Lightning";
                break;
            case "Bubble":
                ManaCost = 10;
                Damage = 50;
                Range = 1;
                directions = "Forward Backward";
                attackVisualEffect = "Water";
                break;
            case "Leaf":
                ManaCost = 20;
                Damage = 60;
                Range = 1;
                directions = "Forward";
                attackVisualEffect = "Leaf";
                break;
            case "Spark":
                ManaCost = 10;
                Damage = 50;
                Range = 1;
                directions = "Forward Backward";
                attackVisualEffect = "Fire";
                break;
            case "Wrap":
                ManaCost = 30;
                Damage = 90;
                Range = 1;
                directions = "Forward";
                break;
            case "Goo":
                ManaCost = 10;
                Damage = 40;
                Range = 1;
                directions = "Forward Backward";
                break;
            case "Chill":
                ManaCost = 20;
                Damage = 60;
                Range = 1;
                directions = "Forward Backward Up Down";
                break;
            case "Bite":
                ManaCost = 20;
                Damage = 70;
                Range = 1;
                directions = "Forward Backward";
                attackVisualEffect = "Bite";
                break;

            // Intermediate
            case "Shock":
                ManaCost = 20;
                Damage = 50;
                Range = 1;
                directions = "Forward Backward Up Down";
                attackVisualEffect = "Lightning";
                break;
            case "Burn":
                ManaCost = 30;
                Damage = 80;
                Range = 1;
                directions = "Forward Backward Up Down";
                attackVisualEffect = "Fire";
                break;
            case "Growth":
                ManaCost = 30;
                Damage = 0;
                effects.Add("Buff Attack Plus 100 Duration 3");
                Range = 0;
                directions = "";
                Description = "Increase attack by 100 for 3 turns";
                break;
            case "Wave":
                ManaCost = 40;
                Damage = 100;
                Range = 2;
                directions = "Forward DiagonalForward";
                attacksAllInRange = true;
                Description = "Attacks all enemies in range.";
                attackVisualEffect = "Water";
                break;
            case "Poison Sting":
                ManaCost = 50;
                Damage = 120;
                Range = 1;
                directions = "Forward Backward";
                //effects.Add("Poison 3");
                break;
            case "Slime Ball":
                ManaCost = 20;
                Damage = 60;
                Range = 1;
                directions = "Forward Backward";
                //effects.Add("Slow 2");
                break;
            case "Ice Shard":
                ManaCost = 30;
                Damage = 100;
                Range = 1;
                directions = "Forward Backward";
                break;
            case "Drain":
                ManaCost = 40;
                Damage = 80;
                Range = 1;
                directions = "Forward Backward";
                //effects.Add("Heal 40");
                break;

            // Advanced
            case "Lightning":
                ManaCost = 50;
                Damage = 120;
                Range = 2;
                directions = "Forward Up Down";
                attackVisualEffect = "Lightning";
                break;
            case "Heat Wave":
                ManaCost = 60;
                Damage = 100;
                Range = 1;
                attacksAllInRange = true;
                directions = "Forward Backward Up Down";
                Description = "Attacks all enemies in range.";
                attackVisualEffect = "Fire";
                break;
            case "Solar Beam":
                ManaCost = 60;
                Damage = 200;
                Range = 2;
                directions = "Forward";
                break;
            case "Water Jet":
                ManaCost = 50;
                Damage = 100;
                Range = 2;
                directions = "Forward Backward";
                attacksAllInRange = true;
                attackVisualEffect = "Water";
                break;
            case "Multiply":
                ManaCost = 30;
                Damage = 0;
                Range = 1;
                directions = "Forward Backward Up Down";
                //effects.Add("Slow 3");
                break;
            case "Freeze":
                ManaCost = 40;
                Damage = 120;
                Range = 1;
                directions = "Forward Backward Up Down";
                //effects.Add("Freeze 2");
                break;
            case "Nightmare":
                ManaCost = 50;
                Damage = 150;
                Range = 1;
                directions = "Forward Backward Up Down";
                //effects.Add("Sleep 2");
                break;

            // Expert
            case "Thunder Bolt":
                ManaCost = 100;
                Damage = 230;
                Range = 1;
                directions = "Forward Backward Up Down";
                attackVisualEffect = "Lightning";
                break;
            case "Inferno":
                ManaCost = 130;
                Damage = 180;
                Range = 2;
                attacksAllInRange = true;
                directions = "Forward Backward Up Down";
                Description = "Attacks all enemies in range.";
                attackVisualEffect = "Fire";
                break;
            case "Poison Ivy":
                ManaCost = 60;
                Damage = 200;
                Range = 1;
                directions = "Forward DiagonalForward";
                break;
            case "Aqua Blast":
                ManaCost = 120;
                Damage = 250;
                Range = 1;
                directions = "Forward DiagonalForward";
                attacksAllInRange = true;
                attackVisualEffect = "Water";
                break;
            case "Slime Storm":
                ManaCost = 80;
                Damage = 150;
                Range = 1;
                directions = "Forward Backward Up Down";
                attacksAllInRange = true;
                break;
            case "Blizzard":
                ManaCost = 100;
                Damage = 150;
                Range = 2;
                directions = "Forward Backward Up Down DiagonalForward DiagonalBackward";
                attacksAllInRange = true;
                break;
        }
    }
}
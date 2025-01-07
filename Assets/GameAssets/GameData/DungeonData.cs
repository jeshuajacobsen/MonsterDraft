using System.Collections;
using System.Collections.Generic;

public class DungeonData
{
    public string name;
    public List<string> cards;

    public DungeonData(string name)
    {
        this.name = name;
        switch (name)
        {
            case "Forest1":
                cards = new List<string> 
                    { "Fireball", "Zaple", "Fireball", "Owisp", "Heal", "Leafree", "Borble", "Borble" };
                break;
            case "Forest2":
                cards = new List<string> 
                    { "Fireball", "Zaple", "Owisp", "Fireball", "Leafree", "Borble", "Fireball", "Borble", "Leafree" };
                break;
            case "Forest3":
                cards = new List<string>
                    { "Fireball", "Zaple", "Owisp", "Heal", "Fireball", "Leafree", "Borble", "Fireball", "Borble", "Leafree" };
                break;
        }
    }
}
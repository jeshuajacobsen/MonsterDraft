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
                    { "Pass", "Fireball", "Pass", "Zaple", "Pass", "Fireball", "Owisp", "Heal", "Pass", "Leafree", "Borble" };
                break;
            case "Forest2":
                cards = new List<string> 
                    { "Pass", "Fireball", "Zaple", "Pass", "Owisp", "Fireball", "Pass", "Leafree", "Borble", "Pass", "Fireball", "Borble" };
                break;
            case "Forest3":
                cards = new List<string>
                    { "Pass", "Fireball", "Zaple", "Pass", "Owisp", "Heal", "Fireball", "Pass", "Leafree", "Borble", "Pass", "Fireball", "Borble" };
                break;
        }
    }
}
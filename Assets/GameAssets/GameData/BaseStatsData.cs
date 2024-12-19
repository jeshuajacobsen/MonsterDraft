
public class BaseStatsData
{
    public int Attack { get; set; }
    public int Health { get; set; }
    public int Defense { get; set; }

    public int ManaCost { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }

    public BaseStatsData(string name)
    {
        Description = "This is the description for " + name;
        switch (name)
        {
            case "Zaple":
                Attack = 3;
                Health = 2;
                Defense = 1;
                ManaCost = 2;
                break;
            case "Owisp":
                Attack = 3;
                Health = 1;
                Defense = 2;
                ManaCost = 2;
                break;
            case "Leafree":
                Attack = 1;
                Health = 3;
                Defense = 3;
                ManaCost = 2;
                break;
            case "Borble":
                Attack = 2;
                Health = 2;
                Defense = 2;
                ManaCost = 2;
                break;
            default:
                Attack = 0;
                Health = 0;
                Defense = 0;
                break;
        }
    }
}
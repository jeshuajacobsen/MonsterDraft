
public class PersistentEffect
{
    public string type;
    public int amount;
    public string description;
    public int duration;

    public PersistentEffect(string type, int amount, string description, int duration)
    {
        this.type = type;
        this.amount = amount;
        this.description = description;
        this.duration = duration;
    }
}
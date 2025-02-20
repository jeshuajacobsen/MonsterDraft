using System.Collections.Generic;

public class ActionCard : Card
{
    public List<string> requirements;

    public ActionCard(string name, int level) : base(name, "Action", level)
    {
        
    }

    public bool StartsWithTarget()
    {
        return effects[0].StartsWith("Target");
    }
}
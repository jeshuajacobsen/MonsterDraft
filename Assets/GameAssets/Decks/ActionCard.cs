using System.Collections.Generic;

public class ActionCard : Card
{
    public List<string> requirements;
    public string Description { get; set; }

    public ActionCard(string name, int level) : base(name, "Action", level)
    {
        BaseActionData baseStats = GameManager.instance.gameData.GetActionData(name);
        Description = baseStats.Description;
    }

    public bool StartsWithTarget()
    {
        return Effects[0].StartsWith("Target");
    }
}
using System.Collections.Generic;

public class ActionCard : Card
{
    public List<string> effects;
    public List<string> requirements;

    public ActionCard(string name) : base(name, "Action")
    {
        BaseActionData data = GameManager.instance.gameData.GetActionData(name);
        effects = data.Effects;
        requirements = data.Requirements;
    }

    public bool StartsWithTarget()
    {
        return effects[0].StartsWith("Target");
    }
}
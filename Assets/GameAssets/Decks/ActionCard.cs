using System.Collections.Generic;

public class ActionCard : Card
{
    public List<string> effects;

    public ActionCard(string name) : base(name, "Action")
    {
        effects = GameManager.instance.gameData.GetActionData(name).Effects;
    }
}
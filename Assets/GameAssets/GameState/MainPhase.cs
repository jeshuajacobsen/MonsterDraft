using UnityEngine;
using UnityEngine.UI;

public class MainPhase : GameState
{
    public MainPhase(RoundManager roundManager) : base(roundManager) { }

    public override void EnterState()
    {
        Debug.Log("Entering Main Phase");
        RoundManager.instance.Actions = 1;
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Draw Phase");
    }

    public override bool CanPlayCard(Card card, Vector3 position)
    {
        if (card is MonsterCard)
        {
            MonsterCard monsterCard = (MonsterCard)card;
            if (monsterCard.ManaCost > roundManager.Mana)
            {
                return false;
            }
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 7; tile++)
                {
                    Transform tileTransform = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                    if (tileTransform != null && tileTransform.GetComponent<Collider2D>().bounds.Contains(position))
                    {
                        return true;
                    }
                }
            }
        }
        if (card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)card;
            if (roundManager.Actions > 0)
            {
                return true;
            }
        }
        if (card is TreasureCard)
        {
            return true;
        }
        return false;
    }

    public override void PlayCard(Card card, Vector3 position)
    {
        if (card is TreasureCard)
        {
            TreasureCard treasureCard = (TreasureCard)card;
            roundManager.Coins += treasureCard.GoldGeneration;
            roundManager.Mana += treasureCard.ManaGeneration;
            roundManager.discardPile.AddCard(card);
        }
        else if (card is MonsterCard && roundManager.DungeonPanel.activeSelf)
        {
            MonsterCard monsterCard = (MonsterCard)card;
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 7; tile++)
                {
                    Transform tileTransform = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                    if (tileTransform != null && tileTransform.GetComponent<Collider2D>().bounds.Contains(position))
                    {
                        Monster newMonster = Instantiate(RoundManager.instance.MonsterPrefab, tileTransform);

                        RoundManager.instance.discardPile.AddCard(card);
                        return;
                    }
                }
            }
        }
        else if (card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)card;
            roundManager.Actions--;
            roundManager.discardPile.AddCard(card);
        }
    }
}
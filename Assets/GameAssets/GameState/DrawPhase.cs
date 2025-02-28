using UnityEngine;
using System.Collections;
using Zenject;

public class DrawPhase : GameState
{
    public DrawPhase() : base() { }

    public override void EnterState()
    {
        Debug.Log("Entering Draw Phase");
        _roundManager.DiscardHand();
        for (int i = 0; i < 5; i++)
        {
            _roundManager.AddCardToHand(_roundManager.roundDeck.DrawCard());
        }
        for (int row = 1; row <= 3; row++)
        {
            for (int tile = 1; tile <= 7; tile++)
            {
                Transform tileTransform = _uiManager.dungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                Tile tileComponent = tileTransform.GetComponent<Tile>();
                if (tileComponent.monster != null)
                {
                    var monster = tileComponent.monster;
                    for (int i = monster.buffs.Count - 1; i >= 0; i--)
                    {
                        monster.buffs[i].duration--;
                        if (monster.buffs[i].duration <= 0)
                        {
                            monster.buffs.RemoveAt(i);
                        }
                    }
                }
            }
        }
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
        return false;
    }

    public override void PlayCard(Card card, Vector3 position)
    {
        Debug.LogError("Cards cannot be played during the Draw Phase!");
    }

    public override void SelectTile(Tile tile, Vector2 position)
    {
        Debug.LogError("Tiles cannot be selected during the Draw Phase!");
    }

    public override void SwitchPhaseState(CardPlayState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
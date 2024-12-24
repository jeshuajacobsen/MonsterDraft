using UnityEngine;
using System.Collections.Generic;

public class EnemyPhase : GameState
{
    public EnemyPhase(RoundManager roundManager) : base(roundManager) { }

    public override void EnterState()
    {
        Debug.Log("Entering Enemy Phase");
        

        for (int row = 1; row <= 3; row++)
        {
            for (int i = 1; i <= 7; i++)
            {
                Tile tile = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{i}").GetComponent<Tile>();
                if (tile.monster != null && tile.monster.team == "Enemy")
                {
                    Monster currentMonster = tile.monster;
                    if (roundManager.EnemyCanMove(tile.monster))
                    {
                        roundManager.MoveEnemyMonster(tile.monster);
                    }
                    bool usedSkill = false;
                    if (roundManager.EnemyCanUseSkill(currentMonster, currentMonster.skill1))
                    {
                        Debug.Log("Enemy using skill 1");
                        usedSkill = true;
                        roundManager.EnemyUseSkill(currentMonster, currentMonster.skill1);
                    }
                    if (!usedSkill && roundManager.EnemyCanUseSkill(currentMonster, currentMonster.skill2))
                    {
                        Debug.Log("Enemy using skill 2");
                        roundManager.EnemyUseSkill(currentMonster, currentMonster.skill2);
                    }
                }
            }
        }

        Card card = roundManager.currentDungeon.DrawCard();
        if (card != null && card is MonsterCard)
        {
        
            List<Tile> openTiles = new List<Tile>();
            for (int row = 1; row <= 3; row++)
            {
                for (int i = 6; i <= 7; i++)
                {
                    Tile tile = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{i}").GetComponent<Tile>();
                    if (tile.monster == null)
                    {
                        openTiles.Add(tile);
                    }
                }
            }
            if (openTiles.Count > 0)
            {
                PlayMonsterCardOnTile((MonsterCard)card, openTiles[Random.Range(0, openTiles.Count)]);
            }
        }
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Enemy Phase");
    }

    public override bool CanPlayCard(Card card, Vector3 position)
    {
        return false;
    }

    public override void PlayCard(Card card, Vector3 position)
    {
        Debug.LogError("Cards cannot be played during the Draw Phase!");
    }

    private void PlayMonsterCardOnTile(MonsterCard card, Tile tile)
    {
        Monster newMonster = Instantiate(RoundManager.instance.MonsterPrefab, tile.transform);
        newMonster.InitValues(card, tile, "Enemy");
        tile.monster = newMonster;
    }

    public override void SelectTile(Tile tile, Vector3 position)
    {
        Debug.LogError("Tiles cannot be selected during the Draw Phase!");
    }
}
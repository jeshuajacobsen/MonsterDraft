using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class CombatManager : MonoBehaviour
{
    private DungeonManager _dungeonManager;
    private VisualEffectManager _visualEffectManager;
    private RoundManager _roundManager;
    private PlayerStats _playerStats;
    private DiContainer _container;
    public GameObject dungeonPanel;

    [Inject]
    public void Construct(DungeonManager dungeonManager, 
                          RoundManager roundManager, 
                          VisualEffectManager visualEffectManager, 
                          PlayerStats playerStats,
                          DiContainer container)
    {
        _dungeonManager = dungeonManager;
        _roundManager = roundManager;
        _visualEffectManager = visualEffectManager;
        _playerStats = playerStats;
        _container = container;
    }

    public List<Tile> GetValidTargets(Monster monster, SkillData skill)
    {
        List<Tile> validTargets = new List<Tile>();
        string[] directionArray = skill.directions.Split(' ');

        if (directionArray.Length == 0)
        {
            Debug.Log("No directions");
            return validTargets;
        }

        int tileIndex = int.Parse(monster.tileOn.name.Replace("Tile", ""));

        foreach (string direction in directionArray)
        {
            List<Tile> targetTiles = new List<Tile>();

            switch (direction)
            {
                case "Up":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetTile(tileIndex));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetTile(tileIndex));
                        }
                    }
                    break;

                case "Down":
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetTile(tileIndex));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetTile(tileIndex));
                        }
                    }
                    break;

                case "Backward":
                    for (int i = 1; i <= skill.Range; i++)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.GetPreviousTile(monster.tileOn, i, monster.tileOn.dungeonRow));
                    }
                    

                    break;

                case "Forward":
                    for (int i = 1; i <= skill.Range; i++)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, i, monster.tileOn.dungeonRow));
                    }
                    break;

                case "DiagonalBackward":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetPreviousTile(monster.tileOn, 1, monster.tileOn.dungeonRow.upRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetPreviousTile(monster.tileOn, 2, monster.tileOn.dungeonRow.upRow.upRow));
                        }
                    }
                    
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetPreviousTile(monster.tileOn, 1, monster.tileOn.dungeonRow.downRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetPreviousTile(monster.tileOn, 2, monster.tileOn.dungeonRow.downRow.downRow));
                        }
                    }
                    break;

                case "DiagonalForward":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow.upRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetNextTile(monster.tileOn, 2, monster.tileOn.dungeonRow.upRow.upRow));
                        }
                    }
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow.downRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetNextTile(monster.tileOn, 2, monster.tileOn.dungeonRow.downRow.downRow));
                        }
                    }
                    break;

                default:
                    Debug.Log("Invalid direction");
                    break;
            }

            foreach (Tile targetTile in targetTiles)
            {
                if (targetTile != null && targetTile.monster != null && targetTile.monster.team == "Enemy")
                {
                    validTargets.Add(targetTile);
                }
            }
        }

        return validTargets;
    }

    public List<Tile> GetValidTargetsForEnemy(Monster monster, SkillData skill)
    {
        
        List<Tile> validTargets = new List<Tile>();
        string[] directionArray = skill.directions.Split(' ');

        if (directionArray.Length == 0)
        {
            Debug.Log("No directions");
            return validTargets;
        }

        int tileIndex = int.Parse(monster.tileOn.name.Replace("Tile", ""));

        foreach (string direction in directionArray)
        {
            List<Tile> targetTiles = new List<Tile>();

            switch (direction)
            {
                case "Up":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetTile(tileIndex));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetTile(tileIndex));
                        }
                    }
                    break;

                case "Down":
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetTile(tileIndex));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetTile(tileIndex));
                        }
                    }
                    break;

                case "Forward":
                    for (int i = 1; i <= skill.Range; i++)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.GetPreviousTile(monster.tileOn, i, monster.tileOn.dungeonRow));
                    }
                    break;

                case "Backward":
                    for (int i = 1; i <= skill.Range; i++)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, i, monster.tileOn.dungeonRow));
                    }
                    break;

                case "DiagonalForward":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetPreviousTile(monster.tileOn, 1, monster.tileOn.dungeonRow.upRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetPreviousTile(monster.tileOn, 2, monster.tileOn.dungeonRow.upRow.upRow));
                        }
                    }
                    
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetPreviousTile(monster.tileOn, 1, monster.tileOn.dungeonRow.downRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetPreviousTile(monster.tileOn, 2, monster.tileOn.dungeonRow.downRow.downRow));
                        }
                    }
                    break;

                case "DiagonalBackward":
                    if (monster.tileOn.dungeonRow.upRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.upRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow.upRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.upRow.upRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.upRow.upRow.GetNextTile(monster.tileOn, 2, monster.tileOn.dungeonRow.upRow.upRow));
                        }
                    }
                    if (monster.tileOn.dungeonRow.downRow != null)
                    {
                        targetTiles.Add(monster.tileOn.dungeonRow.downRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow.downRow));
                        if (skill.Range > 1 && monster.tileOn.dungeonRow.downRow.downRow != null)
                        {
                            targetTiles.Add(monster.tileOn.dungeonRow.downRow.downRow.GetNextTile(monster.tileOn, 2, monster.tileOn.dungeonRow.downRow.downRow));
                        }
                    }
                    break;

                default:
                    Debug.Log("Invalid direction");
                    break;
            }
            foreach (Tile targetTile in targetTiles)
            {
                if (targetTile != null && targetTile.monster != null && targetTile.monster.team == "Ally")
                {
                    validTargets.Add(targetTile);
                }
            }
        }

        return validTargets;
    }

    public void MoveMonster(Monster monster)
    {
        int maxDistance = monster.Movement;
        for (int i = 1; i <= monster.Movement; i++)
        {
            Tile nextTile = monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, i, monster.tileOn.dungeonRow);
            if (nextTile != null && nextTile.GetComponent<Tile>().monster != null)
            {
                maxDistance = i - 1;
                break;
            }
        }
        string tileName = monster.tileOn.name;
        int currentTileNumber = int.Parse(tileName.Substring(4));
        Transform parentTransform = monster.tileOn.transform.parent;
        int newTileNumber = Math.Min(currentTileNumber + monster.Movement, currentTileNumber + maxDistance);
        if (newTileNumber > 7)
        {
            newTileNumber = 7;
        }
        if (newTileNumber < 1)
        {
            newTileNumber = 1;
        }
        Transform newTile = parentTransform.Find("Tile" + newTileNumber);
        monster.tileOn.monster = null;
        monster.MoveTile(newTile.GetComponent<Tile>());
    }

    public bool CanMoveMonster(Monster monster)
    {
        Tile nextTile = monster.tileOn.dungeonRow.GetNextTile(monster.tileOn, 1, monster.tileOn.dungeonRow);
        return nextTile != null && nextTile.GetComponent<Tile>().monster == null;
    }

    public bool EnemyCanMove(Monster monster)
    {
        return _dungeonManager.CheckForMonster(monster.tileOn, -1) == null;
    }

    public void MoveEnemyMonster(Monster monster)
    {

        int maxDistance = monster.Movement;
        for (int i = 1; i <= monster.Movement; i++)
        {
            Monster blockingMonster = _dungeonManager.CheckForMonster(monster.tileOn, -i);
            if (blockingMonster != null)
            {
                maxDistance = i - 1;
                break;
            }
        }
        string tileName = monster.tileOn.name;
        int currentTileNumber = int.Parse(tileName.Substring(4));
        Transform parentTransform = monster.tileOn.transform.parent;
        int newTileNumber = Math.Max(currentTileNumber - monster.Movement, currentTileNumber - maxDistance);
        if (newTileNumber > 7)
        {
            newTileNumber = 7;
        }
        if (newTileNumber < 1)
        {
            newTileNumber = 1;
        }
        Transform newTile = parentTransform.Find("Tile" + newTileNumber);
        monster.tileOn.monster = null;
        monster.tileOn = newTile.GetComponent<Tile>();
        monster.MoveTile(newTile.GetComponent<Tile>());

        
    }

    public void UseSkill(Monster monster, SkillData skill, Tile tile)
    {
        _playerStats.Mana -= skill.ManaCost;
        int damage = DamageDealtToMonster(tile.monster, monster, skill);
        _visualEffectManager.CreateSkillVisualEffect(
            skill.attackVisualEffect, tile, monster.tileOn.transform.position, damage);
        
        monster.actionsUsedThisTurn.Add(skill.name);
    }

    public void UseAreaSkill(Monster monster, SkillData skill, List<Tile> targets)
    {
        _playerStats.Mana -= skill.ManaCost;
        foreach (Tile target in targets)
        {
            int damage = DamageDealtToMonster(target.monster, monster, skill);
            _visualEffectManager.CreateSkillVisualEffect(
                skill.attackVisualEffect, target, monster.tileOn.transform.position, damage);
        }
        monster.actionsUsedThisTurn.Add(skill.name);
    }
    

    public void SelectSkill(Monster monster, SkillData skill)
    {
        if(skill.ManaCost <= _playerStats.Mana)
        {
            _roundManager.gameState.SwitchPhaseState(_container.Instantiate<SelectingSkillTargetState>().Initialize(monster, skill));
        }
    }

    public int DamageDealtToMonster(Monster defender, Monster attacker, SkillData skill)
    {
        float damageMultiplier = (float)attacker.Attack / (attacker.Attack + defender.Defense);
        int damage = Mathf.RoundToInt(damageMultiplier * skill.Damage);
        return damage;
    }

    public bool EnemyCanUseSkill(Monster monster, SkillData skill)
    {
        int currentTileNumber = int.Parse(monster.tileOn.name.Substring(4));
        return GetValidTargetsForEnemy(monster, skill).Count > 0 || currentTileNumber - skill.Range < 1;
    }

    public void EnemyUseSkill(Monster monster, SkillData skill)
    {
        for (int i = 1; i <= skill.Range; i++)
        {
            int currentTileNumber = int.Parse(monster.tileOn.name.Substring(4));
            List<Tile> validTargets = GetValidTargetsForEnemy(monster, skill);
            if (currentTileNumber - i < 1)
            {
                _dungeonManager.DamagePlayerBase(skill.Damage);
                monster.actionsUsedThisTurn.Add(skill.name);
                break;
            }
            else if (validTargets.Count > 0)
            {
                Tile target = validTargets[UnityEngine.Random.Range(0, validTargets.Count)];
                int damage = DamageDealtToMonster(target.monster, monster, skill);
                target.monster.Health -= damage;
                _visualEffectManager.CreateFloatyNumber(damage, target, true);
                monster.actionsUsedThisTurn.Add(skill.name);
                break;
            }
        }
    }

}

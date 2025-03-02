using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingSkillTargetState : CardPlayState
{
    private List<Tile> validTargets;
    private Monster monster;
    private SkillData skill;
    

    public SelectingSkillTargetState Initialize(Monster monster, SkillData skill)
    {
        validTargets = new List<Tile>();
        this.monster = monster;
        this.skill = skill;
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Selecting Skill Target Entered");
        MarkValidTargets();
        //mainPhase.playedActionCardStep++;
    }

    public override void HandleInput()
    {
        // Handle target selection input
    }

    public override void UpdateState()
    {
        bool pointerDown = false;
        Vector2 pointerPosition = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            pointerDown = true;
            pointerPosition = Input.mousePosition;
        }
        
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pointerDown = true;
            pointerPosition = Input.GetTouch(0).position;
        }

        if (pointerDown)
        {
            int tileIndex = int.Parse(monster.tileOn.name.Replace("Tile", ""));

            bool isInEnemyBase = RectTransformUtility.RectangleContainsScreenPoint(
                _dungeonManager.EnemyBase.transform.GetComponent<RectTransform>(),
                pointerPosition,
                _roundManager.gameState.mainCamera
            );

            if (tileIndex + skill.Range > 7 && isInEnemyBase)
            {
                _playerStats.Mana -= skill.ManaCost;
                _dungeonManager.DamageEnemyBase(skill.Damage);
                monster.actionsUsedThisTurn.Add(skill.name);
                if (skill.effects.Count > 0)
                {
                    _roundManager.gameState.SwitchPhaseState(_container.Instantiate<ResolvingSkillEffectState>().Initialize(skill, monster, null));
                }
                else
                {
                    _roundManager.gameState.SwitchPhaseState(_container.Instantiate<IdleState>());
                }
                return;
            }

            if (skill.attacksAllInRange)
            {
                _combatManager.UseAreaSkill(monster, skill, validTargets);
                if (skill.effects.Count > 0)
                {
                    _roundManager.gameState.SwitchPhaseState(_container.Instantiate<ResolvingSkillEffectState>().Initialize(skill, monster, validTargets));
                }
                else
                {
                    _roundManager.gameState.SwitchPhaseState(_container.Instantiate<IdleState>());
                }
                return;
            }

            foreach (Tile tile in validTargets)
            {
                bool isInTile = RectTransformUtility.RectangleContainsScreenPoint(
                    tile.transform.GetComponent<RectTransform>(),
                    pointerPosition,
                    _roundManager.gameState.mainCamera
                );

                if (isInTile)
                {
                    _combatManager.UseSkill(monster, skill, tile);
                    if (skill.effects.Count > 0)
                    {
                        _roundManager.gameState.SwitchPhaseState(_container.Instantiate<ResolvingSkillEffectState>().Initialize(skill, monster, new List<Tile>{tile}));
                    }
                    else
                    {
                        _roundManager.gameState.SwitchPhaseState(_container.Instantiate<IdleState>());
                    }
                    return;
                }
            }

            _roundManager.gameState.SwitchPhaseState(_container.Instantiate<IdleState>());
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Selecting Skill Target State");
        validTargets.ForEach(tile => tile.GetComponent<Image>().color = Color.white);
        validTargets.Clear();
    }

   public void MarkValidTargets()
    {
        validTargets = _combatManager.GetValidTargets(monster, skill);
        if (skill.directions == "")
        {
            validTargets.Add(monster.tileOn);
        }
        int tileIndex = int.Parse(monster.tileOn.name.Replace("Tile", ""));
        if (tileIndex + skill.Range > 7) 
        {
            //TODO mark enemy base
        }
        MarkTargets(validTargets);
    }

    

    public void MarkTargets(List<Tile> targets)
    {
        foreach (Tile target in targets)
        {
            target.GetComponent<Image>().color = Color.green;
        }
    }

    
}

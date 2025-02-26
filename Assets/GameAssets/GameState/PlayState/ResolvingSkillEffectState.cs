using System;
using UnityEngine;
using System.Collections.Generic;

public class ResolvingSkillEffectState : CardPlayState
{
    SkillData skill;
    Monster skillUser;
    List<Tile> targets = new List<Tile>();

    public ResolvingSkillEffectState Initialize(SkillData skill, Monster skillUser, List<Tile> targets) 
    { 
        this.skill = skill;
        this.skillUser = skillUser;
        this.targets = targets;
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Resolving Skill Effect State Entered");
        ResolveEffects();
    }

    public override void HandleInput() { }

    public override void UpdateState()
    {

        
    }

    private void ResolveEffects()
    {
        List<string> effects = skill.effects;
        for (int i = 0; i < effects.Count; i++)
        {
            string[] effectParts = effects[i].Split(' ');
            if (effectParts[0] == "Buff")
            {
                int buffValue = 0;
                string buffType = effectParts[1];
                string buffDescription = "";
                int duration = 0;
                if (effectParts[2] == "Plus")
                {
                    buffValue = int.Parse(effectParts[3]);
                } else if (effectParts[2] == "Minus")
                {
                    buffValue = -int.Parse(effectParts[3]);
                }
                if (effectParts[4] == "Duration")
                {
                    duration = int.Parse(effectParts[5]);
                }
                
                buffDescription = buffValue > 0 ? "+" : "-" + buffValue + " " + buffType;
                if (effectParts.Length > 6 && effectParts[6] == "All")
                {
                    if (effectParts[7] == "Ally")
                    {
                        foreach (var ally in _roundManager.GetAllAllies())
                        {
                            ally.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                        }
                    } else if (effectParts[7] == "Enemy")
                    {
                        foreach (var enemy in _roundManager.GetAllEnemies())
                        {
                            enemy.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                        }
                    }
                } else {
                    targets[0].monster.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                }
            }
        }
        
        mainPhase.SwitchPhaseState(_container.Instantiate<IdleState>());
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Resolving Skill Effect State");
    }
}

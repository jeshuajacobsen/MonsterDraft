using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingSkillTargetState : CardPlayState
{
    private List<Tile> validTargets;
    private Monster monster;
    private SkillData skill;
    

    public SelectingSkillTargetState(MainPhase mainPhase, Monster monster, SkillData skill) : base(mainPhase)
    {
        validTargets = new List<Tile>();
        this.monster = monster;
        this.skill = skill;
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
                RoundManager.instance.EnemyBase.transform.GetComponent<RectTransform>(),
                pointerPosition,
                mainPhase.mainCamera
            );

            if (tileIndex + skill.Range > 7 && isInEnemyBase)
            {
                RoundManager.instance.UseSkillOnBase(monster, skill);
                mainPhase.SetState(new IdleState(mainPhase));
                return;
            }

            foreach (Tile tile in validTargets)
            {
                bool isInTile = RectTransformUtility.RectangleContainsScreenPoint(
                    tile.transform.GetComponent<RectTransform>(),
                    pointerPosition,
                    mainPhase.mainCamera
                );

                if (isInTile)
                {
                    RoundManager.instance.UseSkill(monster, skill, tile);
                    mainPhase.SetState(new IdleState(mainPhase));
                    return;
                }
            }

            mainPhase.SetState(new IdleState(mainPhase));
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
        validTargets = RoundManager.instance.GetValidTargets(monster, skill);
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

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayingMonsterState : CardPlayState
{
    private List<Tile> validTargets;
    private MonsterCard card;
    private int gemCost;

    public AutoPlayingMonsterState(MainPhase mainPhase, MonsterCard card, int gemCost = 0) : base(mainPhase)
    {
        this.validTargets = new List<Tile>();
        this.card = card;
        this.gemCost = gemCost;
    }
    public override void EnterState()
    {
        Debug.Log("Auto Playing Monster State Entered");
        RoundManager.instance.roundPanel.transform.Find("TownPanel").gameObject.SetActive(false);
        MarkValidTargets();
        if (gemCost > 0)
        {
            RoundManager.instance.roundPanel.transform.Find("BoostsPanel").gameObject.SetActive(false);
            RoundManager.instance.SetupBoostCancelButton(gemCost);
        } else {
            RoundManager.instance.SetupDoneButton(false);
        }
    }

    public override void HandleInput()
    {
        
    }

    public override void UpdateState()
    {
        bool pointerUp = false;
        Vector2 pointerPosition = Vector2.zero;

        if (Input.GetMouseButtonUp(0))
        {
            pointerUp = true;
            pointerPosition = Input.mousePosition;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            pointerUp = true;
            pointerPosition = Input.GetTouch(0).position;
        }

        if (pointerUp)
        {
            HandleCardDrop(card, pointerPosition);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting playing monster state");
        validTargets.ForEach(tile => tile.GetComponent<Image>().color = Color.white);
        validTargets.Clear();
        RoundManager.instance.CleanupDoneButton();
    }

    public void MarkValidTargets()
    {
        
        for (int row = 1; row <= 3; row++)
        {
            for (int tile = 1; tile <= 2; tile++)
            {
                Transform tileTransform = RoundManager.instance.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                if (tileTransform != null)
                {
                    Tile tileComponent = tileTransform.GetComponent<Tile>();
                    if (tileComponent.monster == null)
                    {
                        validTargets.Add(tileComponent);
                        tileComponent.GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                    }
                }
            }
        }
    }

    public void HandleCardDrop(MonsterCard card, Vector2 dropPosition)
    {
        for (int i = 0; i < validTargets.Count; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                validTargets[i].GetComponent<RectTransform>(),
                dropPosition,
                mainPhase.mainCamera
            ))
            {
                mainPhase.AutoPlayMonsterCard(card, validTargets[i]);
                mainPhase.SwitchPhaseState(new IdleState(mainPhase));
            }
        }
    }
    
}

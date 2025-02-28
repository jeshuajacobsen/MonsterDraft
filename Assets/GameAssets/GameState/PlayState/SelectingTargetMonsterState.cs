using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingTargetMonsterState : CardPlayState
{
    private List<Tile> validTargets;
    private Card card;
    

    public SelectingTargetMonsterState Initialize(Card card)
    {
        this.card = card;
        validTargets = new List<Tile>();
        return this;
    }

    public override void EnterState()
    {
        Debug.Log("Quick Selecting Monster Tile State Entered");
        MarkValidTargets(card as ActionCard);
        //mainPhase.playedActionCardStep++;
        _roundManager.messageText.text = "Select target for " + card.Name;
        _roundManager.messageText.gameObject.SetActive(true);
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
            for (int i = 0; i < validTargets.Count; i++)
            {
                RectTransform targetRect = validTargets[i].GetComponent<RectTransform>();

                if (RectTransformUtility.RectangleContainsScreenPoint(targetRect, pointerPosition, mainPhase.mainCamera))
                {
                    mainPhase.selectedTile = validTargets[i];
                    mainPhase.SwitchPhaseState(_container.Instantiate<ResolvingEffectState>());
                    break;
                }
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Quick Selecting Monster Tile State");
        validTargets.ForEach(tile => tile.GetComponent<Image>().color = Color.white);
        validTargets.Clear();
    }

    public void MarkValidTargets(ActionCard actionCard)
    {
        string effect = actionCard.Effects[mainPhase.playedActionCardStep - 1];

        string[] effectParts = effect.Split(' ');
        if (effectParts[0] == "Target")
        {
            if (effectParts[1] == "Enemy")
            {
                for (int row = 1; row <= 3; row++)
                {
                    for (int tile = 1; tile <= 7; tile++)
                    {
                        Transform tileTransform = _uiManager.dungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                        if (tileTransform != null)
                        {
                            Tile tileComponent = tileTransform.GetComponent<Tile>();
                            if (tileComponent.monster != null && tileComponent.monster.team == "Enemy")
                            {
                                validTargets.Add(tileComponent);
                                tileComponent.GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                            }
                        }
                    }
                }
            } else if (effectParts[1] == "Ally")
            {
                for (int row = 1; row <= 3; row++)
                {
                    for (int tile = 1; tile <= 7; tile++)
                    {
                        Transform tileTransform = _uiManager.dungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                        if (tileTransform != null)
                        {
                            Tile tileComponent = tileTransform.GetComponent<Tile>();
                            if (tileComponent.monster != null && tileComponent.monster.team == "Ally")
                            {
                                validTargets.Add(tileComponent);
                                tileComponent.GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                            }
                        }
                    }
                }
            }
            
        }
    }
    
}

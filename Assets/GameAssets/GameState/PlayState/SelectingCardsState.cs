using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingCardsState : CardPlayState
{
    private string numberToSelect;
    private string restriction;
    private List<SmallCardView> selectedCards = new List<SmallCardView>();
    
    public SelectingCardsState(MainPhase mainPhase, string numberToSelect, string restriction) : base(mainPhase)
    {
        this.restriction = restriction;
        this.numberToSelect = numberToSelect;
    }

    public override void EnterState()
    {
        Debug.Log("Selecting cards State Entered");
        RoundManager.instance.SetupDoneButton();
    }

    public override void HandleInput()
    {
        // Handle target selection input
    }

    public override void UpdateState()
    {
        RoundManager roundManager = RoundManager.instance;
        if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < roundManager.hand.Count; i++)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(
                    roundManager.hand[i].GetComponent<RectTransform>(),
                    Input.mousePosition,
                    mainPhase.mainCamera
                ))
                {
                    if (numberToSelect == "x" && meetsRestrictions(roundManager.hand[i]))
                    {
                        if (selectedCards.Contains(roundManager.hand[i]))
                        {
                            selectedCards.Remove(roundManager.hand[i]);
                            roundManager.hand[i].GetComponent<Image>().color = Color.white;
                        } else {
                            selectedCards.Add(roundManager.hand[i]);
                            roundManager.hand[i].GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                        }
                    } else
                    {
                        if (selectedCards.Contains(roundManager.hand[i]))
                        {
                            selectedCards.Remove(roundManager.hand[i]);
                            roundManager.hand[i].GetComponent<Image>().color = Color.white;
                        } else if (selectedCards.Count < int.Parse(numberToSelect) && meetsRestrictions(roundManager.hand[i])) {
                            selectedCards.Add(roundManager.hand[i]);
                            roundManager.hand[i].GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                        }
                    }
                }
            }
        }
    }

    public bool meetsRestrictions(SmallCardView cardView)
    {
        if (restriction == "None")
        {
            return true;
        }
        else if (restriction == "Treasure")
        {
            if (cardView.card is TreasureCard)
            {
                return true;
            }
        }
        return false;
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Selecting cards State");
        RoundManager.instance.CleanupDoneButton();
        selectedCards.ForEach(cardView => {
            cardView.GetComponent<Image>().color = Color.white;
        });
        mainPhase.selectedCards = selectedCards;
    }
}

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
        RoundManager.instance.messageText.text = "Select " + numberToSelect + " cards";
        RoundManager.instance.messageText.gameObject.SetActive(true);
    }

    public override void HandleInput()
    {
        // Handle target selection input
    }

    public override void UpdateState()
    {
        RoundManager roundManager = RoundManager.instance;

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
            for (int i = 0; i < roundManager.hand.Count; i++)
            {
                SmallCardView cardView = roundManager.hand[i];
                RectTransform cardRect = cardView.GetComponent<RectTransform>();

                if (RectTransformUtility.RectangleContainsScreenPoint(
                    cardRect,
                    pointerPosition,
                    mainPhase.mainCamera
                ))
                {
                    if (numberToSelect == "x" && meetsRestrictions(cardView))
                    {
                        if (selectedCards.Contains(cardView))
                        {
                            selectedCards.Remove(cardView);
                            cardView.GetComponent<Image>().color = Color.white;
                        }
                        else
                        {
                            selectedCards.Add(cardView);
                            cardView.GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                        }
                    }
                    else
                    {
                        if (selectedCards.Contains(cardView))
                        {
                            selectedCards.Remove(cardView);
                            cardView.GetComponent<Image>().color = Color.white;
                        }
                        else if (selectedCards.Count < int.Parse(numberToSelect) && meetsRestrictions(cardView))
                        {
                            selectedCards.Add(cardView);
                            cardView.GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
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
        else if (restriction == "Action")
        {
            if (cardView.card is ActionCard)
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
        RoundManager.instance.messageText.text = "";
        RoundManager.instance.messageText.gameObject.SetActive(false);
    }
}

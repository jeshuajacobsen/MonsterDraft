using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectingCardsState : CardPlayState
{
    private string numberToSelect;

    private List<SmallCardView> selectedCards = new List<SmallCardView>();
    
    public SelectingCardsState(MainPhase mainPhase, string numberToSelect) : base(mainPhase)
    {
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
                    if (selectedCards.Contains(roundManager.hand[i]))
                    {
                        selectedCards.Remove(roundManager.hand[i]);
                        roundManager.hand[i].GetComponent<Image>().color = Color.white;
                    } else {
                        selectedCards.Add(roundManager.hand[i]);
                        roundManager.hand[i].GetComponent<Image>().color = new Color32(0x3C, 0xFF, 0x00, 0xFF);
                    }
                }
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Selecting cards State");
        RoundManager.instance.doneButton.GetComponent<Button>().onClick.RemoveAllListeners();
        RoundManager.instance.cancelButton.GetComponent<Button>().onClick.RemoveAllListeners();
        RoundManager.instance.doneButton.gameObject.SetActive(false);
        RoundManager.instance.cancelButton.gameObject.SetActive(false);
        selectedCards.ForEach(cardView => {
            cardView.GetComponent<Image>().color = Color.white;
        });
        mainPhase.selectedCards = selectedCards;
    }
}

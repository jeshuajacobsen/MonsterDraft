using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RunManager : MonoBehaviour
{
    public static RunManager instance;
    
    public RunDeck runDeck;

    [SerializeField] private GameObject roundPanel;

    public GameObject SelectedLargeCardView;
    public GameObject betweenRoundPanel;

    public Button doneButton;

    public string currentDungeonLevel;
    public int currentDungeonIndex = 1;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        doneButton.onClick.AddListener(PickCard);
        currentDungeonLevel = "Forest";
    }

    public void Update()
    {
        if (betweenRoundPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            RectTransform[] largeCardViews = new RectTransform[]
            {
                betweenRoundPanel.transform.Find("LargeCardView1").GetComponent<RectTransform>(),
                betweenRoundPanel.transform.Find("LargeCardView2").GetComponent<RectTransform>(),
                betweenRoundPanel.transform.Find("LargeCardView3").GetComponent<RectTransform>()
            };

            for (int i = 0; i < largeCardViews.Length; i++)
            {
                var cardView = largeCardViews[i];
                if (RectTransformUtility.RectangleContainsScreenPoint(cardView, Input.mousePosition, Camera.main))
                {
                    if (SelectedLargeCardView == cardView.gameObject)
                    {
                        SelectedLargeCardView = null;
                        betweenRoundPanel.transform.Find("CardViewSelectedBackground" + (i + 1)).gameObject.SetActive(false);
                        betweenRoundPanel.transform.Find("PromptText").gameObject.SetActive(true);
                        betweenRoundPanel.transform.Find("DoneButton").gameObject.SetActive(false);
                        break;
                    } else {
                        SelectedLargeCardView = cardView.gameObject;
                        betweenRoundPanel.transform.Find("CardViewSelectedBackground" + (i + 1)).gameObject.SetActive(true);
                        betweenRoundPanel.transform.Find("PromptText").gameObject.SetActive(false);
                        betweenRoundPanel.transform.Find("DoneButton").gameObject.SetActive(true);
                        break;
                    }
                }
            }
            for (int i = 0; i < largeCardViews.Length; i++)
            {
                if (SelectedLargeCardView != largeCardViews[i].gameObject)
                {
                    betweenRoundPanel.transform.Find("CardViewSelectedBackground" + (i + 1)).gameObject.SetActive(false);
                }
            }
        }
    }

    private void PickCard()
    {
        if (SelectedLargeCardView != null)
        {
            Card selectedCard = SelectedLargeCardView.GetComponent<LargeCardView>().card;
            runDeck.AddCard(selectedCard);
            betweenRoundPanel.SetActive(false);
            StartRound();
        }
    }

    public void StartRun()
    {
        runDeck = new RunDeck(GameManager.instance.selectedInitialDeck);
        currentDungeonIndex = 1;
        betweenRoundPanel.SetActive(false);
        StartRound();
    }

    public void StartRound()
    {
        roundPanel.gameObject.SetActive(true);
        RoundManager.instance.StartRound("Forest", currentDungeonIndex);
    }

    public void EndRound(List<Card> gainedCards)
    {
        currentDungeonIndex++;
        if (currentDungeonIndex <= GameManager.instance.gameData.DungeonData(currentDungeonLevel).dungeons.Count)
        {
            roundPanel.gameObject.SetActive(false);
            betweenRoundPanel.gameObject.SetActive(true);
            SelectCards(gainedCards);
        } else {
            EndRunWin();
        }
        
        
    }

    public void EndRunWin()
    {
        GameManager.instance.menuPanel.SetActive(true);
        roundPanel.gameObject.SetActive(false);
    }

    public void EndRoundLose()
    {
        GameManager.instance.menuPanel.SetActive(true);
        roundPanel.gameObject.SetActive(false);
    }

    private void SelectCards(List<Card> gainedCards)
    {
        LargeCardView[] largeCardViews = new LargeCardView[]
        {
            betweenRoundPanel.transform.Find("LargeCardView1").GetComponent<LargeCardView>(),
            betweenRoundPanel.transform.Find("LargeCardView2").GetComponent<LargeCardView>(),
            betweenRoundPanel.transform.Find("LargeCardView3").GetComponent<LargeCardView>()
        };

        for (int i = 0; i < gainedCards.Count; i++)
        {
            largeCardViews[i].gameObject.SetActive(true);
            int randomIndex = Random.Range(0, gainedCards.Count);
            Card selectedCard = gainedCards[randomIndex];
            gainedCards.RemoveAt(randomIndex);
            largeCardViews[i].GetComponent<LargeCardView>().SetCard(selectedCard, false);

            while (i == 1 && gainedCards.Count > 0 && largeCardViews[i].card.Name == largeCardViews[0].card.Name)
            {
                randomIndex = Random.Range(0, gainedCards.Count);
                selectedCard = gainedCards[randomIndex];
                gainedCards.RemoveAt(randomIndex);
                largeCardViews[i].GetComponent<LargeCardView>().SetCard(selectedCard, false);
            }

            while (i == 2 && gainedCards.Count > 0 && 
                (largeCardViews[i].card.Name == largeCardViews[0].card.Name ||
                largeCardViews[i].card.Name == largeCardViews[1].card.Name))
            {
                randomIndex = Random.Range(0, gainedCards.Count);
                selectedCard = gainedCards[randomIndex];
                gainedCards.RemoveAt(randomIndex);
                largeCardViews[i].GetComponent<LargeCardView>().SetCard(selectedCard, false);
            }

            if (gainedCards.Count == 0)
            {
                largeCardViews[i].GetComponent<LargeCardView>().SetCard(new TreasureCard("Copper"), false);
            }
            
        }
    }
}
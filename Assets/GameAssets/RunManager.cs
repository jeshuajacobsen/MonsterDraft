using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;
using UnityEngine.Events;

public class RunManager : MonoBehaviour
{
    
    public RunDeck runDeck;

    [SerializeField] private GameObject roundPanel;

    public GameObject SelectedLargeCardView;
    public GameObject betweenRoundPanel;

    public Button doneButton;

    public DungeonLevelData currentDungeonLevel;
    public int currentDungeonIndex = 1;

    public UnityEvent<DungeonLevelData, int> startRoundEvent = new UnityEvent<DungeonLevelData, int>();

    private GameManager _gameManager;
    private RoundManager _roundManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, RoundManager roundManager, DiContainer container)
    {
        _gameManager = gameManager;
        _roundManager = roundManager;
        _container = container;
    }

    public void Start()
    {
        _gameManager.startRunEvent.AddListener(StartRun);
        doneButton.onClick.AddListener(PickCard);
    }

    public void Update()
    {
        if (!betweenRoundPanel.activeSelf) return;

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
            RectTransform[] largeCardViews = new RectTransform[]
            {
                betweenRoundPanel.transform.Find("LargeCardView1").GetComponent<RectTransform>(),
                betweenRoundPanel.transform.Find("LargeCardView2").GetComponent<RectTransform>(),
                betweenRoundPanel.transform.Find("LargeCardView3").GetComponent<RectTransform>()
            };

            for (int i = 0; i < largeCardViews.Length; i++)
            {
                var cardView = largeCardViews[i];
                if (RectTransformUtility.RectangleContainsScreenPoint(cardView, pointerPosition, Camera.main))
                {
                    if (SelectedLargeCardView == cardView.gameObject)
                    {
                        SelectedLargeCardView = null;
                        betweenRoundPanel.transform.Find("CardViewSelectedBackground" + (i + 1)).gameObject.SetActive(false);
                        betweenRoundPanel.transform.Find("PromptText").gameObject.SetActive(true);
                        betweenRoundPanel.transform.Find("DoneButton").gameObject.SetActive(false);
                        break;
                    }
                    else
                    {
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
        runDeck = new RunDeck(_gameManager.selectedInitialDeck);
        currentDungeonIndex = 1;
        betweenRoundPanel.SetActive(false);
        StartRound();
    }

//TODO check if I can get rid of this dependency
    public void StartRound()
    {
        roundPanel.gameObject.SetActive(true);
        startRoundEvent.Invoke(currentDungeonLevel, currentDungeonIndex);
    }

    public void EndRound(List<Card> gainedCards)
    {   
        _gameManager.PrestigePoints += currentDungeonLevel.GetDungeonData(currentDungeonIndex).PrestigeReward;
        currentDungeonIndex++;
        if (currentDungeonIndex <= currentDungeonLevel.dungeons.Count)
        {
            roundPanel.gameObject.SetActive(false);
            betweenRoundPanel.gameObject.SetActive(true);
            SelectCards(gainedCards);
        } else {
            EndRunWin();
        }
        _gameManager.SaveGame();
        
    }

    public void EndRunWin()
    {
        _gameManager.menuPanel.SetActive(true);
        roundPanel.gameObject.SetActive(false);
        _gameManager.unlockedDungeonLevels.Add(
            _gameManager.gameData.GetNextDungeonLevel(currentDungeonLevel.key));
    }

    public void EndRoundLose()
    {
        _gameManager.menuPanel.SetActive(true);
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
            largeCardViews[i].GetComponent<LargeCardView>().SetCard(selectedCard, new Vector2(0, 0), false);

            while (i == 1 && gainedCards.Count > 0 && largeCardViews[i].card.Name == largeCardViews[0].card.Name)
            {
                randomIndex = Random.Range(0, gainedCards.Count);
                selectedCard = gainedCards[randomIndex];
                gainedCards.RemoveAt(randomIndex);
                largeCardViews[i].GetComponent<LargeCardView>().SetCard(selectedCard, new Vector2(0, 0), false);
            }

            while (i == 2 && gainedCards.Count > 0 && 
                (largeCardViews[i].card.Name == largeCardViews[0].card.Name ||
                largeCardViews[i].card.Name == largeCardViews[1].card.Name))
            {
                randomIndex = Random.Range(0, gainedCards.Count);
                selectedCard = gainedCards[randomIndex];
                gainedCards.RemoveAt(randomIndex);
                largeCardViews[i].GetComponent<LargeCardView>().SetCard(selectedCard, new Vector2(0, 0), false);
            }

            if (gainedCards.Count == 0)
            {
                TreasureCard treasureCard = _container.Instantiate<TreasureCard>();
                treasureCard.Initialize("Copper", _gameManager.cardLevels["Copper"]);
                largeCardViews[i].GetComponent<LargeCardView>().SetCard(treasureCard, new Vector2(0, 0), false);
            }
            
        }
    }
}
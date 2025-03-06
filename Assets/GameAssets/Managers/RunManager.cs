using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;
using UnityEngine.Events;

public class RunManager : MonoBehaviour
{
    
    public RunDeck runDeck;

    [SerializeField] private GameObject _roundPanel;
    [SerializeField] private GameObject _menuPanel;

    private GameObject SelectedLargeCardView;
    [SerializeField] private GameObject betweenRoundPanel;

    public Button doneButton;

    public DungeonLevelData currentDungeonLevel;
    public int currentDungeonIndex = 1;
    public int roundNumber = 1;

    public UnityEvent startRoundEvent = new UnityEvent();

    private IGameManager _gameManager;
    private RoundManager _roundManager;
    private CardFactory _CardFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(IGameManager gameManager, RoundManager roundManager, CardFactory cardFactory, DiContainer container)
    {
        _gameManager = gameManager;
        _roundManager = roundManager;
        _CardFactory = cardFactory;
        _container = container;
    }

    public void Start()
    {
        _gameManager.StartRunEvent.AddListener(StartRun);
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
        runDeck = new RunDeck(_gameManager.SelectedInitialDeck);
        currentDungeonIndex = 1;
        betweenRoundPanel.SetActive(false);
        StartRound();
    }

//TODO check if I can get rid of this dependency
    public void StartRound()
    {
        _roundPanel.gameObject.SetActive(true);
        startRoundEvent.Invoke();
    }

    public void EndRound()
    {   
        _gameManager.PrestigePoints += currentDungeonLevel.GetDungeonData(currentDungeonIndex).PrestigeReward;
        currentDungeonIndex++;
        if (currentDungeonIndex <= currentDungeonLevel.dungeons.Count)
        {
            _roundPanel.gameObject.SetActive(false);
            betweenRoundPanel.gameObject.SetActive(true);
            SelectCards(_roundManager.cardsGainedThisRound);
        } else {
            EndRunWin();
        }
        _gameManager.SaveGame();
        
    }

    public void EndRunWin()
    {
        _menuPanel.SetActive(true);
        _roundPanel.gameObject.SetActive(false);
        _gameManager.AddUnlockedDungeonLevel(
            _gameManager.GameData.GetNextDungeonLevel(currentDungeonLevel.key));
    }

    public void EndRoundLose()
    {
        _menuPanel.SetActive(true);
        _roundPanel.gameObject.SetActive(false);
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
                Card treasureCard = _CardFactory.CreateCard("Copper", _gameManager.CardLevels["Copper"]);
                largeCardViews[i].GetComponent<LargeCardView>().SetCard(treasureCard, new Vector2(0, 0), false);
            }
            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class RoundUIManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject roundPanel;
    public GameObject gemStorePanel;
    public GameObject boostsPanel;
    public GameObject dungeonPanel;
    public GameObject townPanel;
    public GameObject selectingOptionPanel;
    public GameObject monsterOptionPanel;
    public GameObject turnOptionsPanel;
    [SerializeField] private GameObject DeckDiscardPanel;
    public Button doneButton;
    public Button cancelButton;
    public GameObject messagePanel;
    public LargeMonsterView largeMonsterView1;
    public LargeMonsterView largeMonsterView2;
    public LargeMonsterView largeMonsterView3;

    [SerializeField] private OptionButton optionButtonPrefab;
    [SerializeField] private SmallCardView SmallCardViewPrefab;

    private DiContainer _container;
    private RoundManager _roundManager;
    private GameManager _gameManager;

    [Inject]
    public void Construct(RoundManager roundManager, GameManager gameManager, DiContainer container)
    {
        _roundManager = roundManager;
        _gameManager = gameManager;
        _container = container;
    }

    public void Start()
    {
        roundPanel.transform.Find("BoostMenuButton").GetComponent<Button>().onClick.AddListener(() => {
            if (boostsPanel.activeSelf)
            {
                boostsPanel.SetActive(false);
                gemStorePanel.SetActive(false);
            } else {
                boostsPanel.SetActive(true);
            }
        });
        boostsPanel.transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(() => {
            roundPanel.transform.Find("BoostsPanel").gameObject.SetActive(false);
        });
        gemStorePanel.transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(() => {
            gemStorePanel.SetActive(false);
        });
        roundPanel.transform.Find("BoostsPanel").Find("GemStoreButton").GetComponent<Button>().onClick.AddListener(() => {
            gemStorePanel.SetActive(true);
        });

        turnOptionsPanel.transform.Find("TownButton").GetComponent<Button>().onClick.AddListener(() => {
            OpenTownPanel();
        });
        turnOptionsPanel.transform.Find("DungeonButton").GetComponent<Button>().onClick.AddListener(() => {
            OpenDungeonPanel();
        });
    }

    public void UpdateCoins(int value)
    {
        DeckDiscardPanel.transform.Find("CoinsImage/Text").GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void UpdateMana(int value)
    {
        DeckDiscardPanel.transform.Find("ManaImage/Text").GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void UpdateActions(int value)
    {
        DeckDiscardPanel.transform.Find("ActionsImage/Text").GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void UpdateExperience(int value)
    {
        DeckDiscardPanel.transform.Find("ExperienceImage/Text").GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void OpenTownPanel()
    {
        townPanel.SetActive(true);
        dungeonPanel.SetActive(false);
        boostsPanel.SetActive(false);
    }

    public void OpenDungeonPanel()
    {
        townPanel.SetActive(false);
        dungeonPanel.SetActive(true);
        boostsPanel.SetActive(false);
    }

    public void SetupOptionPanel(List<string> options, List<Card> displayCards)
    {
        selectingOptionPanel.SetActive(true);
        Transform cardSection = selectingOptionPanel.transform.Find("CardSection");
        Transform buttonSection = selectingOptionPanel.transform.Find("ButtonSection");
        Transform noCardsText = selectingOptionPanel.transform.Find("NoCardText");
        noCardsText.gameObject.SetActive(false);
        foreach (Transform child in cardSection)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in buttonSection)
        {
            Destroy(child.gameObject);
        }
        foreach (string option in options)
        {
            OptionButton optionButton = _container.InstantiatePrefabForComponent<OptionButton>(optionButtonPrefab, buttonSection);
            optionButton.Initialize(option, displayCards);
            optionButton.GetComponent<Button>().onClick.AddListener(OnOptionSelected);
        }
        if (displayCards.Count == 0)
        {
            cardSection.gameObject.SetActive(false);
            noCardsText.gameObject.SetActive(true);
            noCardsText.GetComponent<TextMeshProUGUI>().text = "No cards found.";
        } else {
            cardSection.gameObject.SetActive(true);
            noCardsText.gameObject.SetActive(false);
            foreach (Card card in displayCards)
            {
                SmallCardView cardView = _container.InstantiatePrefabForComponent<SmallCardView>(SmallCardViewPrefab, cardSection);
                cardView.Initialize(card);
            }
        }
    }

    public void SetupOptionPanel(List<string> options)
    {
        selectingOptionPanel.SetActive(true);
        Transform cardSection = selectingOptionPanel.transform.Find("CardSection");
        Transform buttonSection = selectingOptionPanel.transform.Find("ButtonSection");
        Transform noCardsText = selectingOptionPanel.transform.Find("NoCardText");
        noCardsText.gameObject.SetActive(false);
        cardSection.gameObject.SetActive(false);
        foreach (Transform child in cardSection)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in buttonSection)
        {
            Destroy(child.gameObject);
        }
        foreach (string option in options)
        {
            OptionButton optionButton = _container.InstantiatePrefabForComponent<OptionButton>(optionButtonPrefab, buttonSection);
            optionButton.Initialize(option, new List<Card>());
            optionButton.GetComponent<Button>().onClick.AddListener(OnOptionSelected);
        }
    }

    public void OnOptionSelected()
    {
        selectingOptionPanel.SetActive(false);
        if (_roundManager.gameState is MainPhase)
        {
            if (((MainPhase)_roundManager.gameState).playedCard != null)
            {
                _roundManager.gameState.SwitchPhaseState(_container.Instantiate<ResolvingEffectState>());
            } else {
                _roundManager.gameState.SwitchPhaseState(_container.Instantiate<ResolvingOnGainEffectState>());
            }
        }
    }

    public void OpenMonsterOptionPanel(Tile tile, Vector2 pointerPosition)
    {
        monsterOptionPanel.SetActive(true);
        monsterOptionPanel.GetComponent<MonsterOptionsPanel>().SetActiveTile(tile, pointerPosition);
    }

    public void CloseMonsterOptionPanel()
    {
        monsterOptionPanel.SetActive(false);
    }

    public bool IsInMonsterOptionPanel(Vector2 pointerPosition)
    {
        if (monsterOptionPanel.activeSelf)
        {
            RectTransform rectTransform = monsterOptionPanel.GetComponent<RectTransform>();
            
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, pointerPosition, mainCamera))
            {
                return true;
            }
        }
        return false;
    }


    public void SetupDoneButton(bool showCancel = true)
    {
        doneButton.gameObject.SetActive(true);
        doneButton.onClick.RemoveAllListeners();
        doneButton.onClick.AddListener(_roundManager.OnDoneButtonClicked);

        if (showCancel)
        {
            cancelButton.onClick.RemoveAllListeners();
            if (_roundManager.gameState is MainPhase)
            {
                cancelButton.onClick.AddListener(() => {
                    ((MainPhase)_roundManager.gameState).CancelFullPlay();
                });
                cancelButton.gameObject.SetActive(true);
            }
        }
    }

    public void SetupBoostCancelButton(int gemCost)
    {
        cancelButton.gameObject.SetActive(true);
        cancelButton.onClick.RemoveAllListeners();
        if (cancelButton.onClick.GetPersistentEventCount() == 0)
        {
            cancelButton.onClick.AddListener(() => {
                _roundManager.RefundGemsAndReturnToIdle(gemCost);
            });
        }
    }

    public void CleanupDoneButton()
    {
        doneButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        doneButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        messagePanel.gameObject.SetActive(false);
    }

    public void SetGameMessage(string message)
    {
        messagePanel.gameObject.SetActive(true);
        messagePanel.transform.Find("MessageText").GetComponent<TextMeshProUGUI>().text = message;
    }

    public void CloseGameMessage()
    {
        messagePanel.gameObject.SetActive(false);
    }

    public void CloseMonsterInfoPanel()
    {
        largeMonsterView1.gameObject.SetActive(false);
        largeMonsterView2.gameObject.SetActive(false);
        largeMonsterView3.gameObject.SetActive(false);
    }

    public void OpenMonsterInfo(Monster monster, Vector2 position)
    {
        if (largeMonsterView1 != null)
        {
            
            BaseMonsterData currentMonster = _gameManager.gameData.GetBaseMonsterData(monster.name);
            Vector2 firstCardPosition = new Vector2(-1100, -250);
            Vector2 secondCardPosition = new Vector2(0, -250);
            Vector2 thirdCardPosition = new Vector2(1100, -250);

            if (string.IsNullOrEmpty(monster.evolvesFrom) && string.IsNullOrEmpty(monster.evolvesTo))
            {
                largeMonsterView1.SetMonster(monster, position);
                largeMonsterView1.gameObject.SetActive(true);
                return;
            }

            if (!string.IsNullOrEmpty(monster.evolvesTo) && !string.IsNullOrEmpty(monster.evolvesFrom))
            {
                BaseMonsterData evolvesTo = _gameManager.gameData.GetBaseMonsterData(monster.evolvesTo);
                BaseMonsterData evolvesFrom = _gameManager.gameData.GetBaseMonsterData(monster.evolvesFrom);
                
                largeMonsterView1.SetMonsterFromBaseData(evolvesFrom, firstCardPosition);
                largeMonsterView1.gameObject.SetActive(true);
                largeMonsterView2.SetMonster(monster, secondCardPosition, false);
                largeMonsterView2.gameObject.SetActive(true);
                largeMonsterView3.SetMonsterFromBaseData(evolvesTo, thirdCardPosition);
                largeMonsterView3.gameObject.SetActive(true);
                return;
            }
            
            if (!string.IsNullOrEmpty(monster.evolvesTo))
            {
                BaseMonsterData evolvesTo = _gameManager.gameData.GetBaseMonsterData(monster.evolvesTo);
                largeMonsterView1.SetMonster(monster, firstCardPosition, false);
                largeMonsterView1.gameObject.SetActive(true);
                largeMonsterView2.SetMonsterFromBaseData(evolvesTo, secondCardPosition);
                largeMonsterView2.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(evolvesTo.evolvesTo))
                {
                    BaseMonsterData evolvesTo2 = _gameManager.gameData.GetBaseMonsterData(evolvesTo.evolvesTo);
                    largeMonsterView3.SetMonsterFromBaseData(evolvesTo2, thirdCardPosition);
                    largeMonsterView3.gameObject.SetActive(true);
                }
            }
            else if (!string.IsNullOrEmpty(monster.evolvesFrom))
            {
                BaseMonsterData evolvesFrom = _gameManager.gameData.GetBaseMonsterData(monster.evolvesFrom);
                largeMonsterView1.SetMonsterFromBaseData(evolvesFrom, firstCardPosition);
                largeMonsterView1.gameObject.SetActive(true);
                largeMonsterView2.SetMonster(monster, secondCardPosition, false);
                largeMonsterView2.gameObject.SetActive(true);
                if (!string.IsNullOrEmpty(evolvesFrom.evolvesFrom))
                {
                    BaseMonsterData evolvesFrom2 = _gameManager.gameData.GetBaseMonsterData(evolvesFrom.evolvesFrom);
                    largeMonsterView3.SetMonsterFromBaseData(evolvesFrom2, thirdCardPosition);
                    largeMonsterView3.gameObject.SetActive(true);
                }
            }
            
            
        }
    }

}

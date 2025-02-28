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
    public TextMeshProUGUI messageText;

    [SerializeField] private OptionButton optionButtonPrefab;
    [SerializeField] private SmallCardView SmallCardViewPrefab;

    private DiContainer _container;
    private RoundManager _roundManager;

    [Inject]
    public void Construct(RoundManager roundManager, DiContainer container)
    {
        _roundManager = roundManager;
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
}

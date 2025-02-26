using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Events;
using Zenject;

public class GameManager : MonoBehaviour
{

    public GameData gameData;
    public InitialDeck selectedInitialDeck;

    public GameObject menuPanel;
    public GameObject deckEditorPanel;
    public GameObject cardImprovementPanel;
    public GameObject SelectedCardImprovementPanel;
    public GameObject dungeonPanel;
    public GameObject townPanel;
    public List<string> unlockedDungeonLevels;

    public LargeCardView largeCardView1;
    public LargeCardView largeCardView2;
    public LargeCardView largeCardView3;
    private int _prestigePoints;
    [SerializeField] private TextMeshProUGUI prestigeText;
    public UnityEvent OwnedPrestigeChanged = new UnityEvent();
    public int PrestigePoints { 
        get
        {
            return _prestigePoints;
        }
        set
        {
            _prestigePoints = value;
            OwnedPrestigeChanged.Invoke();
        }
    }


    private int _gems;
    public UnityEvent OwnedGemsChanged = new UnityEvent();
    public int Gems {
        get
        {
            return _gems;
        }
        set
        {
            _gems = value;
            OwnedGemsChanged.Invoke();
        }
    }

    public Dictionary<string, int> cardLevels = new Dictionary<string, int>();

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Awake()
    {
    }

    void Start()
    {
        gameData = _container.Instantiate<GameData>().Initialize();

        unlockedDungeonLevels = new List<string>();
        unlockedDungeonLevels.Add("Forest");
        foreach (string cardName in gameData.GetAllMonsterNames())
        {
            cardLevels.Add(cardName, 1);
        }

        foreach (string cardName in gameData.GetAllTreasureNames())
        {
            cardLevels.Add(cardName, 1);
        }

        foreach (string cardName in gameData.GetAllActionNames())
        {
            cardLevels.Add(cardName, 1);
        } 
        selectedInitialDeck = _container.Instantiate<InitialDeck>();
        LoadGame();
    }

    void Update()
    {
        
    }

    public void StartRun()
    {
        menuPanel.SetActive(false); 
        RunManager.instance.StartRun();
    }

    public void OpenDeckEditor()
    {
        menuPanel.SetActive(false);
        deckEditorPanel.GetComponent<DeckEditorPanel>().OnOpen();
        deckEditorPanel.transform.parent.parent.gameObject.SetActive(true);
    }

    public void CloseDeckEditor()
    {
        deckEditorPanel.transform.parent.parent.gameObject.SetActive(false);
        menuPanel.SetActive(true);
        selectedInitialDeck = deckEditorPanel.GetComponent<DeckEditorPanel>().GetSelectedInitialDeck();
        SaveGame();
    }

    public void OpenCardImprovementPanel()
    {
        menuPanel.SetActive(false);
        cardImprovementPanel.transform.parent.parent.gameObject.SetActive(true);
        cardImprovementPanel.GetComponent<CardImprovementPanel>().OnOpen();
    }

    public void CloseCardImprovementPanel()
    {
        cardImprovementPanel.transform.parent.parent.gameObject.SetActive(false);
        menuPanel.SetActive(true);
        SaveGame();
    }

    public void OpenSelectedCardImprovementPanel(Card card)
    {
        SelectedCardImprovementPanel.SetActive(true);
        SelectedCardImprovementPanel.GetComponent<SelectedCardPanel>().OnOpen(card);
    }

    public void CloseSelectedCardImprovementPanel()
    {
        SelectedCardImprovementPanel.SetActive(false);
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData(selectedInitialDeck, unlockedDungeonLevels, PrestigePoints, cardLevels);
        saveData.AddCardsForSaving(deckEditorPanel.GetComponent<DeckEditorPanel>().unlockedCards);
        string jsonData = JsonConvert.SerializeObject(saveData);
        string path = Application.persistentDataPath + "/savefile.json";
        System.IO.File.WriteAllText(path, jsonData);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (System.IO.File.Exists(path))
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(path);
                SaveData saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);
                unlockedDungeonLevels = saveData.unlockedDungeonLevels;
                PrestigePoints = saveData.PrestigePoints;
                deckEditorPanel.GetComponent<DeckEditorPanel>().LoadCards(saveData);
                cardLevels = saveData.cardLevels;
                selectedInitialDeck = _container.Instantiate<InitialDeck>();
                selectedInitialDeck.LoadCards(saveData.initialDeck);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load save file: {ex.Message}");
                ResetToDefaultSaveData();
            }
        }
        else
        {
            ResetToDefaultSaveData();
        }
    }

    private void ResetToDefaultSaveData()
    {
        selectedInitialDeck = _container.Instantiate<InitialDeck>();
        unlockedDungeonLevels = new List<string> { "Forest" };
        PrestigePoints = 0;
        deckEditorPanel.GetComponent<DeckEditorPanel>().FirstTimeSetup();
    }
}

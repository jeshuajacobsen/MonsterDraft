using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Events;
using Zenject;

public class GameManager : MonoBehaviour, IGameManager
{
    private IGameData _GameData;
    private InitialDeck _selectedInitialDeck;

    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _deckEditorPanel;
    [SerializeField] private GameObject _cardImprovementPanel;
    [SerializeField] private GameObject _selectedCardImprovementPanel;
    [SerializeField] private GameObject _townPanel;

    private List<string> _unlockedDungeonLevels = new();
    private Dictionary<string, int> _cardLevels = new();

    [SerializeField] private LargeCardView _largeCardView1;
    [SerializeField] private LargeCardView _largeCardView2;
    [SerializeField] private LargeCardView _largeCardView3;

    private int _prestigePoints;
    private int _gems;

    private UnityEvent _startRunEvent = new();
    private UnityEvent _ownedPrestigeChanged = new();
    private UnityEvent _ownedGemsChanged = new();

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public IGameData GameData => _GameData;
    public InitialDeck SelectedInitialDeck => _selectedInitialDeck;
    public List<string> UnlockedDungeonLevels => _unlockedDungeonLevels;
    public Dictionary<string, int> CardLevels => _cardLevels;

    public UnityEvent StartRunEvent => _startRunEvent;
    public UnityEvent OwnedPrestigeChanged => _ownedPrestigeChanged;
    public UnityEvent OwnedGemsChanged => _ownedGemsChanged;

    public LargeCardView LargeCardView1 => _largeCardView1;
    public LargeCardView LargeCardView2 => _largeCardView2;
    public LargeCardView LargeCardView3 => _largeCardView3;

    public int PrestigePoints
    {
        get => _prestigePoints;
        set
        {
            _prestigePoints = value;
            _ownedPrestigeChanged.Invoke();
        }
    }

    public int Gems
    {
        get => _gems;
        set
        {
            _gems = value;
            _ownedGemsChanged.Invoke();
        }
    }

    public void AddUnlockedDungeonLevel(string dungeonLevel)
    {
        _unlockedDungeonLevels.Add(dungeonLevel);
    }

    private void Start()
    {
        _GameData = _container.Instantiate<GameData>().Initialize();
        _selectedInitialDeck = _container.Instantiate<InitialDeck>();

        _unlockedDungeonLevels.Add("Forest");

        foreach (string cardName in _GameData.GetAllMonsterNames()) _cardLevels[cardName] = 1;
        foreach (string cardName in _GameData.GetAllTreasureNames()) _cardLevels[cardName] = 1;
        foreach (string cardName in _GameData.GetAllActionNames()) _cardLevels[cardName] = 1;

        LoadGame();
    }

    public void StartRun()
    {
        _menuPanel.SetActive(false);
        _startRunEvent.Invoke();
    }

    public void OpenDeckEditor()
    {
        _menuPanel.SetActive(false);
        _deckEditorPanel.GetComponent<DeckEditorPanel>().OnOpen();
        _deckEditorPanel.transform.parent.parent.gameObject.SetActive(true);
    }

    public void CloseDeckEditor()
    {
        _deckEditorPanel.transform.parent.parent.gameObject.SetActive(false);
        _menuPanel.SetActive(true);
        _selectedInitialDeck = _deckEditorPanel.GetComponent<DeckEditorPanel>().GetSelectedInitialDeck();
        SaveGame();
    }

    public void OpenCardImprovementPanel()
    {
        _menuPanel.SetActive(false);
        _cardImprovementPanel.transform.parent.parent.gameObject.SetActive(true);
        _cardImprovementPanel.GetComponent<CardImprovementPanel>().OnOpen();
    }

    public void CloseCardImprovementPanel()
    {
        _cardImprovementPanel.transform.parent.parent.gameObject.SetActive(false);
        _menuPanel.SetActive(true);
        SaveGame();
    }

    public void OpenSelectedCardImprovementPanel(Card card)
    {
        _selectedCardImprovementPanel.SetActive(true);
        _selectedCardImprovementPanel.GetComponent<SelectedCardPanel>().OnOpen(card);
    }

    public void CloseSelectedCardImprovementPanel()
    {
        _selectedCardImprovementPanel.SetActive(false);
    }

    public void SaveGame()
    {
        SaveData saveData = new(_selectedInitialDeck, _unlockedDungeonLevels, PrestigePoints, _cardLevels);
        saveData.AddCardsForSaving(_deckEditorPanel.GetComponent<DeckEditorPanel>().unlockedCards);

        string jsonData = JsonConvert.SerializeObject(saveData);
        string path = Application.persistentDataPath + "/savefile.json";
        System.IO.File.WriteAllText(path, jsonData);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (!System.IO.File.Exists(path))
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(path);
                SaveData saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);
                _unlockedDungeonLevels = saveData.unlockedDungeonLevels;
                PrestigePoints = saveData.PrestigePoints;
                _deckEditorPanel.GetComponent<DeckEditorPanel>().LoadCards(saveData);
                _cardLevels = saveData.cardLevels;
                _selectedInitialDeck = _container.Instantiate<InitialDeck>();
                _selectedInitialDeck.LoadCards(saveData.initialDeck);
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
        _selectedInitialDeck = _container.Instantiate<InitialDeck>();
        _selectedInitialDeck.Initialize();
        _unlockedDungeonLevels = new List<string> { "Forest" };
        PrestigePoints = 0;
        _deckEditorPanel.GetComponent<DeckEditorPanel>().FirstTimeSetup();
    }
}

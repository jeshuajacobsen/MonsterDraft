using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameData gameData = new GameData();
    public InitialDeck selectedInitialDeck;

    public GameObject menuPanel;
    public GameObject deckEditorPanel;
    public GameObject cardImprovementPanel;
    public GameObject dungeonPanel;
    public GameObject townPanel;
    public List<string> unlockedDungeonLevels;

    public LargeCardView largeCardView1;
    public LargeCardView largeCardView2;
    public LargeCardView largeCardView3;
    private int _prestigePoints;
    [SerializeField] private TextMeshProUGUI prestigeText;

    public int PrestigePoints { 
        get
        {
            return _prestigePoints;
        }
        set
        {
            _prestigePoints = value;
            prestigeText.text = _prestigePoints.ToString();
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

    void Start()
    {
        selectedInitialDeck = new InitialDeck();
        unlockedDungeonLevels = new List<string>();
        unlockedDungeonLevels.Add("Forest");
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

    public void SaveGame()
    {
        SaveData saveData = new SaveData(selectedInitialDeck, unlockedDungeonLevels, PrestigePoints);
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

                selectedInitialDeck = new InitialDeck();
                selectedInitialDeck.LoadCards(saveData.initialDeck);
                unlockedDungeonLevels = saveData.unlockedDungeonLevels;
                PrestigePoints = saveData.PrestigePoints;
                deckEditorPanel.GetComponent<DeckEditorPanel>().LoadCards(saveData);
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
        selectedInitialDeck = new InitialDeck();
        unlockedDungeonLevels = new List<string> { "Forest" };
        PrestigePoints = 0;
        deckEditorPanel.GetComponent<DeckEditorPanel>().FirstTimeSetup();
    }
}

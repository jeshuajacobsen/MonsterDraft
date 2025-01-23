using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameData gameData = new GameData();
    public InitialDeck selectedInitialDeck;

    public GameObject menuPanel;
    public GameObject deckEditorPanel;
    public List<string> unlockedDungeonLevels;

    public LargeCardView largeCardView1;
    public LargeCardView largeCardView2;
    public LargeCardView largeCardView3;

    public int prestigePoints = 0;

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
        deckEditorPanel.SetActive(true);
    }
}

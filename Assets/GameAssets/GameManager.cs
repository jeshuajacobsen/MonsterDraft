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
    public List<string> unlockedDungeonLevels;

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
}

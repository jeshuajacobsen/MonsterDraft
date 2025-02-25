using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class MenuPanel : MonoBehaviour
{
    DungeonLevelData dungeonLevelData;
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    void Start()
    {
        transform.Find("StartRunButton").GetComponent<Button>().onClick.AddListener(_gameManager.StartRun);
        transform.Find("ButtonLeft").GetComponent<Button>().onClick.AddListener(ButtonLeft);
        transform.Find("ButtonRight").GetComponent<Button>().onClick.AddListener(ButtonRight);
        transform.Find("EditDeckButton").GetComponent<Button>().onClick.AddListener(_gameManager.OpenDeckEditor);
        transform.Find("ImproveCardsButton").GetComponent<Button>().onClick.AddListener(_gameManager.OpenCardImprovementPanel);
        SetDungeonLevel("Forest");
    }

    
    void Update()
    {
        
    }

    public void SetDungeonLevel(string dungeonLevel)
    {
        dungeonLevelData = _gameManager.gameData.DungeonData(dungeonLevel);
        RunManager.instance.currentDungeonLevel = dungeonLevelData;
        transform.Find("LevelImage").GetComponent<Image>().sprite = SpriteManager.instance.GetLevelSprite(dungeonLevelData.selectedLevelSprite);
        transform.Find("DungeonLevelName").GetComponent<TextMeshProUGUI>().text = dungeonLevelData.name;
        if (_gameManager.gameData.GetPreviousDungeonLevel(dungeonLevelData.key) == null)
        {
            transform.Find("ButtonLeft").gameObject.SetActive(false);
        } else {
            transform.Find("ButtonLeft").gameObject.SetActive(true);
        }
        if (_gameManager.gameData.GetNextDungeonLevel(dungeonLevelData.key) == null)
        {
            transform.Find("ButtonRight").gameObject.SetActive(false);
        } else {
            transform.Find("ButtonRight").gameObject.SetActive(true);
        }

        if (_gameManager.unlockedDungeonLevels.Contains(dungeonLevelData.key))
        {
            transform.Find("StartRunButton").GetComponent<Button>().interactable = true;
            transform.Find("LockedPanel").gameObject.SetActive(false);
        } else {
            transform.Find("StartRunButton").GetComponent<Button>().interactable = false;
            transform.Find("LockedPanel").gameObject.SetActive(true);
        }
    }

    public void ButtonRight()
    {
        string nextDungeonLevel = _gameManager.gameData.GetNextDungeonLevel(dungeonLevelData.key);
        SetDungeonLevel(nextDungeonLevel);
    }

    public void ButtonLeft()
    {
        string previousDungeonLevel = _gameManager.gameData.GetPreviousDungeonLevel(dungeonLevelData.key);
        SetDungeonLevel(previousDungeonLevel);
    }

}

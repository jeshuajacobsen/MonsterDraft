using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class MenuPanel : MonoBehaviour
{
    DungeonLevelData dungeonLevelData;
    private IGameManager _gameManager;
    private RunManager _runManager;
    private SpriteManager _spriteManager;

    [Inject]
    public void Construct(IGameManager gameManager, RunManager runManager, SpriteManager spriteManager)
    {
        _gameManager = gameManager;
        _runManager = runManager;
        _spriteManager = spriteManager;
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
        dungeonLevelData = _gameManager.GameData.DungeonData(dungeonLevel);
        _runManager.currentDungeonLevel = dungeonLevelData;
        transform.Find("LevelImage").GetComponent<Image>().sprite = _spriteManager.GetLevelSprite(dungeonLevelData.selectedLevelSprite);
        transform.Find("DungeonLevelName").GetComponent<TextMeshProUGUI>().text = dungeonLevelData.name;
        if (_gameManager.GameData.GetPreviousDungeonLevel(dungeonLevelData.key) == null)
        {
            transform.Find("ButtonLeft").gameObject.SetActive(false);
        } else {
            transform.Find("ButtonLeft").gameObject.SetActive(true);
        }
        if (_gameManager.GameData.GetNextDungeonLevel(dungeonLevelData.key) == null)
        {
            transform.Find("ButtonRight").gameObject.SetActive(false);
        } else {
            transform.Find("ButtonRight").gameObject.SetActive(true);
        }

        if (_gameManager.UnlockedDungeonLevels.Contains(dungeonLevelData.key))
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
        string nextDungeonLevel = _gameManager.GameData.GetNextDungeonLevel(dungeonLevelData.key);
        SetDungeonLevel(nextDungeonLevel);
    }

    public void ButtonLeft()
    {
        string previousDungeonLevel = _gameManager.GameData.GetPreviousDungeonLevel(dungeonLevelData.key);
        SetDungeonLevel(previousDungeonLevel);
    }

}

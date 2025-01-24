using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPanel : MonoBehaviour
{
    DungeonLevelData dungeonLevelData;
    void Start()
    {
        transform.Find("StartRunButton").GetComponent<Button>().onClick.AddListener(GameManager.instance.StartRun);
        transform.Find("ButtonLeft").GetComponent<Button>().onClick.AddListener(ButtonLeft);
        transform.Find("ButtonRight").GetComponent<Button>().onClick.AddListener(ButtonRight);
        transform.Find("EditDeckButton").GetComponent<Button>().onClick.AddListener(GameManager.instance.OpenDeckEditor);
        SetDungeonLevel("Forest");
    }

    
    void Update()
    {
        
    }

    public void SetDungeonLevel(string dungeonLevel)
    {
        dungeonLevelData = GameManager.instance.gameData.DungeonData(dungeonLevel);
        RunManager.instance.currentDungeonLevel = dungeonLevelData;
        transform.Find("LevelImage").GetComponent<Image>().sprite = SpriteManager.instance.GetLevelSprite(dungeonLevelData.selectedLevelSprite);
        transform.Find("DungeonLevelName").GetComponent<TextMeshProUGUI>().text = dungeonLevelData.name;
        if (GameManager.instance.gameData.GetPreviousDungeonLevel(dungeonLevelData.key) == null)
        {
            transform.Find("ButtonLeft").gameObject.SetActive(false);
        } else {
            transform.Find("ButtonLeft").gameObject.SetActive(true);
        }
        if (GameManager.instance.gameData.GetNextDungeonLevel(dungeonLevelData.key) == null)
        {
            transform.Find("ButtonRight").gameObject.SetActive(false);
        } else {
            transform.Find("ButtonRight").gameObject.SetActive(true);
        }

        if (GameManager.instance.unlockedDungeonLevels.Contains(dungeonLevelData.key))
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
        string nextDungeonLevel = GameManager.instance.gameData.GetNextDungeonLevel(dungeonLevelData.key);
        SetDungeonLevel(nextDungeonLevel);
    }

    public void ButtonLeft()
    {
        string previousDungeonLevel = GameManager.instance.gameData.GetPreviousDungeonLevel(dungeonLevelData.key);
        SetDungeonLevel(previousDungeonLevel);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TabButton : MonoBehaviour
{

    [SerializeField] private string tabName;
    
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        if (tabName == "Dungeon")
        {
            _gameManager.dungeonPanel.transform.gameObject.SetActive(true);
            _gameManager.townPanel.transform.gameObject.SetActive(false);
        } else if (tabName == "Town")
        {
            _gameManager.dungeonPanel.transform.gameObject.SetActive(false);
            _gameManager.townPanel.transform.gameObject.SetActive(true);
        }
    }
}

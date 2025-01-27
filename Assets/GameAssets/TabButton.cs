using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{

    [SerializeField] private string tabName;
    // Start is called before the first frame update
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
            GameManager.instance.dungeonPanel.transform.gameObject.SetActive(true);
            GameManager.instance.townPanel.transform.gameObject.SetActive(false);
        } else if (tabName == "Town")
        {
            GameManager.instance.dungeonPanel.transform.gameObject.SetActive(false);
            GameManager.instance.townPanel.transform.gameObject.SetActive(true);
        }
    }
}

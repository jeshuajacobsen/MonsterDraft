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
            transform.parent.parent.Find("DungeonPanel").gameObject.SetActive(true);
            transform.parent.parent.Find("TownPanel").gameObject.SetActive(false);
        } else if (tabName == "Town")
        {
            transform.parent.parent.Find("DungeonPanel").gameObject.SetActive(false);
            transform.parent.parent.Find("TownPanel").gameObject.SetActive(true);
        }
    }
}

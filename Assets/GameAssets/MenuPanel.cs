using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("StartRunButton").GetComponent<Button>().onClick.AddListener(GameManager.instance.StartRun);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

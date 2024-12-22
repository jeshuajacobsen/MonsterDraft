using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterOptionButton : MonoBehaviour
{
    public string option;
    private Monster monster;
    
    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(Monster monster, string option)
    {
        this.monster = monster;
        this.option = option;
        if (option == "Movement")
        {
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = "Move " + monster.Movement;
        }
        else if (option == "Attack")
        {
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = "Attack";
        }
    }

    public void OnClick()
    {
        Debug.Log("Clicked " + option);
        if (option == "Movement")
        {
            RoundManager.instance.MoveMonster(monster);
        }
        else if (option == "Attack")
        {
            //RoundManager.instance.AttackMonster();
        }
    }
}

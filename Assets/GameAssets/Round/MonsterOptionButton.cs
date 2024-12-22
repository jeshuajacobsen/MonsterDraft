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
        else if (option == "Skill1")
        {
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = monster.skill1.name;
        }
        else if (option == "Skill2")
        {
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = monster.skill2.name;
        }

        if (CheckForMonster(monster.tileOn, 1) != null)
        {
            transform.GetComponent<Button>().interactable = false;
        }


    }

    public void OnClick()
    {
        Debug.Log("Clicked " + option);
        if (option == "Movement")
        {
            RoundManager.instance.MoveMonster(monster);
            RoundManager.instance.monsterOptionPanel.gameObject.SetActive(false);
        }
        else if (option == "Skill1")
        {

            //RoundManager.instance.AttackMonster();
            RoundManager.instance.monsterOptionPanel.gameObject.SetActive(false);
        }
        else if (option == "Skill2")
        {
            //RoundManager.instance.AttackMonster();
            RoundManager.instance.monsterOptionPanel.gameObject.SetActive(false);
        }
    }

    public Monster CheckForMonster(Tile currentTile, int distance)
    {
        Tile nextTile = currentTile.dungeonRow.GetNextTile(currentTile, distance);
        if (nextTile != null && nextTile.monster != null)
        {
            return nextTile.monster;
        }
        return null;
    }
}

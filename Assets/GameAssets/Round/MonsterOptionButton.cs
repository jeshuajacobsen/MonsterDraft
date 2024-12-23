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
            if (RoundManager.CheckForMonster(monster.tileOn, 1) != null)
            {
                transform.GetComponent<Button>().interactable = false;
            }
        }
        else if (option == "Skill1")
        {
            transform.GetComponent<Button>().interactable = false;
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = monster.skill1.name;
            for (int i = 1; i <= monster.skill1.Range; i++)
            {
                if (RoundManager.CheckForMonster(monster.tileOn, i) != null)
                {
                    transform.GetComponent<Button>().interactable = true;
                }
            }
        }
        else if (option == "Skill2")
        {
            transform.GetComponent<Button>().interactable = false;
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = monster.skill2.name;
            for (int i = 1; i <= monster.skill1.Range; i++)
            {
                if (RoundManager.CheckForMonster(monster.tileOn, i) != null)
                {
                    transform.GetComponent<Button>().interactable = true;
                }
            }
        }

        


    }

    public void OnClick()
    {
        if (option == "Movement")
        {
            RoundManager.instance.MoveMonster(monster);
            RoundManager.instance.monsterOptionPanel.gameObject.SetActive(false);
        }
        else if (option == "Skill1")
        {

            RoundManager.instance.UseSkill(monster, monster.skill1);
            RoundManager.instance.monsterOptionPanel.gameObject.SetActive(false);
        }
        else if (option == "Skill2")
        {
            RoundManager.instance.UseSkill(monster, monster.skill2);
            RoundManager.instance.monsterOptionPanel.gameObject.SetActive(false);
        }
    }
}

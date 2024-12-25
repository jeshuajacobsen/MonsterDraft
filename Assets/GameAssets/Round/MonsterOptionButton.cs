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

//TODO: the monsterOptionsPanel does some checks. I should combine them here.
    public void InitValues(Monster monster, string option)
    {
        this.monster = monster;
        this.option = option;
        if (option == "Movement")
        {
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = "Move " + monster.Movement;
            if (RoundManager.CheckForMonster(monster.tileOn, 1) != null && monster.tileOn.name != "Tile7")
            {
                transform.GetComponent<Button>().interactable = false;
            }
        }
        else if (option == "Skill1")
        {
            transform.GetComponent<Button>().interactable = false;
            if (RoundManager.instance.Mana >= monster.skill1.ManaCost)
            {
                transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = monster.skill1.name;
                for (int i = 1; i <= monster.skill1.Range; i++)
                {
                    int tileNumber = int.Parse(monster.tileOn.name.Replace("Tile", ""));
                    Monster monsterOnTile = RoundManager.CheckForMonster(monster.tileOn, i);
                    if ((monsterOnTile != null && monsterOnTile.team == "Enemy") || tileNumber + i > 7)
                    {
                        transform.GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
        else if (option == "Skill2")
        {
            transform.GetComponent<Button>().interactable = false;
            if (RoundManager.instance.Mana >= monster.skill2.ManaCost)
            {
                transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = monster.skill2.name;
                for (int i = 1; i <= monster.skill2.Range; i++)
                {
                    int tileNumber = int.Parse(monster.tileOn.name.Replace("Tile", ""));
                    Monster monsterOnTile = RoundManager.CheckForMonster(monster.tileOn, i);
                    if ((monsterOnTile != null && monsterOnTile.team == "Enemy") || tileNumber + i > 7)
                    {
                        transform.GetComponent<Button>().interactable = true;
                    }
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

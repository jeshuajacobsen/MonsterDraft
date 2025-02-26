using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class MonsterOptionButton : MonoBehaviour
{
    public string option;
    private Monster monster;
    
    private RoundManager _roundManager;

    [Inject]
    public void Construct(RoundManager roundManager)
    {
        _roundManager = roundManager;
    }

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//TODO: the monsterOptionsPanel does some checks. I should combine them here.
    public void Initialize(Monster monster, string option)
    {
        this.monster = monster;
        this.option = option;

        int additionalSkills = 0;
        foreach (var effect in _roundManager.persistentEffects)
        {
            if (effect.type == "AdditionalSkills")
            {
                additionalSkills = effect.amount;
            }
        }

        if (option == "Skill1")
        {
            transform.GetComponent<Button>().interactable = false;
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = monster.skill1.name;
            transform.Find("CostBackgroundImage/CostText").GetComponent<TextMeshProUGUI>().text = monster.skill1.ManaCost.ToString();
            SkillDirections skillDirections = transform.Find("SkillDirections/Panel").GetComponent<SkillDirections>();
            skillDirections.SetDirections(monster.skill1.directions);
            int tileIndex = int.Parse(monster.tileOn.name.Replace("Tile", ""));
            if (_roundManager.Mana >= monster.skill1.ManaCost && 
                monster.actionsUsedThisTurn.Count < additionalSkills + 1)
            {
                if (_roundManager.GetValidTargets(monster, monster.skill1).Count > 0 || tileIndex + monster.skill1.Range > 7 || monster.skill1.directions == "")
                {
                    transform.GetComponent<Button>().interactable = true;
                }
                
            }
        }
        else if (option == "Skill2")
        {
            transform.GetComponent<Button>().interactable = false;
            transform.Find("MoveNameText").GetComponent<TextMeshProUGUI>().text = monster.skill2.name;
            transform.Find("CostBackgroundImage/CostText").GetComponent<TextMeshProUGUI>().text = monster.skill2.ManaCost.ToString();
            SkillDirections skillDirections = transform.Find("SkillDirections/Panel").GetComponent<SkillDirections>();
            skillDirections.SetDirections(monster.skill2.directions);
            int tileIndex = int.Parse(monster.tileOn.name.Replace("Tile", ""));
            if (_roundManager.Mana >= monster.skill2.ManaCost && 
                monster.actionsUsedThisTurn.Count < additionalSkills + 1)
            {
                if (_roundManager.GetValidTargets(monster, monster.skill2).Count > 0 || tileIndex + monster.skill2.Range > 7 || monster.skill2.directions == "")
                {
                    transform.GetComponent<Button>().interactable = true;
                }
                
            }
        }

        


    }

    public void OnClick()
    {
        if (option == "Skill1")
        {

            _roundManager.SelectSkill(monster, monster.skill1);
            _roundManager.monsterOptionPanel.gameObject.SetActive(false);
        }
        else if (option == "Skill2")
        {
            _roundManager.SelectSkill(monster, monster.skill2);
            _roundManager.monsterOptionPanel.gameObject.SetActive(false);
        }
    }
}

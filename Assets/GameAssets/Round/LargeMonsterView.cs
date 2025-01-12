using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LargeMonsterView : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMonster(Monster monster, Vector2 pointerPosition)
    {
        // Basic UI updates
        transform.Find("MonsterName").GetComponent<TextMeshProUGUI>().text = monster.name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite(monster.name);
        
        transform.Find("ManaImage/Text").GetComponent<TextMeshProUGUI>().text = monster.ManaCost.ToString();
        transform.Find("StatsPanel/AttackText").GetComponent<TextMeshProUGUI>().text = monster.Attack.ToString();
        transform.Find("StatsPanel/HealthText").GetComponent<TextMeshProUGUI>().text = 
            monster.Health.ToString() + "/" + monster.MaxHealth.ToString();
        transform.Find("StatsPanel/DefenseText").GetComponent<TextMeshProUGUI>().text = monster.Defense.ToString();
        transform.Find("StatsPanel/SpeedText").GetComponent<TextMeshProUGUI>().text = monster.Movement.ToString();

        // Skill 1
        transform.Find("SkillsPanel/Skill1NameText").GetComponent<TextMeshProUGUI>().text = monster.skill1.name;
        transform.Find("SkillsPanel/Skill1Text").GetComponent<TextMeshProUGUI>().text = monster.skill1.Description;
        transform.Find("SkillsPanel/Skill1ManaCost/Text").GetComponent<TextMeshProUGUI>().text = monster.skill1.ManaCost.ToString();
        transform.Find("SkillsPanel/Skill1RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + monster.skill1.Range;
        transform.Find("SkillsPanel/Skill1DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + monster.skill1.Damage;

        // Skill 2
        transform.Find("SkillsPanel/Skill2NameText").GetComponent<TextMeshProUGUI>().text = monster.skill2.name;
        transform.Find("SkillsPanel/Skill2Text").GetComponent<TextMeshProUGUI>().text = monster.skill2.Description;
        transform.Find("SkillsPanel/Skill2ManaCost/Text").GetComponent<TextMeshProUGUI>().text = monster.skill2.ManaCost.ToString();
        transform.Find("SkillsPanel/Skill2RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + monster.skill2.Range;
        transform.Find("SkillsPanel/Skill2DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + monster.skill2.Damage;

        // Decide left vs. right alignment purely in 2D
        if (pointerPosition.x < 150f)
        {
            AlignLeft(pointerPosition);
        }
        else
        {
            AlignRight(pointerPosition);
        }
    }

    public void SetMonsterFromBaseData(BaseMonsterData monster, Vector2 pointerPosition)
    {
        transform.Find("MonsterName").GetComponent<TextMeshProUGUI>().text = monster.name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite(monster.name);
        
        transform.Find("ManaImage/Text").GetComponent<TextMeshProUGUI>().text = monster.ManaCost.ToString();
        transform.Find("StatsPanel/AttackText").GetComponent<TextMeshProUGUI>().text = monster.Attack.ToString();
        transform.Find("StatsPanel/HealthText").GetComponent<TextMeshProUGUI>().text = 
            monster.Health.ToString() + "/" + monster.Health.ToString();
        transform.Find("StatsPanel/DefenseText").GetComponent<TextMeshProUGUI>().text = monster.Defense.ToString();
        transform.Find("StatsPanel/SpeedText").GetComponent<TextMeshProUGUI>().text = monster.Movement.ToString();

        // Skill 1
        SkillData skill1 = GameManager.instance.gameData.GetSkill(monster.skill1Name);
        transform.Find("SkillsPanel/Skill1NameText").GetComponent<TextMeshProUGUI>().text = skill1.name;
        transform.Find("SkillsPanel/Skill1Text").GetComponent<TextMeshProUGUI>().text = skill1.Description;
        transform.Find("SkillsPanel/Skill1ManaCost/Text").GetComponent<TextMeshProUGUI>().text = skill1.ManaCost.ToString();
        transform.Find("SkillsPanel/Skill1RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + skill1.Range;
        transform.Find("SkillsPanel/Skill1DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + skill1.Damage;

        // Skill 2
        SkillData skill2 = GameManager.instance.gameData.GetSkill(monster.skill2Name);
        transform.Find("SkillsPanel/Skill2NameText").GetComponent<TextMeshProUGUI>().text = skill2.name;
        transform.Find("SkillsPanel/Skill2Text").GetComponent<TextMeshProUGUI>().text = skill2.Description;
        transform.Find("SkillsPanel/Skill2ManaCost/Text").GetComponent<TextMeshProUGUI>().text = skill2.ManaCost.ToString();
        transform.Find("SkillsPanel/Skill2RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + skill2.Range;
        transform.Find("SkillsPanel/Skill2DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + skill2.Damage;

        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = pointerPosition;
    }

    public void AlignRight(Vector2 pointerPosition)
    {
        RectTransform rt = GetComponent<RectTransform>();
        RectTransform parentRect = rt.parent as RectTransform;

        // Convert screen coords to local coords
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            pointerPosition,
            mainCamera,
            out Vector2 localPoint))
        {
            float objectWidth = rt.rect.width * rt.localScale.x;
            Vector2 offset = new Vector2(objectWidth * 0.5f, 0f);

            rt.anchoredPosition = new Vector2(localPoint.x - offset.x, rt.anchoredPosition.y);
        }
    }


    public void AlignLeft(Vector2 pointerPosition)
    {
        RectTransform rt = GetComponent<RectTransform>();
        RectTransform parentRect = rt.parent as RectTransform;

        // Convert screen coords to local coords
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            pointerPosition,
            mainCamera,
            out Vector2 localPoint))
        {
            float objectWidth = rt.rect.width * rt.localScale.x;
            Vector2 offset = new Vector2(objectWidth * 0.5f, 0f);

            rt.anchoredPosition = new Vector2(localPoint.x + offset.x, rt.anchoredPosition.y);
        }
    }

}

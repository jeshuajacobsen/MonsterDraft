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

    public void SetMonster(Monster monster)
    {
        transform.Find("MonsterName").GetComponent<TextMeshProUGUI>().text = monster.name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetCardSprite(monster.name);
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
                                            mainCamera.nearClipPlane + 1.0f);
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        if (mouseWorldPos.x < -900)
        {
            AlignLeftWithMouse();
        } else {
            AlignRightWithMouse();
        }

        transform.Find("ManaImage/Text").GetComponent<TextMeshProUGUI>().text = monster.ManaCost.ToString();
        transform.Find("StatsPanel/AttackText").GetComponent<TextMeshProUGUI>().text = monster.Attack.ToString();
        transform.Find("StatsPanel/HealthText").GetComponent<TextMeshProUGUI>().text = monster.Health.ToString() + "/" + monster.MaxHealth.ToString();
        transform.Find("StatsPanel/DefenseText").GetComponent<TextMeshProUGUI>().text = monster.Defense.ToString();
        transform.Find("StatsPanel/SpeedText").GetComponent<TextMeshProUGUI>().text = monster.Movement.ToString();
        transform.Find("SkillsPanel/Skill1NameText").GetComponent<TextMeshProUGUI>().text = monster.skill1.name;
        transform.Find("SkillsPanel/Skill1Text").GetComponent<TextMeshProUGUI>().text = monster.skill1.Description;
        transform.Find("SkillsPanel/Skill1ManaCost/Text").GetComponent<TextMeshProUGUI>().text = monster.skill1.ManaCost.ToString();
        transform.Find("SkillsPanel/Skill1RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + monster.skill1.Range.ToString();
        transform.Find("SkillsPanel/Skill1DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + monster.skill1.Damage.ToString();
        transform.Find("SkillsPanel/Skill1Text").GetComponent<TextMeshProUGUI>().text = monster.skill2.Description;
        transform.Find("SkillsPanel/Skill2NameText").GetComponent<TextMeshProUGUI>().text = monster.skill2.name;
        transform.Find("SkillsPanel/Skill2Text").GetComponent<TextMeshProUGUI>().text = monster.skill2.Description;
        transform.Find("SkillsPanel/Skill2ManaCost/Text").GetComponent<TextMeshProUGUI>().text = monster.skill2.ManaCost.ToString();
        transform.Find("SkillsPanel/Skill2RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + monster.skill2.Range.ToString();
        transform.Find("SkillsPanel/Skill2DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + monster.skill2.Damage.ToString();
        transform.Find("SkillsPanel/Skill2Text").GetComponent<TextMeshProUGUI>().text = monster.skill2.Description;
        
    }

    void AlignRightWithMouse()
    {
        Vector3 mouseScreenPos = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            mainCamera.nearClipPlane + 1.0f
        );
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        RectTransform rt = GetComponent<RectTransform>();
        float objectWidth = rt.rect.width * rt.lossyScale.x;

        Vector3 offset = transform.right * (objectWidth * 0.5f);

        Vector3 newPos = mouseWorldPos - offset;
        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

    }

    void AlignLeftWithMouse()
    {
        Vector3 mouseScreenPos = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            mainCamera.nearClipPlane + 1.0f
        );
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        RectTransform rt = GetComponent<RectTransform>();
        float objectWidth = rt.rect.width * rt.lossyScale.x;
        Vector3 offset = transform.right * (objectWidth * 0.5f);

        Vector3 newPos = mouseWorldPos + offset;
        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
    }
}

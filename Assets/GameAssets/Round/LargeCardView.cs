using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LargeCardView : MonoBehaviour
{

    public Camera mainCamera;
    public Card card;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void SetCard(Card card, bool move = true)
    {
        this.card = card;
        transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = card.Name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetCardSprite(card.Name);
        transform.Find("CostBackgroundImage/CostText").GetComponent<TextMeshProUGUI>().text = card.Cost.ToString();
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
                                            mainCamera.nearClipPlane + 1.0f);
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        if (move)
        {
            if (mouseWorldPos.x < -900)
            {
                AlignLeftWithMouse();
            } else {
                AlignRightWithMouse();
            }
        }

        if (card is MonsterCard)
        {
            transform.Find("CardDescription").gameObject.SetActive(false);
            transform.Find("StatsPanel").gameObject.SetActive(true);
            transform.Find("SkillsPanel").gameObject.SetActive(true);
            transform.Find("StatsPanel/AttackText").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).Attack.ToString();
            transform.Find("StatsPanel/HealthText").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).Health.ToString();
            transform.Find("StatsPanel/DefenseText").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).Defense.ToString();
            transform.Find("StatsPanel/SpeedText").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).Movement.ToString();
            transform.Find("SkillsPanel/Skill1NameText").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).skill1.name;
            transform.Find("SkillsPanel/Skill1Text").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).skill1.Description;
            transform.Find("SkillsPanel/Skill1ManaCost/Text").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).skill1.ManaCost.ToString();
            transform.Find("SkillsPanel/Skill1RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + ((MonsterCard)card).skill1.Range.ToString();
            transform.Find("SkillsPanel/Skill1DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + ((MonsterCard)card).skill1.Damage.ToString();
            transform.Find("SkillsPanel/Skill1Text").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).skill2.Description;
            transform.Find("SkillsPanel/Skill2NameText").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).skill2.name;
            transform.Find("SkillsPanel/Skill2Text").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).skill2.Description;
            transform.Find("SkillsPanel/Skill2ManaCost/Text").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).skill2.ManaCost.ToString();
            transform.Find("SkillsPanel/Skill2RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + ((MonsterCard)card).skill2.Range.ToString();
            transform.Find("SkillsPanel/Skill2DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + ((MonsterCard)card).skill2.Damage.ToString();
            transform.Find("SkillsPanel/Skill2Text").GetComponent<TextMeshProUGUI>().text = ((MonsterCard)card).skill2.Description;
        }
        else
        {
            transform.Find("CardDescription").gameObject.SetActive(true);
            transform.Find("CardDescription").GetComponent<TextMeshProUGUI>().text = card.Description;
            transform.Find("SkillsPanel").gameObject.SetActive(false);
            transform.Find("StatsPanel").gameObject.SetActive(false);
        }
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

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

    public void SetCard(Card card, Vector2 pointerPosition, bool move = true)
    {
        this.card = card;
        transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = card.Name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite(card.Name);
        transform.Find("CostBackgroundImage/CostText").GetComponent<TextMeshProUGUI>().text = card.Cost.ToString();

        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogWarning("No RectTransform found on this GameObject. " +
                            "For 2D screen positioning without Vector3, you need a RectTransform.");
            return;
        }

        if (move && (!(card is MonsterCard) || (string.IsNullOrEmpty(((MonsterCard)card).evolvesFrom) && string.IsNullOrEmpty(((MonsterCard)card).evolvesTo))))
        {
            if (pointerPosition.x < 150f)
            {
                AlignLeft(pointerPosition);
            }
            else
            {
                AlignRight(pointerPosition);
            }
        } else {
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = pointerPosition;
        } 

        if (card is MonsterCard monsterCard)
        {
            transform.Find("CardDescription").gameObject.SetActive(false);
            transform.Find("StatsPanel").gameObject.SetActive(true);
            transform.Find("SkillsPanel").gameObject.SetActive(true);
            transform.Find("ManaImage").gameObject.SetActive(true);

            transform.Find("ManaImage/Text").GetComponent<TextMeshProUGUI>().text = monsterCard.ManaCost.ToString();
            transform.Find("StatsPanel/AttackText").GetComponent<TextMeshProUGUI>().text = monsterCard.Attack.ToString();
            transform.Find("StatsPanel/HealthText").GetComponent<TextMeshProUGUI>().text = monsterCard.Health.ToString();
            transform.Find("StatsPanel/DefenseText").GetComponent<TextMeshProUGUI>().text = monsterCard.Defense.ToString();
            transform.Find("StatsPanel/SpeedText").GetComponent<TextMeshProUGUI>().text = monsterCard.Movement.ToString();

            // Skill 1
            transform.Find("SkillsPanel/Skill1NameText").GetComponent<TextMeshProUGUI>().text = monsterCard.skill1.name;
            transform.Find("SkillsPanel/Skill1Text").GetComponent<TextMeshProUGUI>().text = monsterCard.skill1.Description;
            transform.Find("SkillsPanel/Skill1ManaCost/Text").GetComponent<TextMeshProUGUI>().text = monsterCard.skill1.ManaCost.ToString();
            transform.Find("SkillsPanel/Skill1RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + monsterCard.skill1.Range;
            transform.Find("SkillsPanel/Skill1DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + monsterCard.skill1.Damage;

            // Skill 2
            transform.Find("SkillsPanel/Skill2NameText").GetComponent<TextMeshProUGUI>().text = monsterCard.skill2.name;
            transform.Find("SkillsPanel/Skill2Text").GetComponent<TextMeshProUGUI>().text = monsterCard.skill2.Description;
            transform.Find("SkillsPanel/Skill2ManaCost/Text").GetComponent<TextMeshProUGUI>().text = monsterCard.skill2.ManaCost.ToString();
            transform.Find("SkillsPanel/Skill2RangeText").GetComponent<TextMeshProUGUI>().text = "Range: " + monsterCard.skill2.Range;
            transform.Find("SkillsPanel/Skill2DamageText").GetComponent<TextMeshProUGUI>().text = "Damage: " + monsterCard.skill2.Damage;
        }
        else
        {
            // Non-monster card
            transform.Find("CardDescription").gameObject.SetActive(true);
            transform.Find("CardDescription").GetComponent<TextMeshProUGUI>().text = card.Description;
            transform.Find("StatsPanel").gameObject.SetActive(false);
            transform.Find("SkillsPanel").gameObject.SetActive(false);
            transform.Find("ManaImage").gameObject.SetActive(false);
        }
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

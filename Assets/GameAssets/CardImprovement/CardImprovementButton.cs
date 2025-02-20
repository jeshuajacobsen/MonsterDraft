using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardImprovementButton : MonoBehaviour
{

    private Card card;
    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        GameManager.instance.OpenSelectedCardImprovementPanel(card);
    }

    public void InitValues(Card card)
    {
        this.card = card;
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = card.Name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite(card.Name);
        transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + GameManager.instance.cardLevels[card.Name] + "/" + card.maxLevel.ToString();
    }
}

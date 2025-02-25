using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class CardImprovementButton : MonoBehaviour
{

    public Card card;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void InitValues(Card card)
    {
        this.card = card;
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = card.Name;
        transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetSprite(card.Name);
        transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + card.level + "/" + card.maxLevel.ToString();
    }
}

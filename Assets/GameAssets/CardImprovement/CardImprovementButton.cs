using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class CardImprovementButton : MonoBehaviour
{

    public Card card;

    public class Factory : PlaceholderFactory<CardImprovementButton> { }
    private SpriteManager _spriteManager;

    [Inject]
    public void Construct(SpriteManager spriteManager)
    {
        _spriteManager = spriteManager;
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Initialize(Card card)
    {
        this.card = card;
        transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = card.Name;
        transform.Find("Image").GetComponent<Image>().sprite = _spriteManager.GetSprite(card.Name);
        transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + card.level + "/" + card.maxLevel.ToString();
    }
}

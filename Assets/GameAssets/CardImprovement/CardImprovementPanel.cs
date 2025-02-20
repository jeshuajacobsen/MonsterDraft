using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardImprovementPanel : MonoBehaviour
{
    public CardImprovementButton CardImprovementButtonPrefab;
    void Start()
    {
        transform.parent.parent.Find("CloseButton").GetComponent<UnityEngine.UI.Button>()
                .onClick.AddListener(GameManager.instance.CloseCardImprovementPanel);
    }

    void Update()
    {
        
    }

    public void OnOpen()
    {
        foreach (Transform child in transform.Find("MonsterCardPanel"))
        {
            Destroy(child.gameObject);
        }
        foreach (string cardName in GameManager.instance.gameData.GetAllMonsterNames())
        {
            CardImprovementButton cardImprovementButton = Instantiate(CardImprovementButtonPrefab, transform.Find("MonsterCardPanel"));
            cardImprovementButton.InitValues(new MonsterCard(cardName, GameManager.instance.cardLevels[cardName]));
        }

        foreach (Transform child in transform.Find("TreasureCardPanel"))
        {
            Destroy(child.gameObject);
        }
        foreach (string cardName in GameManager.instance.gameData.GetAllTreasureNames())
        {
            CardImprovementButton cardImprovementButton = Instantiate(CardImprovementButtonPrefab, transform.Find("TreasureCardPanel"));
            cardImprovementButton.InitValues(new TreasureCard(cardName, GameManager.instance.cardLevels[cardName]));
        }

        foreach (Transform child in transform.Find("ActionCardPanel"))
        {
            Destroy(child.gameObject);
        }
        foreach (string cardName in GameManager.instance.gameData.GetAllActionNames())
        {
            CardImprovementButton cardImprovementButton = Instantiate(CardImprovementButtonPrefab, transform.Find("ActionCardPanel"));
            cardImprovementButton.InitValues(new ActionCard(cardName, GameManager.instance.cardLevels[cardName]));
        }  

        ScrollRect scrollRect = transform.GetComponentInParent<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }

        transform.parent.parent.Find("PrestigePanel/Text").GetComponent<TextMeshProUGUI>().text = GameManager.instance.PrestigePoints.ToString();
      
    }
}

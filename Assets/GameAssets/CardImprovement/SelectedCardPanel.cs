using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedCardPanel : MonoBehaviour
{
    private Card selectedCard;
    void Start()
    {
        transform.Find("CloseButton").GetComponent<Button>()
                .onClick.AddListener(GameManager.instance.CloseSelectedCardImprovementPanel);
        transform.Find("LevelUpButton").GetComponent<Button>()
                .onClick.AddListener(LevelUpSelectedCard);
    }

    void Update()
    {
        
    }

    public void OnOpen(Card card)
    {
        selectedCard = card;
        if (GameManager.instance.cardLevels[card.Name] >= card.maxLevel)
        {
            transform.Find("LargeCardViewMax").gameObject.SetActive(true);
            transform.Find("LevelMaxText").gameObject.SetActive(true);
            transform.Find("LargeCardViewMax").GetComponent<LargeCardView>().SetCard(card, new Vector2(), false);
            transform.Find("LargeCardViewBefore").gameObject.SetActive(false);
            transform.Find("LargeCardViewAfter").gameObject.SetActive(false);
            transform.Find("LevelUpButton").gameObject.SetActive(false);
            transform.Find("ArrowImage").gameObject.SetActive(false);
            transform.Find("LevelUpCostPanel").gameObject.SetActive(false);

        } else {
            transform.Find("LargeCardViewBefore").gameObject.SetActive(true);
            transform.Find("LargeCardViewAfter").gameObject.SetActive(true);
            transform.Find("LargeCardViewBefore").GetComponent<LargeCardView>().SetCard(card, new Vector2(), false);
            LargeCardView largeCardViewAfter = transform.Find("LargeCardViewAfter").GetComponent<LargeCardView>();
            if (card is MonsterCard)
            {
                largeCardViewAfter.SetCard(new MonsterCard(card.Name, GameManager.instance.cardLevels[card.Name] + 1), new Vector2(), false);
            } else if (card is TreasureCard)
            {
                largeCardViewAfter.SetCard(new TreasureCard(card.Name, GameManager.instance.cardLevels[card.Name] + 1), new Vector2(), false);
            } else if (card is ActionCard)
            {
                largeCardViewAfter.SetCard(new ActionCard(card.Name, GameManager.instance.cardLevels[card.Name] + 1), new Vector2(), false);
            }
            largeCardViewAfter.MarkImprovements();
            transform.Find("LargeCardViewMax").gameObject.SetActive(false);
            transform.Find("LevelMaxText").gameObject.SetActive(false);
            transform.Find("LevelUpButton").gameObject.SetActive(true);
            transform.Find("ArrowImage").gameObject.SetActive(true);
            transform.Find("LevelUpCostPanel").gameObject.SetActive(true);
            TextMeshProUGUI levelUpCostText = transform.Find("LevelUpCostPanel/Text").GetComponent<TextMeshProUGUI>();
            levelUpCostText.text = card.LevelUpPrestigeCost.ToString();
            
            if (GameManager.instance.PrestigePoints < card.LevelUpPrestigeCost)
            {
                levelUpCostText.color = Color.red;
            }
            else
            {
                levelUpCostText.color = Color.black;
            }
        }
        
    }

    public void LevelUpSelectedCard()
    {
        if (GameManager.instance.PrestigePoints >= selectedCard.LevelUpPrestigeCost)
        {
            GameManager.instance.PrestigePoints -= selectedCard.LevelUpPrestigeCost;
            GameManager.instance.cardLevels[selectedCard.Name]++;
            OnOpen(transform.Find("LargeCardViewAfter").GetComponent<LargeCardView>().card);
            GameManager.instance.selectedInitialDeck.ResetLevels();
            GameManager.instance.OpenCardImprovementPanel();
            GameManager.instance.SaveGame();
        }
    }
}

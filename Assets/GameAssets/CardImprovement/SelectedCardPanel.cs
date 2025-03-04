using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class SelectedCardPanel : MonoBehaviour
{
    private Card selectedCard;

    private GameManager _gameManager;
    private CardFactory _cardFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, CardFactory cardFactory, DiContainer container)
    {
        _gameManager = gameManager;
        _cardFactory = cardFactory;
        _container = container;
    }

    void Start()
    {
        transform.Find("CloseButton").GetComponent<Button>()
                .onClick.AddListener(_gameManager.CloseSelectedCardImprovementPanel);
        transform.Find("LevelUpButton").GetComponent<Button>()
                .onClick.AddListener(LevelUpSelectedCard);
    }

    public void OnOpen(Card card)
    {
        selectedCard = card;

        if (_gameManager.cardLevels[card.Name] >= card.maxLevel)
        {
            transform.Find("LargeCardViewMax").gameObject.SetActive(true);
            transform.Find("LevelMaxText").gameObject.SetActive(true);
            transform.Find("LargeCardViewMax").GetComponent<LargeCardView>().SetCard(card, new Vector2(), false);

            transform.Find("LargeCardViewBefore").gameObject.SetActive(false);
            transform.Find("LargeCardViewAfter").gameObject.SetActive(false);
            transform.Find("LevelUpButton").gameObject.SetActive(false);
            transform.Find("ArrowImage").gameObject.SetActive(false);
            transform.Find("LevelUpCostPanel").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("LargeCardViewBefore").gameObject.SetActive(true);
            transform.Find("LargeCardViewAfter").gameObject.SetActive(true);

            transform.Find("LargeCardViewBefore").GetComponent<LargeCardView>().SetCard(card, new Vector2(), false);

            LargeCardView largeCardViewAfter = transform.Find("LargeCardViewAfter").GetComponent<LargeCardView>();

            largeCardViewAfter.SetCard(_cardFactory.CreateCard(card.Name, _gameManager.cardLevels[card.Name] + 1), new Vector2(), false);

            largeCardViewAfter.MarkImprovements();

            transform.Find("LargeCardViewMax").gameObject.SetActive(false);
            transform.Find("LevelMaxText").gameObject.SetActive(false);
            transform.Find("LevelUpButton").gameObject.SetActive(true);
            transform.Find("ArrowImage").gameObject.SetActive(true);
            transform.Find("LevelUpCostPanel").gameObject.SetActive(true);

            TextMeshProUGUI levelUpCostText = transform.Find("LevelUpCostPanel/Text").GetComponent<TextMeshProUGUI>();
            levelUpCostText.text = card.LevelUpPrestigeCost.ToString();

            levelUpCostText.color = (_gameManager.PrestigePoints < card.LevelUpPrestigeCost) ? Color.red : Color.black;
        }
    }

    public void LevelUpSelectedCard()
    {
        if (_gameManager.PrestigePoints >= selectedCard.LevelUpPrestigeCost)
        {
            _gameManager.PrestigePoints -= selectedCard.LevelUpPrestigeCost;
            _gameManager.cardLevels[selectedCard.Name]++;

            OnOpen(transform.Find("LargeCardViewAfter").GetComponent<LargeCardView>().card);

            _gameManager.selectedInitialDeck.ResetLevels();
            _gameManager.OpenCardImprovementPanel();
            _gameManager.SaveGame();
        }
    }
}

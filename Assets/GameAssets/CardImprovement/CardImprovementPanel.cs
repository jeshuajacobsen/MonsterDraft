using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class CardImprovementPanel : MonoBehaviour
{
    private CardImprovementButton.Factory _cardImprovementButtonFactory;

    private GameManager _gameManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, CardImprovementButton.Factory cardImprovementButtonFactory, DiContainer container)
    {
        _gameManager = gameManager;
        _container = container;
        _cardImprovementButtonFactory = cardImprovementButtonFactory;
    }

    void Start()
    {
        transform.parent.parent.Find("CloseButton").GetComponent<UnityEngine.UI.Button>()
                .onClick.AddListener(_gameManager.CloseCardImprovementPanel);
    }

    public void OnOpen()
    {
        ClearPanel("MonsterCardPanel");
        ClearPanel("TreasureCardPanel");
        ClearPanel("ActionCardPanel");

        foreach (string cardName in _gameManager.gameData.GetAllMonsterNames())
        {
            AddCardToPanel("MonsterCardPanel", cardName, "Monster");
        }

        foreach (string cardName in _gameManager.gameData.GetAllTreasureNames())
        {
            AddCardToPanel("TreasureCardPanel", cardName, "Treasure");
        }

        foreach (string cardName in _gameManager.gameData.GetAllActionNames())
        {
            AddCardToPanel("ActionCardPanel", cardName, "Action");
        }

        ScrollRect scrollRect = transform.GetComponentInParent<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }

        transform.parent.parent.Find("PrestigePanel/Text").GetComponent<TextMeshProUGUI>().text = _gameManager.PrestigePoints.ToString();
    }

    private void ClearPanel(string panelName)
    {
        foreach (Transform child in transform.Find(panelName))
        {
            Destroy(child.gameObject);
        }
    }

    private void AddCardToPanel(string panelName, string cardName, string cardType)
    {
        CardImprovementButton cardImprovementButton = _cardImprovementButtonFactory.Create();
        cardImprovementButton.transform.SetParent(transform.Find(panelName), false);

        if (cardType == "Monster")
        {
            var monsterCard = _container.Instantiate<MonsterCard>();
            monsterCard.Initialize(cardName, _gameManager.cardLevels[cardName]);
            cardImprovementButton.Initialize(monsterCard);
        }
        else if (cardType == "Treasure")
        {
            var treasureCard = _container.Instantiate<TreasureCard>();
            treasureCard.Initialize(cardName, _gameManager.cardLevels[cardName]);
            cardImprovementButton.Initialize(treasureCard);
        }
        else if (cardType == "Action")
        {
            var actionCard = _container.Instantiate<ActionCard>();
            actionCard.Initialize(cardName, _gameManager.cardLevels[cardName]);
            cardImprovementButton.Initialize(actionCard);
        }
        cardImprovementButton.transform.GetComponent<Button>().onClick.AddListener(() => _gameManager.OpenSelectedCardImprovementPanel(cardImprovementButton.card));
    }
}

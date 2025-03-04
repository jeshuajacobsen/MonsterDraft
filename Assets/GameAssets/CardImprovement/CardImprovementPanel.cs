using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class CardImprovementPanel : MonoBehaviour
{
    private CardImprovementButton.Factory _cardImprovementButtonFactory;

    private GameManager _gameManager;
    private CardFactory _cardFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, 
                          CardImprovementButton.Factory cardImprovementButtonFactory, 
                          CardFactory cardFactory,
                          DiContainer container)
    {
        _gameManager = gameManager;
        _cardFactory = cardFactory;
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
            AddCardToPanel("MonsterCardPanel", cardName);
        }

        foreach (string cardName in _gameManager.gameData.GetAllTreasureNames())
        {
            AddCardToPanel("TreasureCardPanel", cardName);
        }

        foreach (string cardName in _gameManager.gameData.GetAllActionNames())
        {
            AddCardToPanel("ActionCardPanel", cardName);
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

    private void AddCardToPanel(string panelName, string cardName)
    {
        CardImprovementButton cardImprovementButton = _cardImprovementButtonFactory.Create();
        cardImprovementButton.transform.SetParent(transform.Find(panelName), false);

        cardImprovementButton.Initialize(_cardFactory.CreateCard(cardName, _gameManager.cardLevels[cardName]));

        cardImprovementButton.transform.GetComponent<Button>().onClick.AddListener(() => 
            _gameManager.OpenSelectedCardImprovementPanel(cardImprovementButton.card)
        );
    }
}

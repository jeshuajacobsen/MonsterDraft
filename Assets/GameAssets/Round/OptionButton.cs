using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Zenject;

public class OptionButton : MonoBehaviour
{
    string effect;
    List<Card> cards;

    private RoundManager _roundManager;
    private PlayerStats _playerStats;

    [Inject]
    public void Construct(RoundManager roundManager, PlayerStats playerStats)
    {
        _roundManager = roundManager;
        _playerStats = playerStats;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Initialize(string effect, List<Card> cards)
    {
        this.effect = effect;
        this.cards = cards;
        string[] effectParts = effect.Split(' ');
        if (effectParts[0] == "Coins")
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "+" + effectParts[1] + " Coins";
        } else if (effectParts[0] == "Draw")
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "+" + effectParts[1] + " Cards";
        } else if (effectParts[0] == "Mana")
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "+" + effectParts[1] + " Mana";
        } else if (effectParts[0] == "Trash")
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Trash";
        } else if (effectParts[0] == "Discard")
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Discard";
        } else if (effectParts[0] == "DrawRevealed")
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Draw";
        } else if (effectParts[0] == "Play")
        {
            transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Play";
        }
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        string[] effectParts = effect.Split(' ');
        if (effectParts[0] == "Coins")
        {
            _playerStats.Coins += int.Parse(effectParts[1]);
        } else if (effectParts[0] == "Draw")
        {
            for (int i = 0; i < int.Parse(effectParts[1]); i++)
            {
                _roundManager.AddCardToHand(_roundManager.roundDeck.DrawCard());
            }
        } else if (effectParts[0] == "Mana")
        {
            _playerStats.Mana += int.Parse(effectParts[1]);
        } else if (effectParts[0] == "Trash")
        {
            _roundManager.TrashCardsFromDeck(this.cards);
        } else if (effectParts[0] == "Discard")
        {
            _roundManager.DiscardCardsFromDeck(this.cards);
        } else if (effectParts[0] == "DrawRevealed")
        {
            foreach (Card card in cards)
            {
                _roundManager.AddCardToHand(card);
            }
        } else if (effectParts[0] == "Play")
        {
            foreach (Card card in cards)
            {
                ((MainPhase)_roundManager.gameState).cardsToAutoPlay.Add(card);
            }
        }
        
    }
}

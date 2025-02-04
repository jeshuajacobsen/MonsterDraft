using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OptionButton : MonoBehaviour
{
    string effect;
    List<Card> cards;
    GameState mainPhase;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitValues(string effect, List<Card> cards, GameState mainPhase)
    {
        this.effect = effect;
        this.cards = cards;
        this.mainPhase = mainPhase;
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
            RoundManager.instance.Coins += int.Parse(effectParts[1]);
        } else if (effectParts[0] == "Draw")
        {
            for (int i = 0; i < int.Parse(effectParts[1]); i++)
            {
                RoundManager.instance.AddCardToHand(RoundManager.instance.roundDeck.DrawCard());
            }
        } else if (effectParts[0] == "Mana")
        {
            RoundManager.instance.Mana += int.Parse(effectParts[1]);
        } else if (effectParts[0] == "Trash")
        {
            RoundManager.instance.TrashCardsFromDeck(this.cards);
        } else if (effectParts[0] == "Discard")
        {
            RoundManager.instance.DiscardCardsFromDeck(this.cards);
        } else if (effectParts[0] == "DrawRevealed")
        {
            foreach (Card card in cards)
            {
                RoundManager.instance.AddCardToHand(card);
            }
        } else if (effectParts[0] == "Play")
        {
            foreach (Card card in cards)
            {
                ((MainPhase)mainPhase).cardsToAutoPlay.Add(card);
            }
        }
        
    }
}

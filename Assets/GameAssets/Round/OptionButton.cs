using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionButton : MonoBehaviour
{
    string effect;

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void Update()
    {
        
    }

    public void InitValues(string effect)
    {
        this.effect = effect;
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
        }
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
        }
        
    }
}

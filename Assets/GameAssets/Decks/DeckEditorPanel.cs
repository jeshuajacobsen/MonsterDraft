using UnityEngine;
using System.Collections.Generic;

public class DeckEditorPanel : MonoBehaviour
{
    public List<Card> unlockedCards;
    public DeckEditorCardView cardViewPrefab;
    void Start()
    {
        foreach (var cardName in GameManager.instance.gameData.availableDeckEditorCards)
        {
            DeckEditorCardView cardView = Instantiate(cardViewPrefab, transform);
            string type = GameManager.instance.gameData.GetCardType(cardName.Key);
            if(type == "Treasure")
            {
                cardView.InitValues(new TreasureCard(cardName.Key), cardName.Value);
            }
            else if(type == "Monster")
            {
                cardView.InitValues(new MonsterCard(cardName.Key), cardName.Value);
            }
            else if(type == "Action")
            {
                cardView.InitValues(new ActionCard(cardName.Key), cardName.Value);
            }
        }
    }

    void Update()
    {
        
    }
}

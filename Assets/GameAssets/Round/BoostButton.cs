using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class BoostButton : MonoBehaviour
{
    [SerializeField] private string boostName;
    [SerializeField] private int gemCost;
    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void Update()
    {
        
    }

    private void OnClick()
    {
        if (GameManager.instance.Gems < gemCost)
        {
            return;
        }

        StartCoroutine(HandleBoostEffect());
    }

    private IEnumerator HandleBoostEffect()
    {
        if (boostName == "FreeMonster")
        {
            List<Tile> validTargets = new List<Tile>();
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 2; tile++)
                {
                    Transform tileTransform = RoundManager.instance.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                    if (tileTransform != null)
                    {
                        Tile tileComponent = tileTransform.GetComponent<Tile>();
                        if (tileComponent.monster == null)
                        {
                            validTargets.Add(tileComponent);
                        }
                    }
                }
            }
            if (validTargets.Count == 0)
            {
                // TODO: Add message about no open tiles.
                yield break;
            }
            else
            {
                MonsterCard card = new MonsterCard(GameManager.instance.gameData.GetRandomMonsterName(new List<string>(), "Common"));
                yield return new WaitForEndOfFrame();
                RoundManager.instance.gameState.SwitchPhaseState(new AutoPlayingMonsterState((MainPhase)RoundManager.instance.gameState, card, gemCost));
            }
        }
        else if (boostName == "Coins")
        {
            RoundManager.instance.Coins += 5;
        }
        else if (boostName == "Draw")
        {
            Card drawnCard = RoundManager.instance.roundDeck.DrawCard();
            if (drawnCard == null)
            {
                // TODO: Add message about not enough cards in deck.
                yield break;
            }
            RoundManager.instance.AddCardToHand(drawnCard);
        }
        else if (boostName == "Mana")
        {
            RoundManager.instance.Mana += 5;
        }

        GameManager.instance.Gems -= gemCost;
    }
}

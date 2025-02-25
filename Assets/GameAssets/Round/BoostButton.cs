using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Zenject;

public class BoostButton : MonoBehaviour
{
    [SerializeField] private string boostName;
    [SerializeField] private int gemCost;

    private GameManager _gameManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameManager gameManager, DiContainer container)
    {
        _gameManager = gameManager;
        _container = container;
    }

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void Update()
    {
        
    }

    private void OnClick()
    {
        if (_gameManager.Gems < gemCost)
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
                string monsterName = _gameManager.gameData.GetRandomMonsterName(new List<string>(), "Common");
                var monsterCard = _container.Instantiate<MonsterCard>();
                monsterCard.Initialize(monsterName, _gameManager.cardLevels[monsterName]);
                yield return new WaitForEndOfFrame();
                RoundManager.instance.gameState.SwitchPhaseState(new AutoPlayingMonsterState((MainPhase)RoundManager.instance.gameState, monsterCard, gemCost));
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

        _gameManager.Gems -= gemCost;
    }
}

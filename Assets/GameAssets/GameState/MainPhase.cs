using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainPhase : GameState
{
    private Camera mainCamera;
    public static UnityEvent ExitMainPhase = new UnityEvent();

    public MainPhase(RoundManager roundManager) : base(roundManager) { }

    public override void EnterState()
    {
        Debug.Log("Entering Main Phase");
        roundManager.Actions = 1;
        mainCamera = Camera.main;
    }

    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isInsideOptionPanel = RectTransformUtility.RectangleContainsScreenPoint(
                roundManager.monsterOptionPanel.GetComponent<RectTransform>(),
                Input.mousePosition,
                mainCamera
            );
            if (roundManager.monsterOptionPanel.gameObject.activeSelf && isInsideOptionPanel)
            {
                return;
            } else {
                roundManager.monsterOptionPanel.gameObject.SetActive(false);
                roundManager.hand.ForEach(card => card.HandleMouseDown());
                for(int i = 1; i <= 7; i++)
                {
                    roundManager.dungeonRow1.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
                    roundManager.dungeonRow2.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
                    roundManager.dungeonRow3.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseDown();
                }
            }

            
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < roundManager.hand.Count; i++)
            {
                if (roundManager.hand[i] == null) continue;
                if (roundManager.hand[i].isDragging)
                {
                    roundManager.hand[i].HandleMouseUp();
                    break;
                }
            }
            // for(int i = 1; i <= 7; i++)
            // {
            //     dungeonRow1.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseUp();
            //     dungeonRow2.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseUp();
            //     dungeonRow3.transform.Find("Tile" + i).GetComponent<Tile>().HandleMouseUp();
            // }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Main Phase");
        roundManager.Actions = 0;
        roundManager.DiscardHand();
        roundManager.Mana = 0;
        roundManager.Coins = 0;
        ExitMainPhase.Invoke();
    }

    public override bool CanPlayCard(Card card, Vector3 position)
    {
        if (card is MonsterCard)
        {
            MonsterCard monsterCard = (MonsterCard)card;
            if (monsterCard.ManaCost > roundManager.Mana)
            {
                return false;
            }
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 7; tile++)
                {
                    Transform tileTransform = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                    if (tileTransform != null && tileTransform.GetComponent<Collider2D>().bounds.Contains(position))
                    {
                        return true;
                    }
                }
            }
        }
        if (card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)card;
            if (roundManager.Actions > 0)
            {
                return true;
            }
        }
        if (card is TreasureCard)
        {
            return true;
        }
        return false;
    }

    public override void PlayCard(Card card, Vector3 position)
    {
        if (card is TreasureCard)
        {
            TreasureCard treasureCard = (TreasureCard)card;
            roundManager.Coins += treasureCard.GoldGeneration;
            roundManager.Mana += treasureCard.ManaGeneration;
            roundManager.discardPile.AddCard(card);
        }
        else if (card is MonsterCard && roundManager.DungeonPanel.activeSelf)
        {
            MonsterCard monsterCard = (MonsterCard)card;
            for (int row = 1; row <= 3; row++)
            {
                for (int tile = 1; tile <= 7; tile++)
                {
                    Transform tileTransform = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{tile}");
                    if (tileTransform != null && tileTransform.GetComponent<Collider2D>().bounds.Contains(position))
                    {
                        Monster newMonster = Instantiate(RoundManager.instance.MonsterPrefab, tileTransform);
                        newMonster.InitValues(monsterCard, tileTransform.GetComponent<Tile>());
                        tileTransform.GetComponent<Tile>().monster = newMonster;
                        RoundManager.instance.discardPile.AddCard(card);
                        roundManager.hand.Remove(roundManager.hand.Find(x => x.card == card));
                        return;
                    }
                }
            }
        }
        else if (card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)card;
            roundManager.Actions--;
            roundManager.discardPile.AddCard(card);
        }
        roundManager.hand.Remove(roundManager.hand.Find(x => x.card == card));
    }

    public override void SelectTile(Tile tile, Vector3 position)
    {
        if (tile.monster != null)
        {
            roundManager.monsterOptionPanel.gameObject.SetActive(true);
            roundManager.monsterOptionPanel.transform.position = position;
            Vector3 panelSize = roundManager.monsterOptionPanel.GetComponent<RectTransform>().sizeDelta;
            roundManager.monsterOptionPanel.transform.position += new Vector3(panelSize.x / 2, -panelSize.y / 2, 0);
            roundManager.monsterOptionPanel.transform.GetComponent<MonsterOptionsPanel>().SetActiveTile(tile);
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

public class EnemyPhase : GameState
{
    public EnemyPhase(RoundManager roundManager) : base(roundManager) { }

    static int StartPadding = 2;

    public override void EnterState()
    {
        Debug.Log("Entering Enemy Phase");
        

        for (int row = 1; row <= 3; row++)
        {
            for (int i = 1; i <= 7; i++)
            {
                Tile tile = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{i}").GetComponent<Tile>();
                if (tile.monster != null && tile.monster.team == "Enemy")
                {
                    Monster currentMonster = tile.monster;
                    if (roundManager.EnemyCanMove(tile.monster))
                    {
                        roundManager.MoveEnemyMonster(tile.monster);
                    }
                    bool usedSkill = false;
                    if (roundManager.EnemyCanUseSkill(currentMonster, currentMonster.skill1))
                    {
                        Debug.Log("Enemy using skill 1");
                        usedSkill = true;
                        roundManager.EnemyUseSkill(currentMonster, currentMonster.skill1);
                    }
                    if (!usedSkill && roundManager.EnemyCanUseSkill(currentMonster, currentMonster.skill2))
                    {
                        Debug.Log("Enemy using skill 2");
                        roundManager.EnemyUseSkill(currentMonster, currentMonster.skill2);
                    }
                }
            }
        }

        if (StartPadding > 0)
        {
            StartPadding--;
            return;
        }
        Card card = roundManager.currentDungeon.DrawCard();
        if (card != null && card is MonsterCard)
        {
        
            List<Tile> openTiles = new List<Tile>();
            for (int row = 1; row <= 3; row++)
            {
                for (int i = 6; i <= 7; i++)
                {
                    Tile tile = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{i}").GetComponent<Tile>();
                    if (tile.monster == null)
                    {
                        openTiles.Add(tile);
                    }
                }
            }
            if (openTiles.Count > 0)
            {
                PlayMonsterCardOnTile((MonsterCard)card, openTiles[Random.Range(0, openTiles.Count)]);
            }
        } else if (card != null && card is ActionCard)
        {
            ActionCard actionCard = (ActionCard)card;
            string[] effects = actionCard.effects[0].Split(' ');
            List<Monster> targets = new List<Monster>();

            if (effects[0] == "Target")
            {
                if (effects[1] == "Enemy")
                {
                    for (int row = 1; row <= 3; row++)
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            Tile tile = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{i}").GetComponent<Tile>();
                            if (tile.monster != null && tile.monster.team == "Ally")
                            {
                                targets.Add(tile.monster);
                            }
                        }
                    }
                }
                else if (effects[1] == "Ally")
                {
                    for (int row = 1; row <= 3; row++)
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            Tile tile = roundManager.DungeonPanel.transform.Find($"CombatRow{row}/Tile{i}").GetComponent<Tile>();
                            if (tile.monster != null && tile.monster.team == "Enemy")
                            {
                                targets.Add(tile.monster);
                            }
                        }
                    }
                }
                
            }
            if (targets.Count == 0)
            {
                return;
            }

            int damageDealt = 0;
            Monster target = targets[Random.Range(0, targets.Count)];
            VisualEffect visualEffect = null;
            for (int i = 1; i < actionCard.effects.Count; i++)
            {
                string[] effectParts = actionCard.effects[i].Split(' ');
                if (effectParts[0] == "Damage")
                {
                    if (visualEffect != null)
                    {
                        visualEffect.reachedTarget.AddListener(() => {
                            target.Health -= int.Parse(effectParts[1]);
                        });
                    } else {
                        target.Health -= int.Parse(effectParts[1]);
                    }
                } else if (effectParts[0] == "Heal")
                {
                    int heal = int.Parse(effectParts[1]);
                    target.Health += heal;
                } else if (effectParts[0] == "Buff")
                {
                    int buffValue = 0;
                    string buffType = effectParts[1];
                    string buffDescription = "";
                    int duration = 0;
                    if (effectParts[2] == "Plus")
                    {
                        buffValue = int.Parse(effectParts[3]);
                    } else if (effectParts[2] == "Minus")
                    {
                        buffValue = -int.Parse(effectParts[3]);
                    }
                    if (effectParts[4] == "Duration")
                    {
                        duration = int.Parse(effectParts[5]);
                    }
                    buffDescription = buffValue > 0 ? "+" : "-" + buffValue + " " + buffType;
                    
                    target.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                } else if (effectParts[0] == "Animate")
                {
                    if (effectParts[1] == "Fireball")
                    {
                        visualEffect = RoundManager.instance.AddVisualEffect("Fireball", target.tileOn);
                    }
                }
            }
        }
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Enemy Phase");
    }

    public override bool CanPlayCard(Card card, Vector3 position)
    {
        return false;
    }

    public override void PlayCard(Card card, Vector3 position)
    {
        Debug.LogError("Cards cannot be played during the Draw Phase!");
    }

    private void PlayMonsterCardOnTile(MonsterCard card, Tile tile)
    {
        Monster newMonster = Instantiate(RoundManager.instance.MonsterPrefab, tile.transform);
        newMonster.InitValues(card, tile, "Enemy");
        tile.monster = newMonster;
    }

    public override void SelectTile(Tile tile, Vector2 position)
    {
        Debug.LogError("Tiles cannot be selected during the Draw Phase!");
    }
}
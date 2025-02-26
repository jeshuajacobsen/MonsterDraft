using System;
using UnityEngine;
using System.Collections.Generic;

public class ResolvingEffectState : CardPlayState
{

    public override void EnterState()
    {
        Debug.Log("Resolving Effect State Entered");
        ResolveEffects();
    }

    public override void HandleInput() { }

    public override void UpdateState()
    {

        
    }

    private void ResolveEffects()
    {
        for (int i = mainPhase.playedActionCardStep; i < mainPhase.playedCard.Effects.Count; i++)
        {
            string[] effectParts = mainPhase.playedCard.Effects[i].Split(' ');
            if (effectParts[0] == "Actions")
            {
                _roundManager.Actions += int.Parse(effectParts[1]);
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0]  == "Coins") 
            {
                if (effectParts[1] == "x")
                {
                    if (effectParts[2] == "Times")
                    {
                        _roundManager.Coins += mainPhase.selectedCards.Count * int.Parse(effectParts[3]);
                    }
                } else {
                    int coins = int.Parse(effectParts[1]);
                    if (effectParts.Length > 2 && effectParts[2] == "Per")
                    {
                        if (effectParts[3] == "Treasure" && effectParts[4] == "Played")
                        {
                            coins *= mainPhase.treasuresPlayed;
                        }
                    }
                    _roundManager.Coins += coins;
                }
                mainPhase.playedActionCardStep++;
                
            } else if (effectParts[0] == "Mana")
            {
                int amount = int.Parse(effectParts[1]);
                if (effectParts.Length > 2 && effectParts[2] == "Per")
                {
                    if (effectParts[3] == "Ally")
                    {
                        amount *= _roundManager.GetAllAllies().Count;
                        _roundManager.Mana += amount;
                    } else if (effectParts[3] == "Coins/2")
                    {
                        amount = _roundManager.Coins / 2;
                        _roundManager.Mana += amount;
                    }
                } else {
                    _roundManager.Mana += amount;
                }
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Experience")
            {
                _roundManager.Experience += int.Parse(effectParts[1]);
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Draw")
            {
                if (effectParts[1] == "x")
                {
                    int x = mainPhase.selectedCards.Count;
                    for (int j = 0; j < x; j++)
                    {
                        _roundManager.AddCardToHand(_roundManager.roundDeck.DrawCard());
                    }
                } else {
                    for (int j = 0; j < int.Parse(effectParts[1]); j++)
                    {
                        _roundManager.AddCardToHand(_roundManager.roundDeck.DrawCard());
                    }
                }
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Select")
            {
                mainPhase.playedActionCardStep++;
                if (effectParts[1] == "Cards")
                {
                    mainPhase.SwitchPhaseState(_container.Instantiate<SelectingCardsState>().Initialize(effectParts[2], "None"));
                } else if (effectParts[1] == "Treasure")
                {
                    mainPhase.SwitchPhaseState(_container.Instantiate<SelectingCardsState>().Initialize(effectParts[2], "Treasure"));
                } else if (effectParts[1] == "Action")
                {
                    mainPhase.SwitchPhaseState(_container.Instantiate<SelectingCardsState>().Initialize(effectParts[2], "Action"));
                }
                return;
            } else if (effectParts[0] == "Discard")
            {
                if (effectParts[1] == "x")
                {
                    int x = mainPhase.selectedCards.Count;
                    for (int j = 0; j < x; j++)
                    {
                        _roundManager.discardPile.AddCard(mainPhase.selectedCards[j].card);
                        _roundManager.RemoveCardFromHand(mainPhase.selectedCards[j]);
                        _roundManager.DestroyCard(mainPhase.selectedCards[j]);
                    }
                } else if (effectParts[1] == "Selected") 
                {
                    for (int j = 0; j < mainPhase.selectedCards.Count; j++)
                    {
                        _roundManager.discardPile.AddCard(mainPhase.selectedCards[j].card);
                        _roundManager.RemoveCardFromHand(mainPhase.selectedCards[j]);
                        _roundManager.DestroyCard(mainPhase.selectedCards[j]);
                    }
                }
                
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Trash")
            {
                if (effectParts[1] == "Selected") 
                {
                    for (int j = 0; j < mainPhase.selectedCards.Count; j++)
                    {
                        _roundManager.RemoveCardFromHand(mainPhase.selectedCards[j]);
                        _roundManager.DestroyCard(mainPhase.selectedCards[j]);
                    }
                }
                
                mainPhase.playedActionCardStep++;

            } else if (effectParts[0] == "Gain")
            {
                string restriction = "None";
                mainPhase.playedActionCardStep++;
                if (effectParts[1] == "Treasure")
                {
                    restriction = "Treasure";
                }
                if (effectParts[2] == "Costing")
                {
                    if (effectParts[3] == "Selected")
                    {
                        if (effectParts[4] == "Cost")
                        {
                            
                            if (effectParts[5] == "Plus")
                            {
                                int cost = 0;
                                if (mainPhase.selectedCards.Count > 0)
                                {
                                    cost += mainPhase.selectedCards[0].card.CoinCost;
                                    cost += int.Parse(effectParts[6]);
                                    bool cancelable = mainPhase.playedActionCardStep == 1;
                                    mainPhase.SwitchPhaseState(_container.Instantiate<GainingCardState>().Initialize(restriction, cost, cancelable));
                                    return;
                                }
                            }
                        }
                    } else if (effectParts[3] == "Saved")
                    {
                        int additionalCost = 0;
                        if (effectParts[4] == "Plus")
                        {
                            additionalCost = int.Parse(effectParts[5]);
                        }
                        bool cancelable = mainPhase.playedActionCardStep == 1;
                        mainPhase.SwitchPhaseState(
                            _container.Instantiate<GainingCardState>().Initialize(restriction, mainPhase.savedValue + additionalCost, cancelable));
                        return;
                    }
                }
            } else if (effectParts[0] == "Play")
            {
                mainPhase.playedActionCardStep++;
                if (effectParts[1] == "Selected")
                {
                    if (mainPhase.selectedCards.Count > 0)
                    {
                        for (int j = 0; j < int.Parse(effectParts[2]); j++)
                        {
                            mainPhase.cardsToAutoPlay.Add(mainPhase.selectedCards[0].card);
                        }
                        mainPhase.RemoveCard(mainPhase.selectedCards[0]);
                    }
                    
                }
            } else if (effectParts[0] == "Target")
            {
                mainPhase.playedActionCardStep++;
                mainPhase.SwitchPhaseState(_container.Instantiate<SelectingTargetMonsterState>().Initialize(mainPhase.playedCard));
                return;
            } else if (effectParts[0] == "Damage")
            {
                int damage = int.Parse(effectParts[1]);
                mainPhase.selectedTile.monster.Health -= damage;
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Heal")
            {
                int heal = int.Parse(effectParts[1]);
                mainPhase.selectedTile.monster.Health += heal;
                mainPhase.playedActionCardStep++;
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
                if (effectParts.Length > 6 && effectParts[6] == "All")
                {
                    if (effectParts[7] == "Ally")
                    {
                        foreach (var ally in _roundManager.GetAllAllies())
                        {
                            ally.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                        }
                    } else if (effectParts[7] == "Enemy")
                    {
                        foreach (var enemy in _roundManager.GetAllEnemies())
                        {
                            enemy.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                        }
                    }
                } else {
                    mainPhase.selectedTile.monster.buffs.Add(new MonsterBuff(buffType, buffValue, buffDescription, duration));
                }
                
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Save")
            {
                if (effectParts[1] == "x")
                {
                    if (effectParts[2] == "Sum" && effectParts[3] == "Costs")
                    {
                        int sum = 0;
                        for (int j = 0; j < mainPhase.selectedCards.Count; j++)
                        {
                            sum += mainPhase.selectedCards[j].card.CoinCost;
                        }
                        mainPhase.savedValue = sum;
                    }
                }
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Search")
            {
                if (effectParts[1] == "Discard")
                {
                    if (effectParts[2] == "Copper")
                    {
                        for (int j = _roundManager.discardPile.cards.Count - 1; j >= 0; j--)
                        {
                            var card = _roundManager.discardPile.cards[j];
                            if (card.Name == "Copper")
                            {
                                _roundManager.discardPile.RemoveCard(card);
                                mainPhase.foundCards.Add(card);
                            }
                        }
                    }
                } else if (effectParts[1] == "Deck")
                {
                    if (effectParts[2] == "Next")
                    {
                        mainPhase.foundCards.Clear();
                        Card nextCard = FindNextCardInDeck(effectParts[3]);
                        if (nextCard == null)
                        {
                            _roundManager.roundDeck.ShuffleDiscardIntoDeck();
                            nextCard = FindNextCardInDeck(effectParts[3]);
                        }
                        mainPhase.foundCards.Add(nextCard);
                    }
                }
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Found")
            {
                if (effectParts[1] == "Into")
                {
                    if (effectParts[2] == "Hand")
                    {
                        foreach (var card in mainPhase.foundCards)
                        {
                            _roundManager.AddCardToHand(card);
                        }
                    }
                }
                else if (effectParts[1] == "Option")
                {
                    if (effectParts[2] == "If")
                    {
                        if (effectParts[3] == "Cost")
                        {
                            int limit = int.Parse(effectParts[4]);
                            if (effectParts[5] == "Mana")
                            {
                                if (effectParts[6] == "Less")
                                {
                                    if (effectParts[7] == "Play")
                                    {
                                        List<string> options = new List<string>();
                                        if (mainPhase.foundCards.Count == 0)
                                        {
                                            options.Add("Done");
                                        }
                                        else if (((MonsterCard)mainPhase.foundCards[0]).ManaCost <= limit)
                                        {
                                            options.Add("Play");
                                            options.Add("DrawRevealed");
                                            mainPhase.SwitchPhaseState(_container.Instantiate<SelectingOptionState>().Initialize(options, mainPhase.foundCards));
                                        } else {
                                            options.Add("DrawRevealed");
                                            mainPhase.SwitchPhaseState(_container.Instantiate<SelectingOptionState>().Initialize(options, mainPhase.foundCards));                                        
                                        }
                                        mainPhase.playedActionCardStep++;
                                        return;
                                    }
                                }
                            }
                        }
                    } else {
                        List<string> options = new List<string>();
                        for (int j = 2; j < effectParts.Length; j++)
                        {
                            options.Add(effectParts[j]);
                        }
                        mainPhase.playedActionCardStep++;
                        mainPhase.SwitchPhaseState(_container.Instantiate<SelectingOptionState>().Initialize(options, mainPhase.foundCards));
                        return;
                    }
                }
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Nothing")
            {
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "PersistentEffect")
            {
                if (effectParts[1] == "SkillsCost")
                {
                    int amount = int.Parse(effectParts[2]);
                    _roundManager.persistentEffects.Add(
                        new PersistentEffect("SkillsCost", amount, "Skills cost " + amount, int.Parse(effectParts[4])));
                } else if (effectParts[1] == "AdditionalSkills")
                {
                    int amount = int.Parse(effectParts[2]);
                    _roundManager.persistentEffects.Add(
                        new PersistentEffect("AdditionalSkills", amount, "Monsters can use " + amount + " extra skills", int.Parse(effectParts[4])));
                }
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Destroy")
            {
                if (effectParts[1] == "Target")
                {
                    mainPhase.selectedTile.monster.Health = 0;
                }
                mainPhase.playedActionCardStep++;
            } else if (effectParts[0] == "Animate")
            {
                if (effectParts[1] == "Fireball")
                {
                    _roundManager.AddVisualEffect("Fireball", mainPhase.selectedTile);
                }
                mainPhase.playedActionCardStep++;
            }
        }
        
        mainPhase.FinishPlay();
    }

    private Card FindNextCardInDeck(string cardType)
    {
        for (int i = 0; i < _roundManager.roundDeck.cards.Count; i++)
        {
            if (_roundManager.roundDeck.cards[i].Type == cardType)
            {
                return _roundManager.roundDeck.cards[i];
            }
        }
        return null;
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Resolving Effect State");
    }
}

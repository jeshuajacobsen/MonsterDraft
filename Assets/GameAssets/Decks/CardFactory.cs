using System;
using Zenject;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory
{
    private readonly GameManager _gameManager;
    
    [Inject]
    public CardFactory(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public Card CreateCard(string name, int level)
    {
        Card card;
        string type = _gameManager.gameData.GetCardType(name);
        
        switch (type)
        {
            case "Monster":
                card = new MonsterCard();
                InitializeMonsterCard((MonsterCard)card, name, level);
                break;
            case "Treasure":
                card = new TreasureCard();
                InitializeTreasureCard((TreasureCard)card, name, level);
                break;
            case "Action":
                card = new ActionCard();
                InitializeActionCard((ActionCard)card, name, level);
                break;
            default:
                throw new ArgumentException($"Unknown card type: {type}");
        }
        
        card.Construct(_gameManager);
        return card;
    }
    
    private void InitializeCardBase(Card card, string name, int level)
    {
        card.Name = name;
        card.level = level;
        card.OnGainEffects = new List<string>();
        card.Effects = new List<string>();
        card.EffectVariables = new Dictionary<string, string>();
    }

    private void InitializeMonsterCard(MonsterCard card, string name, int level)
    {
        InitializeCardBase(card, name, level);
        var baseStats = _gameManager.gameData.GetBaseMonsterData(name);
        card.CoinCost = baseStats.CoinCost;
        card.LevelUpPrestigeCost = baseStats.LevelUpPrestigeCost;
        card.maxLevel = baseStats.maxLevel;
        card.BuyCardPrestigeCost = baseStats.BuyCardPrestigeCost;
        card.Attack = baseStats.Attack;
        card.Health = baseStats.Health;
        card.Defense = baseStats.Defense;
        card.Movement = baseStats.Movement;
        card.ManaCost = baseStats.ManaCost;
        card.skill1 = _gameManager.gameData.GetSkill(baseStats.skill1Name);
        card.skill2 = _gameManager.gameData.GetSkill(baseStats.skill2Name);
        card.evolvesFrom = baseStats.evolvesFrom;
        card.evolvesTo = baseStats.evolvesTo;
        card.experienceGiven = baseStats.experienceGiven;
        card.experienceRequired = baseStats.experienceRequired;
    }

    private void InitializeTreasureCard(TreasureCard card, string name, int level)
    {
        InitializeCardBase(card, name, level);
        var baseStats = _gameManager.gameData.GetTreasureData(name);
        card.CoinCost = baseStats.CoinCost;
        card.LevelUpPrestigeCost = baseStats.LevelUpPrestigeCost;
        card.maxLevel = baseStats.maxLevel;
        card.BuyCardPrestigeCost = baseStats.BuyCardPrestigeCost;
        card.CoinGeneration = baseStats.CoinGeneration;
        card.ManaGeneration = baseStats.ManaGeneration;
        card.OnGainEffects = new List<string>(baseStats.onGainEffects);
        card.Effects = new List<string>(baseStats.effects);
        card.EffectVariables = new Dictionary<string, string>(baseStats.effectVariables);
        card.Description = baseStats.Description;
    }

    private void InitializeActionCard(ActionCard card, string name, int level)
    {
        InitializeCardBase(card, name, level);
        var baseStats = _gameManager.gameData.GetActionData(name);
        card.CoinCost = baseStats.CoinCost;
        card.LevelUpPrestigeCost = baseStats.LevelUpPrestigeCost;
        card.maxLevel = baseStats.maxLevel;
        card.BuyCardPrestigeCost = baseStats.BuyCardPrestigeCost;
        card.Description = baseStats.Description;
        card.OnGainEffects = new List<string>(baseStats.onGainEffects);
        card.Effects = new List<string>(baseStats.effects);
        card.EffectVariables = new Dictionary<string, string>(baseStats.effectVariables);
        card.Description = baseStats.Description;
    }
}

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using Zenject;

[TestFixture]
public class CardFactoryTests
{
    private DiContainer _container;
    private CardFactory _cardFactory;
    private IGameManager _mockGameManager;
    private IGameData _mockGameData;

    [SetUp]
    public void SetUp()
    {
        _container = new DiContainer();

        _mockGameManager = Substitute.For<IGameManager>(); 
        _mockGameData = Substitute.For<IGameData>();

        _mockGameManager.GameData.Returns(_mockGameData);

        _container.Bind<IGameManager>().FromInstance(_mockGameManager);
        _container.Bind<IGameData>().FromInstance(_mockGameData);
        _container.Bind<CardFactory>().AsSingle();

        _cardFactory = _container.Resolve<CardFactory>();
    }


    [Test]
    public void CreateCard_ShouldReturnMonsterCard_WhenTypeIsMonster()
    {
        string cardName = "Zaple";
        int level = 1;

        _mockGameData.GetCardType(cardName).Returns("Monster");

        _mockGameData.GetBaseMonsterData(cardName).Returns(new BaseMonsterData("Zaple"));

        _mockGameData.GetSkill("Zap").Returns(new SkillData().Initialize("Zap"));
        _mockGameData.GetSkill("Shock").Returns(new SkillData().Initialize("Shock"));

        Card result = _cardFactory.CreateCard(cardName, level);

        Assert.IsInstanceOf<MonsterCard>(result);
        Assert.AreEqual(cardName, result.Name);
        Assert.AreEqual(level, result.level);
        Assert.AreEqual(50, ((MonsterCard)result).CoinCost);
        Assert.AreEqual(30, ((MonsterCard)result).Attack);
        Assert.AreEqual("Zap", ((MonsterCard)result).skill1.name);
    }

    [Test]
    public void CreateCard_ShouldReturnTreasureCard_WhenTypeIsTreasure()
    {
        string cardName = "Copper";
        int level = 1;

        _mockGameData.GetCardType(cardName).Returns("Treasure");

        _mockGameData.GetTreasureData(cardName).Returns(new TreasureData(cardName));

        Card result = _cardFactory.CreateCard(cardName, level);

        Assert.IsInstanceOf<TreasureCard>(result);
        Assert.AreEqual(cardName, result.Name);
        Assert.AreEqual(level, result.level);
        Assert.AreEqual(20, ((TreasureCard)result).CoinCost);
        Assert.AreEqual(20, ((TreasureCard)result).CoinGeneration);
        Assert.AreEqual("+20 Coins", ((TreasureCard)result).Description);
    }

    [Test]
    public void CreateCard_ShouldReturnActionCard_WhenTypeIsAction()
    {
        string cardName = "Fireball";
        int level = 1;

        _mockGameData.GetCardType(cardName).Returns("Action");

        _mockGameData.GetActionData(cardName).Returns(new BaseActionData(cardName));

        Card result = _cardFactory.CreateCard(cardName, level);

        Assert.IsInstanceOf<ActionCard>(result);
        Assert.AreEqual(cardName, result.Name);
        Assert.AreEqual(level, result.level);
        Assert.AreEqual(30, ((ActionCard)result).CoinCost);
        Assert.AreEqual("Deals 30 damage to an enemy.", ((ActionCard)result).Description);
        Assert.AreEqual(((ActionCard)result).OnGainEffects.Count, 0);
    }

    [Test]
    public void CreateCard_ShouldThrowException_WhenCardTypeIsUnknown()
    {
        string cardName = "UnknownCard";
        int level = 1;

        _mockGameData.GetCardType(cardName).Returns("Unknown");

        var ex = Assert.Throws<ArgumentException>(() => _cardFactory.CreateCard(cardName, level));
        Assert.AreEqual($"Unknown card type: Unknown", ex.Message);
    }
}

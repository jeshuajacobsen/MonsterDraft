using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    [SerializeField] private SkillVisualEffect skillVisualEffectPrefab;
    [SerializeField] private CardVisualEffect cardVisualEffectPrefab;
    [SerializeField] private FloatyNumber floatyNumberPrefab;
    [SerializeField] private CardImprovementButton cardImprovementButtonPrefab;
    [SerializeField] private Monster monsterPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IGameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<RoundManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RunManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SpriteManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerStats>().AsSingle();
        Container.Bind<RoundUIManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DungeonManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CardManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CombatManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<VisualEffectManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CardFactory>().AsSingle();

        Container.BindFactory<SkillVisualEffect, SkillVisualEffect.Factory>()
            .FromComponentInNewPrefab(skillVisualEffectPrefab);
        Container.BindFactory<CardVisualEffect, CardVisualEffect.Factory>()
            .FromComponentInNewPrefab(cardVisualEffectPrefab);
        Container.BindFactory<FloatyNumber, FloatyNumber.Factory>()
            .FromComponentInNewPrefab(floatyNumberPrefab);
        Container.BindFactory<CardImprovementButton, CardImprovementButton.Factory>()
            .FromComponentInNewPrefab(cardImprovementButtonPrefab);
        Container.BindFactory<Monster, Monster.Factory>()
            .FromComponentInNewPrefab(monsterPrefab);
    }
}

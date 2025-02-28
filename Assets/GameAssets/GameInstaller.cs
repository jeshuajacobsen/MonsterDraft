using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    [SerializeField] private SkillVisualEffect skillVisualEffectPrefab;
    [SerializeField] private CardVisualEffect cardVisualEffectPrefab;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<RoundManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RunManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SpriteManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerStats>().AsSingle();
        Container.Bind<RoundUIManager>().FromComponentInHierarchy().AsSingle();

        Container.BindFactory<SkillVisualEffect, SkillVisualEffect.Factory>()
            .FromComponentInNewPrefab(skillVisualEffectPrefab);
        Container.BindFactory<CardVisualEffect, CardVisualEffect.Factory>()
            .FromComponentInNewPrefab(cardVisualEffectPrefab);
    }
}

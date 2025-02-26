using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();

        Container.Bind<RoundManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RunManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        //Container.Bind<SpriteManager>().AsSingle().NonLazy();
    }
}

using Game.Client;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IClientManager>()
            .FromComponentInHierarchy().AsSingle();
    }
}
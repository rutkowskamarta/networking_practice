using Game.Client;
using Game.PlayerData;
using Game.UI;
using Zenject;

namespace Game.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameClientManager>()
                .FromComponentInHierarchy().AsSingle();

            Container.Bind<IUIViewsManager>()
                .FromComponentInHierarchy().AsSingle();

            Container.Bind<IPlayerDataManager>()
                .FromComponentInHierarchy().AsSingle();
        }
    }
}
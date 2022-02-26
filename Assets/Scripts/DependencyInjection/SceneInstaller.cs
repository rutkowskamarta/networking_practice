using Game.Client;
using Game.Player;
using Game.Room;
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

            Container.Bind<IRoomManager>()
               .FromComponentInHierarchy().AsSingle();
        }
    }
}
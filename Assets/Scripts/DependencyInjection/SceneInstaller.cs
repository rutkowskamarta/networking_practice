using Game.Client;
using Game.UI;
using Zenject;

namespace Game.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IClientManager>()
                .FromComponentInHierarchy().AsSingle();

            Container.Bind<IUIViewsManager>()
                .FromComponentInHierarchy().AsSingle();
        }
    }
}
using App.Services.Runners;
using Zenject;

namespace App.Core.Installers
{
    public class BootStrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameSettingsService>()
                .AsSingle();

            Container.Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();
        }
    }
}

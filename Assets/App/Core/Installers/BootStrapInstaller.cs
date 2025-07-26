using Zenject;

namespace App.Core.Installers
{
    public class BootStrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameSettingsService>()
                .AsSingle();
        }
    }
}

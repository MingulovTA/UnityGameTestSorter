using App.Services.Popups;
using App.Services.Runners;
using UnityEngine;
using Zenject;

namespace App.Core.Installers
{
    public class BootStrapInstaller : MonoInstaller
    {
        private const string POPUPS_CONTAINTER_RESOURCE_PATH = "UI/Popups/PopupsContainer";
        
        [SerializeField] private PopupsContainer _popupsContainer;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameSettingsService>()
                .AsSingle();

            Container.Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();

            RegisterPopups();
        }

        private void RegisterPopups()
        {
            Container.Bind<IPopupsContainer>()
                .To<PopupsContainer>()
                .FromComponentInNewPrefabResource(POPUPS_CONTAINTER_RESOURCE_PATH)
                .AsSingle()
                .NonLazy();

            Container.Bind<IPopupService>()
                .To<PopupService>()
                .AsSingle();
        }
    }
}

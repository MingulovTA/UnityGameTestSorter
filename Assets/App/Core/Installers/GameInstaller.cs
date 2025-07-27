using App.Game;
using App.Game.GameField;
using App.Game.Holes;
using App.Game.Pools;
using App.Game.Signals;
using UnityEngine;
using Zenject;

namespace App.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameFieldView _gameFieldView;
        [SerializeField] private HolesField _holesField;
        [SerializeField] private Camera _camera;

        public override void InstallBindings()
        {
            RegisterGameplayServices();
            RegisterSceneViews();
            RegisterSignalBus();
        }

        private void RegisterGameplayServices()
        {
            Container.BindInterfacesTo<SorterGame>()
                .AsSingle();
            
            Container.BindInterfacesTo<FiguresSpawner>()
                .AsSingle();
            
            Container.BindInterfacesTo<FiguresPool>()
                .AsSingle();
            
            Container.BindInterfacesTo<SorterGameStats>()
                .AsSingle();
            
            Container.Bind<IFiguresFactory>()
                .To<FiguresFactory>()
                .AsSingle();
        }

        private void RegisterSceneViews()
        {
            Container.Bind<Camera>()
                .FromInstance(_camera);
            
            Container.Bind<GameFieldView>()
                .FromInstance(_gameFieldView);
            
            Container.Bind<HolesField>()
                .FromInstance(_holesField);
        }

        private void RegisterSignalBus()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<FigureDroppedToHoleSignal>();
            Container.DeclareSignal<FigureReachedSignal>();
            Container.DeclareSignal<GameStartSignal>();
            Container.DeclareSignal<GameCompleteSignal>();
            Container.DeclareSignal<PlayerStatsUpdateSignal>();
        }
    }
}

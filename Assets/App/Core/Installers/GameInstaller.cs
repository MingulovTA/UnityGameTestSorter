using App.Game;
using App.Game.GameField;
using UnityEngine;
using Zenject;

namespace App.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameFieldView _gameFieldView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SorterGame>()
                .AsSingle();
            
            Container.Bind<GameFieldView>()
                .FromInstance(_gameFieldView);
        }
    }
}

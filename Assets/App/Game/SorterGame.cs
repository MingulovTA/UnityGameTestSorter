using System;
using App.Game.GameField;
using UnityEngine;
using Zenject;

namespace App.Game
{
    public class SorterGame : IInitializable, IDisposable
    {
        private IGameSettingsService _gameSettingsService;
        private GameFieldView _gameFieldView;
    
        [Inject]
        SorterGame(IGameSettingsService gameSettingsService,
            GameFieldView gameFieldView)
        {
            _gameSettingsService = gameSettingsService;
            _gameFieldView = gameFieldView;
        }

        public void Initialize()
        {
            Debug.Log($"_gameSettingsService {_gameSettingsService.GameSettings.FiguresSpeed}");
        }

        public void Dispose()
        {
        
        }
    }
}

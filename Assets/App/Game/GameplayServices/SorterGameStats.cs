using System;
using App.Game.Signals;
using Zenject;
using Random = UnityEngine.Random;
using RangeInt = App.Game.Settings.CustomTypes.RangeInt;

namespace App.Game
{
    public class SorterGameStats : ISorterGameStats, IInitializable, IDisposable
    {
        private int _figuresRemainsToWin;
        private int _playerHealth;

        public int FiguresRemainsToWin => _figuresRemainsToWin;
        public int PlayerHealth => _playerHealth;

        private SignalBus _signalBus;
        private IGameSettingsService _gameSettingsService;
    
        [Inject]
        public SorterGameStats(SignalBus signalBus, IGameSettingsService gameSettingsService)
        {
            _signalBus = signalBus;
            _gameSettingsService = gameSettingsService;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<FigureDroppedToHoleSignal>(FigureInstallHandler);
            _signalBus.Subscribe<FigureKilledSignal>(FigureReachedHandler);
            InitGameplaySettings();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FigureDroppedToHoleSignal>(FigureInstallHandler);
            _signalBus.Unsubscribe<FigureKilledSignal>(FigureReachedHandler);
        }

        private void InitGameplaySettings()
        {
            _playerHealth = _gameSettingsService.GameSettings.PlayerHealth;
            _figuresRemainsToWin = GetRandomIntFromRange(_gameSettingsService.GameSettings.FiguresCountToWin); 
        }

        private void FigureInstallHandler()
        {
            _figuresRemainsToWin--;
            _signalBus.Fire<PlayerStatsUpdateSignal>();
            if (_figuresRemainsToWin<=0)
                _signalBus.Fire(new GameCompleteSignal(true));
        }

        private void FigureReachedHandler()
        {
            _playerHealth--;
            _signalBus.Fire<PlayerStatsUpdateSignal>();
            if (_playerHealth<=0)
                _signalBus.Fire(new GameCompleteSignal(false));
        }

        private int GetRandomIntFromRange(RangeInt rangeInt) => Random.Range(rangeInt.Min, rangeInt.Max);
    }
}

using System;
using App.Game.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using RangeInt = App.Game.Settings.CustomTypes.RangeInt;

namespace App.Game
{
    public class SorterGameStats : ISorterGameStats, IInitializable, IDisposable
    {
        private int _figuresRemainsToWin;
        private int _playerHealth;
        private int _playerScore;

        public int FiguresRemainsToWin => _figuresRemainsToWin;
        public int PlayerScore => _playerScore;
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
            _playerScore++;
            _figuresRemainsToWin--;
             _signalBus.Fire<PlayerStatsUpdateSignal>();
        }

        private void FigureReachedHandler()
        {
            _playerHealth--;
            _signalBus.Fire<PlayerStatsUpdateSignal>();
        }

        private int GetRandomIntFromRange(RangeInt rangeInt) => Random.Range(rangeInt.Min,rangeInt.Max);
    }
}

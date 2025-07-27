using System;
using App.Game.Signals;
using Zenject;

namespace App.Game
{
    public class SorterGame : IInitializable, IDisposable, ISorterGame
    {
        private readonly SignalBus _signalBus;
        private readonly ISorterGameStats _sorterGameStats;
    
        [Inject]
        SorterGame(
            SignalBus signalBus,
            ISorterGameStats sorterGameStats)
        {
            _signalBus = signalBus;
            _sorterGameStats = sorterGameStats;
        }

        public void Initialize() => _signalBus.Subscribe<PlayerStatsUpdateSignal>(PlayerStatsUpdateHandler);

        public void Dispose() => _signalBus.Unsubscribe<PlayerStatsUpdateSignal>(PlayerStatsUpdateHandler);

        public void StartGame() => _signalBus.Fire(new GameStartSignal());

        private void PlayerStatsUpdateHandler()
        {
            if (_sorterGameStats.PlayerHealth <= 0)
            {
                _signalBus.Fire(new GameCompleteSignal(false));
                return;
            }
            
            if (_sorterGameStats.FiguresRemainsToWin <= 0)
            {
                _signalBus.Fire(new GameCompleteSignal(true));
            }
        }
    }
}

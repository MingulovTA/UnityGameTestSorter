using System;
using App.Game.Signals;
using App.Services.Popups;
using AppData;
using Arpa_common.General.Extentions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace App.Game
{
    public class SorterGame : IInitializable, IDisposable, ISorterGame
    {
        private readonly IPopupService _popupService;
        private readonly SignalBus _signalBus;
        private readonly ISorterGameStats _sorterGameStats;
    
        [Inject]
        SorterGame(
            SignalBus signalBus,
            ISorterGameStats sorterGameStats,
            IPopupService popupService)
        {
            _signalBus = signalBus;
            _sorterGameStats = sorterGameStats;
            _popupService = popupService;
        }

        public void Initialize() => _signalBus.Subscribe<PlayerStatsUpdateSignal>(PlayerStatsUpdateHandler);

        public void Dispose() => _signalBus.Unsubscribe<PlayerStatsUpdateSignal>(PlayerStatsUpdateHandler);

        public void StartGame() => _signalBus.Fire(new GameStartSignal());

        private void PlayerStatsUpdateHandler()
        {
            if (_sorterGameStats.PlayerHealth <= 0)
            {
                _signalBus.Fire(new GameCompleteSignal(false));
                _popupService.Open<PopupComplete>(PopupId.Lose, PopupCloseHandler)
                    .Init(_sorterGameStats.PlayerScore);
                return;
            }
            
            if (_sorterGameStats.FiguresRemainsToWin <= 0)
            {
                _signalBus.Fire(new GameCompleteSignal(true));
                _popupService.Open<PopupComplete>(PopupId.Win, PopupCloseHandler)
                    .Init(_sorterGameStats.PlayerScore);
                
            }
        }

        private void PopupCloseHandler(PopupCloseResult pcr) => SceneManager.LoadScene(0);
    }
}

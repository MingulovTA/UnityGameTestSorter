using App.Game.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Game.UI
{
    public class PlayerHealthIndicator : MonoBehaviour
    {
        [SerializeField] private Text _textLabel;

        private ISorterGameStats _sorterGameStats;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(
            ISorterGameStats sorterGameStats,
            SignalBus signalBus)
        {
            _sorterGameStats = sorterGameStats;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<PlayerStatsUpdateSignal>(UpdateView);
            _signalBus.Subscribe<GameStartSignal>(UpdateView);
            UpdateView();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<PlayerStatsUpdateSignal>(UpdateView);
            _signalBus.Unsubscribe<GameStartSignal>(UpdateView);
        }
        private void UpdateView()
        {
            _textLabel.text = _sorterGameStats.PlayerHealth.ToString();
        }
    }
}

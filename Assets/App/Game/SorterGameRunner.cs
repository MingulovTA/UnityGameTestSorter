using System.Collections;
using App.Services.Popups;
using AppData;
using UnityEngine;
using Zenject;

namespace App.Game
{
    public class SorterGameRunner : MonoBehaviour
    {
        private ISorterGame _sorterGame;

        [Inject]
        private void Construct(ISorterGame sorterGame) => _sorterGame = sorterGame;

        private void Start() => _sorterGame.StartGame();
    }
}

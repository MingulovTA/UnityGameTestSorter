using System.Collections.Generic;
using System.Linq;
using App.Game.GameField;
using UnityEngine;

namespace App.Game.View.GameField
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private List<FigurePathView> _figurePaths;
        [SerializeField] private ParticleSystem _fxPoof;

        public ParticleSystem FxPoof => _fxPoof;
        public List<FigurePathView> FigurePaths => _figurePaths;

        private void OnValidate() => _figurePaths = GetComponentsInChildren<FigurePathView>().ToList();
    }
}

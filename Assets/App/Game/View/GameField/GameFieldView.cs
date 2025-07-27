using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Game.GameField
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private List<FigurePathView> _figurePaths;

        public List<FigurePathView> FigurePaths => _figurePaths;

        private void OnValidate() => _figurePaths = GetComponentsInChildren<FigurePathView>().ToList();
    }
}

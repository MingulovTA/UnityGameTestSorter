using App.Game.Figure;
using UnityEngine;

namespace App.Game.Signals
{
    public class FigureReachedSignal
    {
        public FigureView FigureView;

        public FigureReachedSignal(FigureView figureView)
        {
            FigureView = figureView;
        }
    }
}

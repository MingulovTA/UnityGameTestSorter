using App.Game.Figure;
using UnityEngine;

namespace App.Game.Signals
{
    public class FigureKilledSignal
    {
        public FigureView FigureView;

        public FigureKilledSignal(FigureView figureView)
        {
            FigureView = figureView;
        }
    }
}

using App.Game.Figure;

namespace App.Game.Signals
{
    public class FigureDroppedToHoleSignal
    {
        public FigureView FigureView;

        public FigureDroppedToHoleSignal(FigureView figureView)
        {
            FigureView = figureView;
        }
    }
}

using App.Game.Figure;

namespace App.Game.Pools
{
    public interface IFiguresPool
    {
        FigureView GetRandomFigureOf(ShapeTypeId shapeTypeId);
        void KillAllActiveFigures();
    }
}
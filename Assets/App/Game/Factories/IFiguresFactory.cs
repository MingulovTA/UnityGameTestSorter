using App.Game.Figure;

namespace App.Game
{
    public interface IFiguresFactory
    {
        FigureView GetRandomFigureByPath(string figurePath);
    }
}
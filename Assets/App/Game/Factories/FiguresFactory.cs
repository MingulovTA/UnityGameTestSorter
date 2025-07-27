using App.Game.Figure;
using Zenject;

namespace App.Game
{
    public class FiguresFactory : IFiguresFactory
    {
        private IInstantiator _instantiator;

        public FiguresFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public FigureView GetRandomFigureByPath(string figurePath)
        {
            var figure = _instantiator.InstantiatePrefabResourceForComponent<FigureView>(figurePath);
            figure.Path = figurePath;
            return figure;
        }
    }
}

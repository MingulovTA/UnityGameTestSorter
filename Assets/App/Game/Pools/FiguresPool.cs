using System;
using System.Collections.Generic;
using System.Linq;
using App.Game.Figure;
using App.Game.Signals;
using Arpa_common.General.Extentions;
using Zenject;

namespace App.Game.Pools
{
    public class FiguresPool : IFiguresPool, IInitializable, IDisposable
    {
        private readonly Dictionary<ShapeTypeId, List<string>> _figuresPaths = new Dictionary<ShapeTypeId, List<string>>
        {
            {ShapeTypeId.Circle, new List<string> {"Circle1", "Circle2", "Circle3", "Circle4"}},
            {ShapeTypeId.Square, new List<string> {"Square1", "Square2", "Square3", "Square4", "Square5"}},
            {ShapeTypeId.Star, new List<string> {"Star1", "Star2", "Star3", "Star4"}},
            {ShapeTypeId.Triangle, new List<string> {"Triangle1", "Triangle2", "Triangle3", "Triangle4"}},
        };

        private List<FigureView> _freeFigures = new List<FigureView>();

        private IFiguresFactory _figuresFactory;
        private SignalBus _signalBus;

        [Inject]
        public FiguresPool(
            IFiguresFactory figuresFactory,
            SignalBus signalBus)
        {
            _figuresFactory = figuresFactory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<FigureKilledSignal>(FigureKilledHandler);
            _signalBus.Subscribe<FigureDroppedToHoleSignal>(FigureDroppedToHoleHandler);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FigureKilledSignal>(FigureKilledHandler);
            _signalBus.Unsubscribe<FigureDroppedToHoleSignal>(FigureDroppedToHoleHandler);
        }

        public FigureView GetRandomFigureOf(ShapeTypeId shapeTypeId)
        {
            string figurePath = $"Figures/{_figuresPaths[shapeTypeId].GetRandomItem()}";
            var freeFigureOrNull = _freeFigures.FirstOrDefault(ff => ff.Path == figurePath);
            if (freeFigureOrNull!=null)
                return Pop(freeFigureOrNull);

            return _figuresFactory.GetRandomFigureByPath(figurePath);
        }

        private FigureView Pop(FigureView freeFigure)
        {
            _freeFigures.Remove(freeFigure);
            freeFigure.gameObject.SetActive(true);
            return freeFigure;
        }

        private void FigureKilledHandler(FigureKilledSignal figureKilledSignal) =>
            Push(figureKilledSignal.FigureView);

        private void FigureDroppedToHoleHandler(FigureDroppedToHoleSignal figureDroppedToHoleSignal) =>
            Push(figureDroppedToHoleSignal.FigureView);

        private void Push(FigureView figureView)
        {
            figureView.gameObject.SetActive(false);
            figureView.AddTo(_freeFigures);
        }
    }
}

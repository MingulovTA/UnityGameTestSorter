using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Game.Figure;
using App.Game.GameField;
using App.Game.Pools;
using App.Game.Settings.CustomTypes;
using App.Game.Signals;
using App.Game.View.GameField;
using App.Services.Runners;
using Arpa_common.General.Extentions;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace App.Game
{
    public class FiguresSpawner : IInitializable, IDisposable
    {
        private readonly List<ShapeTypeId> AllShapes = new List<ShapeTypeId>
            {ShapeTypeId.Circle, ShapeTypeId.Square, ShapeTypeId.Star, ShapeTypeId.Triangle};

        private IGameSettingsService _gameSettingsService;
        private GameFieldView _gameFieldView;
        private ICoroutineRunner _coroutineRunner;
        private IFiguresPool _figuresPool;
        private SignalBus _signalBus;

        private Coroutine _spawnCoroutine;
        private ShapeTypeId _lastShapeTypeId;
        private FigurePathView _lastFigurePath;
    
        [Inject] FiguresSpawner(
            IGameSettingsService gameSettingsService,
            GameFieldView gameFieldView,
            ICoroutineRunner coroutineRunner,
            IFiguresPool figuresPool,
            SignalBus signalBus)
        {
            _gameSettingsService = gameSettingsService;
            _gameFieldView = gameFieldView;
            _coroutineRunner = coroutineRunner;
            _figuresPool = figuresPool;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<GameStartSignal>(StartSpawning);
            _signalBus.Subscribe<GameCompleteSignal>(StopSpawning);
            StartSpawning();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartSignal>(StartSpawning);
            _signalBus.Unsubscribe<GameCompleteSignal>(StopSpawning);
        }

        private void StartSpawning()
        {
            if (_spawnCoroutine!=null)
                _coroutineRunner.Stop(_spawnCoroutine);
            _spawnCoroutine = _coroutineRunner.Run(SpawnCoroutine());
        }

        private void StopSpawning() => _coroutineRunner.Stop(_spawnCoroutine);

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                SpawnRandomFigure();
                yield return new WaitForSeconds(
                    GetRandomFloatFromRange(_gameSettingsService.GameSettings.DelayBeforeSpawnFigure));
            }
        }

        private void SpawnRandomFigure()
        {
            _lastShapeTypeId = AllShapes.Where(shape => shape != _lastShapeTypeId).ToList().GetRandomItem();
            _lastFigurePath = _gameFieldView.FigurePaths.Where(fp => fp != _lastFigurePath).ToList().GetRandomItem();
        
            FigureView figureView = _figuresPool.GetRandomFigureOf(_lastShapeTypeId);
            figureView.Init(_lastFigurePath,GetRandomFloatFromRange(_gameSettingsService.GameSettings.FiguresSpeed),_gameFieldView.FxPoof);
            figureView.transform.SetParent(_gameFieldView.transform);
            figureView.Go();
        }
    
        private float GetRandomFloatFromRange(RangeFloat rangeFloat) => Random.Range(rangeFloat.Min, rangeFloat.Max);
    }
}

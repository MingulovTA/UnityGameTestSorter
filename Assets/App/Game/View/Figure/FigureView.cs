using App.Game.GameField;
using App.Game.Holes;
using App.Game.Signals;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace App.Game.Figure
{
    public class FigureView : MonoBehaviour
    {
        private const float GOTO_ORIGIN_TIME = .25f;

        public string Path { get; set; }

        [SerializeField] private Transform _transform;
        [SerializeField] private ShapeTypeId _shapeTypeId;
        [SerializeField] private DragItem _dragItem;

        private FigurePathView _figurePathView;
        private float _speed;
        private Tween _movingTween;
        private Tween _dragItemTween;
        private HoleView _nearestHole;

        private SignalBus _signalBus;
        private ParticleSystem _fxPoof;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _dragItem.OnPress += PressHandler;
            _dragItem.OnRelease += ReleaseHandler;
            
        }

        private void OnDisable()
        {
            _dragItem.OnPress += PressHandler;
            _dragItem.OnRelease += ReleaseHandler;
            _movingTween?.Kill();
            _dragItemTween?.Kill();
        }

        public void Init(FigurePathView figurePathView, float speed, ParticleSystem fxPoof)
        {
            _figurePathView = figurePathView;
            _speed = speed;
            _dragItem.transform.localPosition = Vector3.zero;
            _fxPoof = fxPoof;
        }

        public void Go()
        {
            _transform.position = _figurePathView.StartWp.position;
            _movingTween = _transform.DOMove(_figurePathView.EndWp.position, 1f / _speed)
                .SetEase(Ease.Linear)
                .OnComplete(Kill);
        }
        
        private void Kill()
        {
            KillWithoutEvent();
            _signalBus.Fire(new FigureKilledSignal(this));
        }
        
        public void KillWithoutEvent()
        {
            _fxPoof.transform.position = _dragItem.transform.position+Vector3.back;
            _fxPoof.Play();
            _movingTween?.Kill();
            _dragItemTween?.Kill();
        }


        private void PressHandler()
        {
            _dragItemTween?.Kill();
            _movingTween.Pause();
        }

        private void ReleaseHandler()
        {
            if (_nearestHole == null)
            {
                _movingTween.Play();
                MoveDragItemToOrigin();
                return;
            }
            
            if (_nearestHole.ShapeTypeId!=_shapeTypeId)
            {
                Kill();
            }
            else
            {
                HideInHole();
            }
        }

        private void HideInHole()
        {
            _dragItemTween?.Kill();
            _dragItemTween = _dragItem.transform.DOMove(_nearestHole.transform.position, 0.1f).OnComplete(delegate
            {
                _signalBus.Fire(new FigureDroppedToHoleSignal(this));
            });
        }

        private void MoveDragItemToOrigin()
        {
            _dragItemTween?.Kill();
            _dragItemTween = _dragItem.transform.DOLocalMove(Vector3.zero, GOTO_ORIGIN_TIME);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var holeOrNull = other.GetComponent<HoleView>();
            if (holeOrNull!=null)
                _nearestHole = holeOrNull;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var holeOrNull = other.GetComponent<HoleView>();
            if (holeOrNull != null && _nearestHole == holeOrNull)
                _nearestHole = null;
        }
    }
}

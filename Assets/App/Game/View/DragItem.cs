using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace App.Game
{
    public class DragItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private bool _isPressed;
        
        private Camera _camera;

        private Vector3 _cursorPosition;
        private Vector3 _currentPosition;
    
        public event Action OnPress;
        public event Action OnRelease;

        [Inject]
        private void Construct(Camera camera)
        {
            _camera = camera;
        }

        private void Awake() => _currentPosition.z = _transform.position.z;

        private void Update()
        {
            if (!_isPressed) return;
            _cursorPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _currentPosition.x = _cursorPosition.x;
            _currentPosition.y = _cursorPosition.y;
            _transform.position = _currentPosition;
        }

        public void OnPointerDown(PointerEventData eventData) => Press();

        public void OnPointerUp(PointerEventData eventData) => Release();
        
        private void Press()
        {
            _isPressed = true;
            OnPress?.Invoke();
        }

        private void Release()
        {
            _isPressed = false;
            OnRelease?.Invoke();
        }
    }
}

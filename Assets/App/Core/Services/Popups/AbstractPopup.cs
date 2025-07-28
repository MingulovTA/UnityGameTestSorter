using System;
using App.Services.Runners;
using UnityEngine;
using Zenject;

namespace App.Services.Popups
{
    public abstract class AbstractPopup : MonoBehaviour
    {
        protected virtual bool IsReadyToClose => _isReadyToClose;
        protected bool _isReadyToClose;
        protected Action<PopupCloseResult> _closeCallback;
        protected ICoroutineRunner _coroutineRunner;
        protected RectTransform _showPlacementOrNull;
        
        protected IPopupService _popupService;

        [Inject] 
        private void Construct(
            IPopupService popupService,
            ICoroutineRunner coroutineRunner
            )
        {
            _popupService = popupService;
            _coroutineRunner = coroutineRunner;
        }

        public void LockForClosing()
        {
            _isReadyToClose = false;
        }

        public void UnlockForClosing()
        {
            _isReadyToClose = true;
        }

        public void Open(Action<PopupCloseResult> closeCallback, RectTransform showPlacement)
        {
            LockForClosing();
            _closeCallback = closeCallback;
            _showPlacementOrNull = showPlacement;
            ShowAnimation();
        }

        public void Close(PopupCloseResult result = PopupCloseResult.Cancel)
        {
            if (!_isReadyToClose) return;
            _isReadyToClose = false;
            StartHideAnimation(result);
            
        }

        protected virtual void ShowAnimation()
        {
            gameObject.SetActive(true);
            OnCompleteShowingAnimation();
        }

        protected virtual void StartHideAnimation(PopupCloseResult result)
        {
            gameObject.SetActive(false);
            _closeCallback.Invoke(result);
        }

        protected virtual void OnCompleteShowingAnimation()
        {
            UnlockForClosing();
        }
    }
}
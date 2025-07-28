using System;
using System.Collections;
using System.Collections.Generic;
using ArpaSubmodules.ArpaCommon.General.Extentions.Tween;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Services.Popups
{
    public class BasePopup : AbstractPopup
    {
        [Header("BasePopup settings")]
        [SerializeField] private float _showHideTime = 0.4f;
        [SerializeField] private Vector3 _hiddenLocalPosition = new Vector3(0,-1500,0);
        [SerializeField] private Image _bgBlackImage;
        [SerializeField] private float _bgShowedAlphaValue = 0.85f;
        [SerializeField] private Transform _rootTransform;

        
        [SerializeField] private List<Transform> _showByTweenItems;
        [SerializeField] private float _sequentiallyTweenDelay = 0;
        [SerializeField] private float _itemsShowHideTime = 0;
        [SerializeField] private bool _isNeedToWaitForPopupShowing = true;
        
        [SerializeField] private string _showHideSfxKey;
        [SerializeField] private string _showItemSfxKey;

        public event Action<Transform> OnItemShowed;

        protected override void StartHideAnimation(PopupCloseResult result) => 
            StartCoroutine(HideAnimation(result));

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _popupService.LastPopup == this)
                Close(PopupCloseResult.No);
        }

        protected override void ShowAnimation()
        {
            PreinitPopupMomentary();
            _coroutineRunner.Run(ShowAnimationYield());
        }

        private void PreinitPopupMomentary()
        {
            if (_showPlacementOrNull != null)
            {
                _rootTransform.SetParent(_showPlacementOrNull);
                _rootTransform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                _rootTransform.localScale = Vector3.zero;
            }
            else
            {
                _rootTransform.localPosition = _hiddenLocalPosition;
            }

            _bgBlackImage.SetAlpha(0);
            foreach (var t in _showByTweenItems)
                t.localScale = Vector3.zero;
        }

        private IEnumerator ShowAnimationYield()
        {
            gameObject.SetActive(true);
            yield return null;
            _rootTransform.SetParent(transform);
            _rootTransform.DOLocalMove(Vector3.zero, _showHideTime)
                .SetEase(_showPlacementOrNull==null?Ease.OutBack:Ease.Unset);
            _rootTransform.DOScale(Vector3.one, _showHideTime);
            
            _bgBlackImage.SetAlpha(_bgShowedAlphaValue,_showHideTime);
            
            if (_isNeedToWaitForPopupShowing)
                yield return new WaitForSeconds(_showHideTime);
            
            for (var i = 0; i < _showByTweenItems.Count; i++)
            {
                OnItemShowed?.Invoke(_showByTweenItems[i]);
                _showByTweenItems[i].DOScale(Vector3.one, _itemsShowHideTime)
                    .SetEase(Ease.OutBack);
                yield return new WaitForSeconds(_sequentiallyTweenDelay);
            }
            OnCompleteShowingAnimation();
        }

        protected virtual IEnumerator HideAnimation(PopupCloseResult result)
        {
            foreach (var showByTweenItem in _showByTweenItems)
                showByTweenItem.DOScale(Vector3.zero, _itemsShowHideTime);

            _bgBlackImage.SetAlpha(0, _showHideTime);
            if (_showPlacementOrNull == null)
            {
                _rootTransform.DOLocalMove(_hiddenLocalPosition, _showHideTime).SetEase(Ease.InBack);
            }
            else
            {
                _rootTransform.DOScale(Vector3.zero, _showHideTime);
                _rootTransform.DOMove(_showPlacementOrNull.position, _showHideTime);
            }
            yield return new WaitForSeconds(_showHideTime);
            yield return null;
            _closeCallback?.Invoke(result);
        }
    }
}

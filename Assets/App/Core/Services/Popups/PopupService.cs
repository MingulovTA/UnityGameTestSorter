using System;
using System.Collections.Generic;
using System.Linq;
using AppData;
using Arpa_common.General.Extentions;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace App.Services.Popups
{
    public class PopupService : IPopupService
    {
        private const string POPUPS_RESOURCE_PATH = "UI/Popups/";

        private IPopupsContainer _popupsContainer;
        private IInstantiator _instantiator;
        
        private Dictionary<BasePopup,Action<PopupCloseResult>> _popupsQueue = new Dictionary<BasePopup, Action<PopupCloseResult>>();
        public bool IsAnyPopupOpened => _popupsQueue.Count > 0;
        public BasePopup LastPopup => _popupsQueue.Last().Key;
        public event Action<PopupId> OnPopupOpened;

        [Inject]
        public PopupService(IInstantiator instantiator, IPopupsContainer popupsContainer)
        {
            _instantiator = instantiator;
            _popupsContainer = popupsContainer;
        }
        
        public void Open(PopupId popupId, Action<PopupCloseResult> closeCallback = null, RectTransform showPlacement = null)
        {
            Open<BasePopup>(popupId,closeCallback,showPlacement);
        }

        public T Open<T>(PopupId popupId, Action<PopupCloseResult> closeCallback = null,
            RectTransform showPlacement = null) where T : class
        {
            var basePopup =
                _instantiator.InstantiatePrefabResourceForComponent<BasePopup>($"{POPUPS_RESOURCE_PATH}{popupId}",
                    _popupsContainer.PopupsSceneWp);
            _popupsQueue.Add(basePopup, closeCallback);
            basePopup.Open(PopupCloseHandler, showPlacement);
            _popupsContainer.Canvas.gameObject.SetActive(true);
            OnPopupOpened?.Invoke(popupId);
            return basePopup as T;
        }

        public void ClosePopups()
        {
            if (_popupsQueue.Count>0)
                _popupsQueue.Last().Key.Close();
        }

        private void PopupCloseHandler(PopupCloseResult closeResult)
        {
            Object.Destroy(_popupsQueue.Last().Key.gameObject);
            var externalCallback = _popupsQueue.Last().Value;
            _popupsQueue.Remove(_popupsQueue.Last().Key);
            if (_popupsQueue.Count==0)
                _popupsContainer.Canvas.gameObject.SetActive(false);
            externalCallback?.Invoke(closeResult);
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
}

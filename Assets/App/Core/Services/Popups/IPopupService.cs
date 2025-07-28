using System;
using AppData;
using UnityEngine;

namespace App.Services.Popups
{
    public interface IPopupService
    {
        void Open(PopupId popupId, Action<PopupCloseResult> closeCallback = null, RectTransform showPlacement = null); public T Open<T>(PopupId popupId, Action<PopupCloseResult> closeCallback = null, RectTransform showPlacement = null) where T : class;
        void ClosePopups();
        bool IsAnyPopupOpened { get; }
        BasePopup LastPopup { get; }
        
        event  Action<PopupId> OnPopupOpened;
    }
}
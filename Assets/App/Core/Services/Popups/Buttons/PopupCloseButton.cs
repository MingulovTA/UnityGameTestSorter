using System;
using App.Services.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace App.Services.Popups.Buttons
{
    [RequireComponent(typeof(Button))]
    public class PopupCloseButton : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] protected AbstractPopup _basePopup;
        [SerializeField] protected PopupCloseResult _popupCloseResult = PopupCloseResult.Ok;

        private void OnEnable()
        {
            _btn.onClick.AddListener(Click);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveListener(Click);
        }

        public virtual void Click()
        {
            _basePopup.Close(_popupCloseResult);
        }

        private void OnValidate()
        {
            _btn = GetComponent<Button>();
            Transform t = transform;
            while (t!=null&&_basePopup==null)
            {
                AbstractPopup basePopup = t.GetComponent<AbstractPopup>();
                if (basePopup != null)
                {
                    _basePopup = basePopup;
                }
                else
                {
                    t = t.parent;
                }
            }
        }
    }
}
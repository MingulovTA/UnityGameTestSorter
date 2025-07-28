using AppData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Services.Popups.Buttons
{
    [RequireComponent(typeof(Button))]
    public class PopupOpenButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _showPlacementOrNull;
        [SerializeField] private PopupId _popupId;
        [SerializeField] private Button _btn;

        private IPopupService _popupService;

        [Inject]
        private void Construct(IPopupService popupService)
        {
            _popupService = popupService;
        }

        private void OnEnable()
        {
            _btn.onClick.AddListener(Click);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveListener(Click);
        }

        private void Click()
        {
            _popupService.Open(_popupId,null, _showPlacementOrNull);
        }

        private void OnValidate()
        {
            _btn = GetComponent<Button>();
        }
    }
}

using UnityEngine;

namespace App.Services.Popups
{
    public class PopupsContainer : MonoBehaviour, IPopupsContainer
    {
        [SerializeField] private Transform _popupsSceneWp;
        [SerializeField] private Canvas _canvas;
        public Transform PopupsSceneWp => _popupsSceneWp;
        public Canvas Canvas => _canvas;
    }
}
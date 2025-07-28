using UnityEngine;

namespace App.Services.Popups
{
    public interface IPopupsContainer
    {
        public Transform PopupsSceneWp { get; }
        public Canvas Canvas { get; }
    }
}
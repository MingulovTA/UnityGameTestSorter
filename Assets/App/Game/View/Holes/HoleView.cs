using UnityEngine;

namespace App.Game.Holes
{
    public class HoleView : MonoBehaviour
    {
        [SerializeField] private ShapeTypeId _shapeTypeId;

        public ShapeTypeId ShapeTypeId => _shapeTypeId;
    }
}

using UnityEngine;

namespace App.Game.GameField
{
    public class FigurePathView : MonoBehaviour
    {
        [SerializeField] private Transform _startWp;
        [SerializeField] private Transform _endWp;

        public Transform StartWp => _startWp;

        public Transform EndWp => _endWp;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(_startWp.position, _endWp.position);
        }
    }
}

using UnityEngine;

namespace Arpa_common.General.Extentions.Tween
{
    public class RotatingItem : MonoBehaviour
    {
        [SerializeField] private Vector3 _speed = new Vector3(0,0,5);
        private Transform _transform;
        private Vector3 _angle;

        private void Awake()
        {
            _transform = transform;
            _angle = _transform.localEulerAngles;
        }


        private void Update()
        {
            _angle+=_speed*Time.deltaTime;
            _transform.localEulerAngles = _angle;
        }
    }
}

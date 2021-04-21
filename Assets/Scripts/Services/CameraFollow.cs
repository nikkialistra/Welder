using UnityEngine;

namespace Services
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _smoothSpeed;

        private Vector3 _offset;

        private void Start() => _offset = transform.position - _target.position;

        private void LateUpdate()
        {
            var desiredPosition = _target.position + _offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
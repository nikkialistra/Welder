using UnityEngine;

namespace Welder
{
    public class MouseLook : MonoBehaviour
    {
        public bool CanRotate { get; set; } = true;
        
        [SerializeField] private Transform _welder;
        
        [SerializeField] private float _mouseSensitivity = 100f;
        [SerializeField] private float _mouseSmoothTime = 0.03f;

        private float yRotation;
        
        private Vector2 _currentMouseDelta;
        private Vector2 _currentMouseDeltaVelocity;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (!CanRotate)
            {
                return;
            }
            
            var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, mouseDelta, ref _currentMouseDeltaVelocity, _mouseSmoothTime);

            yRotation -= _currentMouseDelta.y * _mouseSensitivity;
            yRotation = Mathf.Clamp(yRotation, -90f, 60);
            
            transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
            _welder.Rotate(Vector3.up * (mouseDelta.x * _mouseSensitivity));
        }
    }
}
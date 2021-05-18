using UnityEngine;

namespace Welder
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform _welder;
        
        [SerializeField] private float mouseSensitivity = 100f;

        private float yRotation;
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -90f, 90);
            
            transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
            _welder.Rotate(Vector3.up * mouseX);
        }
    }
}
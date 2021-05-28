using System;
using UI;
using UnityEngine;

namespace Welder
{
    public class MouseLook : MonoBehaviour
    {
        public bool CanRotate { get; set; } = true;
        
        [SerializeField] private Transform _welder;
        
        [SerializeField] private float _mouseSensitivity = 100f;
        [SerializeField] private float _mouseSmoothTime = 0.03f;

        [Space]
        [SerializeField] private Menu _menu;

        private float yRotation;
        
        private Vector2 _currentMouseDelta;
        private Vector2 _currentMouseDeltaVelocity;

        private void OnEnable()
        {
            _menu.Show += OnMenuShow;
            _menu.Hide += OnMenuHide;
        }

        private void OnDisable()
        {
            _menu.Show -= OnMenuShow;
            _menu.Hide -= OnMenuHide;
        }

        private void OnMenuHide()
        {
            CanRotate = true;
            BlockCursor();
        }

        private void OnMenuShow()
        {
            CanRotate = false;
            UnblockCursor();
        }

        private static void BlockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private static void UnblockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
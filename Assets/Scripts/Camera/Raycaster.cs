using System;
using UnityEngine;

namespace Camera
{
    public class Raycaster : MonoBehaviour
    {
        public event Action<Vector3> RaycastHit;
    
        [SerializeField] private LayerMask _floor;

        private UnityEngine.Camera _camera;

        private void Awake() => _camera = UnityEngine.Camera.main;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
                Raycast();
        }

        private void Raycast()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, _floor))
                RaycastHit?.Invoke(hit.point);
        }
    }
}
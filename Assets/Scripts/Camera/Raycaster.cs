using System;
using RoomObjects;
using UnityEngine;

namespace Camera
{
    public class Raycaster : MonoBehaviour
    {
        public event Action<Vector3> MoveToPoint;
        public event Action<Vector3, Vector3> MoveToInteractable;
    
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
            {
                var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if (interactable != null)
                    MoveToInteractable?.Invoke(interactable.InteractionPosition, interactable.InteractionLookAtPoint);
                else
                    MoveToPoint?.Invoke(hit.point);
            }
                
        }
    }
}
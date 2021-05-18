using System;
using RoomObjects.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services
{
    public class MouseHandler : MonoBehaviour
    {
        public event Action<Vector3> MoveToPoint;
        public event Action<IInteractable> MoveToInteractable;

        public event Action<IInteractable> Interact; 
        public event Action<Vector3> PutOnPoint; 
    
        [SerializeField] private LayerMask _validLayers;

        private UnityEngine.Camera _camera;
        
        private EventSystem _eventSystem;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _eventSystem = EventSystem.current;
        }

        private void Update()
        {
            if (_eventSystem.IsPointerOverGameObject())
                return;
            
            CheckForMoveSignal();
            CheckForInteractSignal();
        }

        private void CheckForMoveSignal()
        {
            if (Input.GetMouseButtonDown(0))
                TryValidateMoveSignal();
        }

        private void CheckForInteractSignal()
        {
            if (Input.GetMouseButtonDown(1))
                TryValidateInteractSignal();
        }

        private void TryValidateMoveSignal()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, _validLayers)) 
                return;
            
            var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.SelectBlink();
                MoveToInteractable?.Invoke(interactable);
            }
            else
            {
                MoveToPoint?.Invoke(hit.point);
            }

        }

        private void TryValidateInteractSignal()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, _validLayers)) 
                return;
            
            var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
                Interact?.Invoke(interactable);
            else
                PutOnPoint?.Invoke(hit.point);
        }
    }
}
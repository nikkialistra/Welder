﻿using System;
using RoomObjects.Contracts;
using UnityEngine;

namespace Camera
{
    public class MouseHandler : MonoBehaviour
    {
        public event Action<Vector3> MoveToPoint;
        public event Action<IInteractable> MoveToInteractable;

        public event Action<IInteractable> Interact; 
        public event Action InteractWithoutTarget; 
    
        [SerializeField] private LayerMask _validLayers;

        private UnityEngine.Camera _camera;

        private void Awake() => _camera = UnityEngine.Camera.main;

        private void Update()
        {
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
                InteractWithoutTarget?.Invoke();
        }
    }
}
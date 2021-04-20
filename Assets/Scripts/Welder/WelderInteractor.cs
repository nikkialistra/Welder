using System;
using Camera;
using RoomObjects;
using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(WelderMover))]
    [RequireComponent(typeof(Animator))]
    public class WelderInteractor : MonoBehaviour
    {
        [SerializeField] private MouseHandler _mouseHandler;
        
        [Space] [SerializeField] private bool _haveEquipment;

        private IInteractable _interactable;
        
        private WelderMover _welderMover;
        private WelderAnimator _welderAnimator;

        private void Awake()
        {
            _welderMover = GetComponent<WelderMover>();
            _welderAnimator = GetComponent<WelderAnimator>();
        }

        private void OnEnable()
        {
            _welderMover.InteractableGot += OnInteractableGot;
            _welderMover.InteractableReset += OnInteractableReset;

            _mouseHandler.Interact += OnInteract;
        }
        
        private void OnDisable()
        {
            _welderMover.InteractableGot -= OnInteractableGot;
            _welderMover.InteractableReset -= OnInteractableReset;

            _mouseHandler.Interact -= OnInteract;
        }

        private void OnInteractableGot(IInteractable interactable) => _interactable = interactable;

        private void OnInteractableReset() => _interactable = null;

        private void OnInteract(IInteractable interactable)
        {
            if (!IsValidInteraction(interactable))
                return;

            switch (interactable.InteractableType)
            {
                case InteractableType.Raise:
                    TryRaise(interactable);
                    break;
                case InteractableType.Weld:
                    TryWeld(interactable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsValidInteraction(IInteractable interactable)
        {
            if (_interactable == null)
                return false;

            if (_interactable != interactable)
                return false;

            return true;
        }

        private void TryRaise(IInteractable interactable)
        {
            if (_haveEquipment)
                _welderAnimator.Raise();
        }

        private void TryWeld(IInteractable interactable)
        {
            Debug.Log("welding");
        }
    }
}
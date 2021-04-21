using System;
using Camera;
using RoomObjects;
using RoomObjects.Contracts;
using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(WelderMover))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(WelderEquipment))]
    public class WelderInteractor : MonoBehaviour
    {
        [SerializeField] private MouseHandler _mouseHandler;

        [Space] [SerializeField] private bool _haveEquipment;

        private IInteractable _interactable;
        
        private WelderMover _welderMover;
        private WelderAnimator _welderAnimator;
        private WelderEquipment _welderEquipment;

        private void Awake()
        {
            _welderMover = GetComponent<WelderMover>();
            _welderAnimator = GetComponent<WelderAnimator>();
            _welderEquipment = GetComponent<WelderEquipment>();
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

        private void OnInteractableGot(IInteractable interactable)
        {
            _interactable = interactable;
            _interactable.Activate();
        }

        private void OnInteractableReset()
        {
            _interactable?.Deactivate();
            _interactable = null;
        }

        private void OnInteract(IInteractable interactable)
        {
            if (!IsValidInteraction(interactable))
                return;

            switch (interactable.GetInteractableType())
            {
                case InteractableType.Raise:
                    TryRaise(interactable);
                    break;
                case InteractableType.Weld:
                    TryWeld(interactable);
                    break;
                case InteractableType.Equip:
                    TryEquip(interactable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsValidInteraction(IInteractable interactable)
        {
            if (_interactable == null)
                return false;

            return _interactable == interactable;
        }

        private void TryEquip(IInteractable equipable)
        {
            var equipableGameObject = equipable.GameObject;
            var equipment = equipableGameObject.GetComponent<Equipment>();
            if (equipment == null)
                return;

            if (_welderEquipment.TryEquip(equipment.EquipmentType))
                Destroy(equipableGameObject);
        }

        private void TryRaise(IInteractable raisable)
        {
            if (_welderEquipment.HasFullPack())
                _welderAnimator.Raise();
        }

        private void TryWeld(IInteractable weldable)
        {
            Debug.Log("welding");
        }
    }
}
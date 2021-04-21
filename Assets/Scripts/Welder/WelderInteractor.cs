using System;
using System.Linq;
using RoomObjects;
using RoomObjects.Contracts;
using Services;
using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(WelderMover))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(WelderEquipment))]
    public class WelderInteractor : MonoBehaviour
    {
        public bool CanPut => _raisingObject != null;
        
        [SerializeField] private MouseHandler _mouseHandler;

        [Space]
        [SerializeField] private float _heightForUse;

        [SerializeField] private float _timeForUse;


        [Space]
        [SerializeField] private float _dangerDistance;

        private IInteractable _interactable;

        private Raisable _raisingObject;

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
            _welderMover.Put += OnPut;

            _mouseHandler.Interact += OnInteract;
        }

        private void OnDisable()
        {
            _welderMover.InteractableGot -= OnInteractableGot;
            _welderMover.InteractableReset -= OnInteractableReset;
            _welderMover.Put -= OnPut;

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
            StopWeldIfNeeded();
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
                    TryWeld();
                    break;
                case InteractableType.Equip:
                    TryEquip(interactable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnPut() => TryPutObject();

        private void TryPutObject()
        {
            if (!CanPut)
                return;
            
            _raisingObject.Put(-_heightForUse, _timeForUse);
            _welderAnimator.Put();

            _raisingObject = null;
        }

        private bool IsValidInteraction(IInteractable interactable)
        {
            if (_interactable == null)
                return false;

            if (CanPut)
                return false;

            return _interactable == interactable;
        }

        private void TryEquip(IInteractable interactable)
        {
            var equipable = interactable as Equipable;
            if (equipable == null)
                return;

            if (_welderEquipment.TryEquip(equipable.EquipmentType))
            {
                OnInteractableReset();
                Destroy(equipable.GameObject);
            }
        }

        private void TryRaise(IInteractable interactable)
        {
            if (!_welderEquipment.HasFullPack())
            {
                ShowMessage("У меня не полное снаряжение, не стоит поднимать так. " + _welderEquipment.GetLackingParts());
                return;
            }

            var raisable = interactable as Raisable;
            if (raisable == null)
                return;

            _raisingObject = raisable;
            raisable.Raise(transform, _heightForUse, _timeForUse);

            _welderAnimator.Raise();
        }

        private void TryWeld()
        {
            if (!_welderEquipment.HasFullPack())
            {
                ShowMessage("У меня не полное снаряжение, опасно заниматься сваркой. " + _welderEquipment.GetLackingParts());
                return;
            }

            if (CheckForDanger())
            {
                ShowMessage("Бочки рядом, опасно работать.");
                return;
            }
            
            _welderAnimator.StartWeld();
        }

        private bool CheckForDanger()
        {
            var raisables = FindObjectsOfType<Raisable>();
            return raisables.Any(raisable => Vector3.Distance(transform.position, raisable.transform.position) < _dangerDistance);
        }

        private void StopWeldIfNeeded() => _welderAnimator.StopWeld();

        private static void ShowMessage(string message) => Debug.Log(message);
    }
}
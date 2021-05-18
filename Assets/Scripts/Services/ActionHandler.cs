using System;
using RoomObjects;
using UnityEngine;
using UnityEngine.UI;
using Welder;

namespace Services
{
    [RequireComponent(typeof(ActionOutcome))]
    public class ActionHandler : MonoBehaviour
    {
        [SerializeField] private Equipment _equipment;

        [SerializeField] private GameObject _equipChoices;

        [SerializeField] private Button _checkEquipment;
        [SerializeField] private Button _use;

        private Interactable _interactable;

        private ActionOutcome _actionOutcome;
        
        private bool _equipmentChecked;

        private void Awake()
        {
            _actionOutcome = GetComponent<ActionOutcome>();
        }

        private void Update()
        {
            if (_interactable == null)
            {
                return;
            }

            CheckKeyPresses();
        }

        private void CheckKeyPresses()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CheckEquipment();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Use();
            }
        }

        public void ShowChoices(Interactable interactable)
        {
            switch (interactable.InteractableType)
            {
                case InteractableType.Equip:
                    var equipable = interactable.gameObject.GetComponent<Interactable>();
                    if (equipable == null)
                    {
                        return;
                    }
                    
                    _interactable = equipable;
                    break;
                case InteractableType.Raise:
                    break;
                case InteractableType.Weld:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _equipChoices.SetActive(true);

            _checkEquipment.onClick.AddListener(CheckEquipment);
            _use.onClick.AddListener(Use);
        }

        public void HideEquipmentChoices()
        {
            _equipChoices.SetActive(false);
            
            _checkEquipment.onClick.RemoveListener(CheckEquipment);
            _use.onClick.RemoveListener(Use);
        }

        private void CheckEquipment()
        {
            _checkEquipment.interactable = false;
            _equipmentChecked = true;
            
            _actionOutcome.ShowCorrect();
        }

        private void Use()
        {
            _checkEquipment.interactable = false;
            _use.interactable = false;

            if (!_equipmentChecked)
            {
                _equipment.Equip(_interactable, wasChecked: false);
                _actionOutcome.ShowDanger();
            }
            else
            {
                _equipment.Equip(_interactable, wasChecked: true);
                _actionOutcome.ShowCorrect();
            }

            HideEquipmentChoices();
        }
    }
}
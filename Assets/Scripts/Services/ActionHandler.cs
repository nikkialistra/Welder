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

        private Equipable _equipable;

        private ActionOutcome _actionOutcome;
        
        private bool _equipmentChecked;

        private void Awake()
        {
            _actionOutcome = GetComponent<ActionOutcome>();
        }

        public void ShowChoices(RoomObject roomObject)
        {
            switch (roomObject.InteractableType)
            {
                case InteractableType.Equip:
                    var equipable = roomObject.gameObject.GetComponent<Equipable>();
                    if (equipable == null)
                    {
                        return;
                    }
                    
                    _equipable = equipable;
                    break;
                case InteractableType.Raise:
                    break;
                case InteractableType.Weld:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _equipChoices.SetActive(true);

            _checkEquipment.onClick.AddListener(OnCheckEquipment);
            _use.onClick.AddListener(OnUse);
        }

        private void OnCheckEquipment()
        {
            _checkEquipment.interactable = false;
            _equipmentChecked = true;
            
            _actionOutcome.ShowCorrect();
        }

        private void OnUse()
        {
            if (!_equipmentChecked)
            {
                _equipment.EquipNotChecked(_equipable);
                _actionOutcome.ShowDanger();
            }
            else
            {
                _equipment.Equip(_equipable);
                _actionOutcome.ShowCorrect();
            }

            HideEquipmentChoices();
        }

        private void HideEquipmentChoices()
        {
            _equipChoices.SetActive(false);
            
            _checkEquipment.onClick.RemoveListener(OnCheckEquipment);
            _use.onClick.RemoveListener(OnUse);
        }
    }
}
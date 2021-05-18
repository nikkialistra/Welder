using System;
using RoomObjects;
using RoomObjects.Contracts;
using UnityEngine;
using UnityEngine.UI;
using Welder;

namespace Services
{
    [RequireComponent(typeof(ActionOutcome))]
    public class ActionHandler : MonoBehaviour
    {
        [SerializeField] private WelderEquipment _welderEquipment;

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

        public void ShowEquipChoices(IInteractable interactable)
        {
            var equipable = interactable as Equipable;
            if (equipable == null)
                throw new ArgumentException(nameof(interactable));
            else
                _equipable = equipable;
            
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
                _welderEquipment.TryEquipNotChecked(_equipable);
                _actionOutcome.ShowDanger();
            }
            else
            {
                _welderEquipment.TryEquip(_equipable);
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
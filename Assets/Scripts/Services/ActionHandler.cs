using RoomObjects.Interactables;
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

        private ActionOutcome _actionOutcome;

        private Equipable _equipable;
        private bool _showing;
        private bool _equipmentChecked;

        private void Awake()
        {
            _actionOutcome = GetComponent<ActionOutcome>();
        }

        private void Update()
        {
            if (!_showing)
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

        public void ShowChoices(Equipable equipable)
        {
            _showing = true;
            _equipable = equipable;
            
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
                _equipment.Equip(_equipable, wasChecked: false);
                _actionOutcome.ShowDanger();
            }
            else
            {
                _equipment.Equip(_equipable, wasChecked: true);
                _actionOutcome.ShowCorrect();
            }

            HideEquipmentChoices();
        }
    }
}
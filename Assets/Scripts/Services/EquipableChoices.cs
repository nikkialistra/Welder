using RoomObjects.Interactables;
using UnityEngine;
using UnityEngine.UI;
using Welder;

namespace Services
{
    [RequireComponent(typeof(ActionOutcome))]
    public class EquipableChoices : MonoBehaviour
    {
        [SerializeField] private Equipment _equipment;

        [SerializeField] private RectTransform _choices;

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
            if (_showing && _equipable == equipable)
            {
                return;
            }

            _showing = true;
            _equipable = equipable;
            
            ShowEquipmentChoices();
        }

        private void ShowEquipmentChoices()
        {
            _choices.gameObject.SetActive(true);

            _checkEquipment.interactable = !_equipable.IsChecked;

            _use.interactable = true;
            
            _checkEquipment.onClick.AddListener(CheckEquipment);
            _use.onClick.AddListener(Use);
        }

        private void HideEquipmentChoices()
        {
            _choices.gameObject.SetActive(false);
            
            _checkEquipment.onClick.RemoveListener(CheckEquipment);
            _use.onClick.RemoveListener(Use);
        }

        private void CheckEquipment()
        {
            _checkEquipment.interactable = false;
            _equipable.Check();
            
            _actionOutcome.ShowCorrect();
        }

        private void Use()
        {
            _checkEquipment.interactable = false;
            _use.interactable = false;

            _equipment.Equip(_equipable);
            
            if (_equipable.IsChecked)
            {
                _actionOutcome.ShowCorrect();
            }
            else
            {
                _actionOutcome.ShowDanger();

            }

            HideEquipmentChoices();
        }
    }
}
using System;
using System.Collections;
using RoomObjects.Interactables;
using UnityEngine;
using UnityEngine.UI;
using Welder;

namespace Services
{
    [RequireComponent(typeof(ActionOutcome))]
    public class EquipableChoices : MonoBehaviour, IChoicesShower
    {
        [SerializeField] private Equipment _equipment;

        [SerializeField] private RectTransform _equipableChoices;

        [SerializeField] private Button _checkEquipment;
        [SerializeField] private Button _use;

        private ActionOutcome _actionOutcome;

        private Equipable _equipable;
        private bool _showing;
        private bool _equipmentChecked;
        
        private Coroutine _hideAfterCoroutine;

        private void Awake()
        {
            _actionOutcome = GetComponent<ActionOutcome>();
        }

        private void Update()
        {
            if (!_showing || _equipable == null)
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

        public void Show(IInteractable interactable)
        {
            var equipable = interactable as Equipable;
            
            if (equipable == null)
            {
                throw new ArgumentException(nameof(interactable));
            }

            if (_showing && _equipable == equipable)
            {
                return;
            }

            _showing = true;
            _equipable = equipable;
            
            _equipableChoices.gameObject.SetActive(true);
            
            ShowEquipmentChoices();
        }

        private void Hide()
        {
            _equipable = null;
            
            _equipableChoices.gameObject.SetActive(false);
            
            _checkEquipment.onClick.RemoveListener(CheckEquipment);
            _use.onClick.RemoveListener(Use);

            _hideAfterCoroutine = StartCoroutine(HideAfter());
        }

        private void ShowEquipmentChoices()
        {
            if (_hideAfterCoroutine != null)
            {
                StopCoroutine(_hideAfterCoroutine);
            }

            _checkEquipment.interactable = !_equipable.IsChecked;

            _use.interactable = true;
            
            _checkEquipment.onClick.AddListener(CheckEquipment);
            _use.onClick.AddListener(Use);
        }

        private IEnumerator HideAfter()
        {
            yield return new WaitForSeconds(0.3f);
            _showing = false;
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

            Hide();
        }
    }
}
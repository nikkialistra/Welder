using System;
using System.Collections;
using RoomObjects.Interactables;
using UnityEngine;
using UnityEngine.UI;
using Welder;

namespace Services
{
    public class UseableChoices : MonoBehaviour, IChoicesShower
    {
        [SerializeField] private ItemHolder _itemHolder;

        [SerializeField] private RectTransform _useableChoices;
        
        [SerializeField] private Button _use;

        private Useable _useable;
        private bool _showing;

        private Coroutine _hideAfterCoroutine;

        private void Update()
        {
            if (!_showing || _useable == null)
            {
                return;
            }

            CheckKeyPresses();
        }

        private void CheckKeyPresses()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Use();
            }
        }

        public void Show(IInteractable interactable)
        {
            var useable = interactable as Useable;

            if (useable == null)
            {
                throw new ArgumentException(nameof(interactable));
            }

            if (_showing && _useable == useable)
            {
                return;
            }

            _showing = true;
            _useable = useable;
            
            _useableChoices.gameObject.SetActive(true);
            
            ShowEquipmentChoices();
        }

        private void Hide()
        {
            _useable = null;
            
            _useableChoices.gameObject.SetActive(false);
            
            _use.onClick.RemoveListener(Use);

            _hideAfterCoroutine = StartCoroutine(HideAfter());
        }

        private void ShowEquipmentChoices()
        {
            if (_hideAfterCoroutine != null)
            {
                StopCoroutine(_hideAfterCoroutine);
            }

            _use.interactable = true;
            
            _use.onClick.AddListener(Use);
        }

        private IEnumerator HideAfter()
        {
            yield return new WaitForSeconds(0.3f);
            _showing = false;
        }

        private void Use()
        {
            _use.interactable = false;

            _itemHolder.Take(_useable);

            Hide();
        }
    }
}
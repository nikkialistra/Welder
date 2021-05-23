using RoomObjects.Interactables;
using UnityEngine;
using UnityEngine.UI;
using Welder;

namespace Services
{
    public class UseableShower : MonoBehaviour
    {
        [SerializeField] private ItemHolder _itemHolder;
        [SerializeField] private Equipment _equipment;

        [SerializeField] private RectTransform _useableChoices;
        
        [SerializeField] private Button _use;

        private Useable _useable;
        private bool _showing;

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

        public void Show(Useable useable)
        {
            if (!_equipment.MaskEquiped || !_equipment.GlovesEquiped)
            {
                return;
            }

            if (_showing && _useable == useable)
            {
                return;
            }

            _showing = true;
            _useable = useable;
            
            _useableChoices.gameObject.SetActive(true);
            
            ShowUseableChoices();
        }

        private void Hide()
        {
            _showing = false;
            _useable = null;
            
            _useableChoices.gameObject.SetActive(false);

            _use.onClick.RemoveListener(Use);
        }

        private void ShowUseableChoices()
        {
            _use.interactable = true;
            
            _use.onClick.AddListener(Use);
        }

        private void Use()
        {
            _use.interactable = false;

            _itemHolder.Take(_useable);

            Hide();
        }
    }
}
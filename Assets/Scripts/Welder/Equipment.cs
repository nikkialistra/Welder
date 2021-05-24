using System;
using RoomObjects.Interactables;
using UnityEngine;

namespace Welder
{
    public class Equipment : MonoBehaviour
    {
        public bool MaskEquiped => _mask != null;
        public bool GlovesEquiped => _gloves != null;
        
        public bool MaskChecked
        {
            get
            {
                if (_mask == null)
                    throw new ArgumentNullException();

                return _mask.IsChecked;
            }
        }
        
        public bool GlovesChecked
        {
            get
            {
                if (_gloves == null)
                    throw new ArgumentNullException();

                return _gloves.IsChecked;
            }
        }

        [SerializeField] private RectTransform _weldingMask;

        [SerializeField] private Transform _handWithoutGloves;
        
        [SerializeField] private GameObject _handWithGlovesPrefab;
        [SerializeField] private Transform _handPosition;

        [SerializeField] private Equipable _mask;
        [SerializeField] private Equipable _gloves;
        
        private bool _wasChecked;

        private void Start()
        {
            EquipStartupEquipables();
        }

        private void EquipStartupEquipables()
        {
            if (_mask != null)
            {
                ShowMask();
            }

            if (_gloves != null)
            {
                ShowGloves();
            }
        }

        public void Equip(Equipable equipable)
        {
            switch (equipable.EquipableTypes)
            {
                case EquipableTypes.Mask:
                    _mask = equipable;
                    ShowMask();
                    break;
                case EquipableTypes.Gloves:
                    _gloves = equipable;
                    ShowGloves();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            equipable.Hide();
        }

        private void ShowMask()
        {
            _weldingMask.gameObject.SetActive(true);
        }

        private void ShowGloves()
        {
            Destroy(_handWithoutGloves.gameObject);
            
            Instantiate(_handWithGlovesPrefab, _handPosition);
        }
    }
}
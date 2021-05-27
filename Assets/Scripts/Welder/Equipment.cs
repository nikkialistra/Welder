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

        [Space]
        [SerializeField] private Equipable _mask;
        [SerializeField] private Equipable _gloves;
        
        [Space]
        [SerializeField] private AudioSource _equipMask;
        [SerializeField] private AudioSource _equipGloves;
        
        private bool _wasChecked;

        private void Start()
        {
            EquipStartupEquipables();
        }

        private void EquipStartupEquipables()
        {
            if (_mask != null)
            {
                EquipMask();
            }

            if (_gloves != null)
            {
                EquipGloves();
            }
        }

        public void Equip(Equipable equipable)
        {
            switch (equipable.EquipableTypes)
            {
                case EquipableTypes.Mask:
                    _mask = equipable;
                    _equipMask.Play();
                    EquipMask();
                    break;
                case EquipableTypes.Gloves:
                    _gloves = equipable;
                    _equipGloves.Play();
                    EquipGloves();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            equipable.Hide();
        }

        private void EquipMask()
        {
            _weldingMask.gameObject.SetActive(true);
        }

        private void EquipGloves()
        {
            Destroy(_handWithoutGloves.gameObject);
            
            Instantiate(_handWithGlovesPrefab, _handPosition);
        }
    }
}
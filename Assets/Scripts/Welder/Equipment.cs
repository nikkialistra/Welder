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

        [SerializeField] private MeshFilter _leftHand;
        [SerializeField] private MeshFilter _rightHand;
        
        [SerializeField] private Mesh _handInGlove;

        private Equipable _mask;
        private Equipable _gloves;
        
        private bool _wasChecked;

        public void Equip(Equipable equipable)
        {
            switch (equipable.EquipmentTypes)
            {
                case EquipmentTypes.Mask:
                    _mask = equipable;
                    ShowMask();
                    break;
                case EquipmentTypes.Gloves:
                    _gloves = equipable;
                    ShowGloves();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            equipable.Interact();
        }

        private void ShowMask()
        {
            _weldingMask.gameObject.SetActive(true);
        }

        private void ShowGloves()
        {
            // _leftHand.mesh = _handInGlove;
            // _rightHand.mesh = _handInGlove;
        }
    }
}
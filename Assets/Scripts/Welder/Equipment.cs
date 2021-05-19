using System;
using RoomObjects.Interactables;
using UnityEngine;

namespace Welder
{
    public class Equipment : MonoBehaviour
    {
        public bool MaskEquiped => _maskEquiped;
        public bool GlovesEquiped => _glovesEquiped;

        [SerializeField] private RectTransform _weldingMask;

        [SerializeField] private MeshFilter _leftHand;
        [SerializeField] private MeshFilter _rightHand;
        
        [SerializeField] private Mesh _handInGlove;

        private bool _maskEquiped;
        private bool _glovesEquiped;
        
        private bool _wasChecked;

        public void Equip(Equipable equipable, bool wasChecked)
        {
            switch (equipable.EquipmentTypes)
            {
                case RoomObjects.Interactables.EquipmentTypes.Mask:
                    _maskEquiped = true;
                    ShowMask();
                    break;
                case RoomObjects.Interactables.EquipmentTypes.Gloves:
                    _glovesEquiped = true;
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
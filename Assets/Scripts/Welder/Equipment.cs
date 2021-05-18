using RoomObjects;
using UnityEngine;

namespace Welder
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private RectTransform _weldingMask;

        [SerializeField] private MeshFilter _leftHand;
        [SerializeField] private MeshFilter _rightHand;
        
        [SerializeField] private Mesh _handInGlove;

        public bool Equiped => _equiped;

        private bool _equiped;
        private bool _wasChecked;

        public void Equip(Interactable interactable, bool wasChecked)
        {
            _equiped = true;
            _wasChecked = wasChecked;
            
            ShowEquipment();
            
            interactable.Interact();
        }

        private void ShowEquipment()
        {
            _weldingMask.gameObject.SetActive(true);

            // _leftHand.mesh = _handInGlove;
            // _rightHand.mesh = _handInGlove;
        }
    }
}
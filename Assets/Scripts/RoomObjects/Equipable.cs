using RoomObjects.Contracts;
using UnityEngine;

namespace RoomObjects
{
    [RequireComponent(typeof(MeshFilter))]
    public class Equipable : RoomObject
    {
        public override InteractableType GetInteractableType() => InteractableType.Equip;
        
        [SerializeField] private Mesh _mesh;

        public EquipmentType EquipmentType => _equipmentType;

        [SerializeField] private EquipmentType _equipmentType;
        
        private MeshFilter _meshFilter;

        protected override void OnAwake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void TakeOff()
        {
            _meshFilter.mesh = _mesh;
        }
    }
}
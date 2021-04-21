using RoomObjects.Contracts;
using UnityEngine;

namespace RoomObjects
{
    public class Equipment : RoomObject
    {
        public override InteractableType GetInteractableType() => InteractableType.Equip;
        
        public EquipmentType EquipmentType => _equipmentType;

        [SerializeField] private EquipmentType _equipmentType;
    }
}
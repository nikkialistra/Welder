using RoomObjects;
using UnityEngine;

namespace Welder
{
    public class Equipment : MonoBehaviour
    {
        public bool Equiped => _equiped;

        private bool _equiped;

        public void Equip(Equipable equipable)
        {
            _equiped = true;
            equipable.TakeOff();
        }

        public void EquipNotChecked(Equipable equipable)
        {
            _equiped = true;
            equipable.TakeOff();
        }
    }
}
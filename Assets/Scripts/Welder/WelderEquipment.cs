using RoomObjects;
using UnityEngine;

namespace Welder
{
    public class WelderEquipment : MonoBehaviour
    {
        public bool Equiped => _equiped;

        private bool _equiped;

        public bool TryEquip(Equipable equipable)
        {
            if (_equiped == false)
            {
                _equiped = true;
                equipable.TakeOff();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryEquipNotChecked(Equipable equipable)
        {
            if (_equiped == false)
            {
                _equiped = true;
                equipable.TakeOff();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
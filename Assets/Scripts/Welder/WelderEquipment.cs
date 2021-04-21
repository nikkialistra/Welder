using System;
using System.Text;
using RoomObjects;
using UnityEngine;

namespace Welder
{
    public class WelderEquipment : MonoBehaviour
    {
        [SerializeField] private bool _helmetPutOn;
        [SerializeField] private bool _suitPutOn;
        [SerializeField] private bool _glovesPutOn;
        [SerializeField] private bool _shoePutOn;

        public bool HasFullPack()
        {
            return _helmetPutOn && _suitPutOn && _glovesPutOn && _shoePutOn;
        }

        public bool TryEquip(EquipmentType equipment)
        {
            switch (equipment)
            {
                case EquipmentType.Helmet:
                    return TryEquipSpecificPart(ref _helmetPutOn);
                case EquipmentType.Suit:
                    return TryEquipSpecificPart(ref _suitPutOn);
                case EquipmentType.Gloves:
                    return TryEquipSpecificPart(ref _glovesPutOn);
                case EquipmentType.Shoe:
                    return TryEquipSpecificPart(ref _shoePutOn);
                default:
                    throw new ArgumentOutOfRangeException(nameof(equipment));
            }
        }

        public string GetLackingParts()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Я еще не надел: ");
            
            if (!_helmetPutOn)
                stringBuilder.Append("каску, ");
            if (!_suitPutOn)
                stringBuilder.Append("костюм, ");
            if (!_glovesPutOn)
                stringBuilder.Append("перчатки, ");
            if (!_shoePutOn)
                stringBuilder.Append("ботинки, ");

            stringBuilder.Remove(stringBuilder.Length - 2, 2);

            return stringBuilder.ToString();
        }

        private bool TryEquipSpecificPart(ref bool equipmentPart)
        {
            if (equipmentPart) 
                return false;
            
            equipmentPart = true;
            return true;
        }
    }
}
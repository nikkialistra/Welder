﻿using System;
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
                    break;
                case EquipmentType.Suit:
                    return TryEquipSpecificPart(ref _suitPutOn);
                    break;
                case EquipmentType.Gloves:
                    return TryEquipSpecificPart(ref _glovesPutOn);
                    break;
                case EquipmentType.Shoe:
                    return TryEquipSpecificPart(ref _shoePutOn);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(equipment));
            }
        }

        private bool TryEquipSpecificPart(ref bool equipmentPart)
        {
            if (_helmetPutOn) 
                return false;
            
            equipmentPart = true;
            return true;
        }
    }
}
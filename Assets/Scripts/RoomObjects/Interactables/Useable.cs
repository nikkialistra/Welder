﻿using UI;
using UnityEngine;

namespace RoomObjects.Interactables
{
    public class Useable : MonoBehaviour, IInteractable
    {
        public UseableTypes UseableType => _useableType;

        [SerializeField] private UseableTypes _useableType;
        
        [SerializeField] private ChoicesManager _choicesManager;
        [SerializeField] private UseableShower _useableShower;

        public void ShowChoices()
        {
            _choicesManager.ShowEmptyChoices();
            _useableShower.Show(this);
        }
    }
}
using System;
using UI;
using UnityEngine;

namespace RoomObjects.Interactables
{
    public class Equipable : MonoBehaviour, IInteractable
    {
        public EquipableTypes EquipableTypes => _equipableTypes;

        public bool IsChecked => _isChecked;

        [SerializeField] private bool _isChecked;

        [SerializeField] private ChoicesManager _choicesManager;
        
        [SerializeField] private EquipableShower _equipableShower;
        [SerializeField] private EquipableTypes _equipableTypes;

        public void ShowChoices()
        {
            _choicesManager.ShowEmptyChoices();
            _equipableShower.Show(this);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Check()
        {
            _isChecked = true;
        }
    }
}
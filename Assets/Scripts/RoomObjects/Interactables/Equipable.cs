using Services;
using UnityEngine;

namespace RoomObjects.Interactables
{
    public class Equipable : MonoBehaviour, IInteractable
    {
        public EquipableTypes EquipableTypes => _equipableTypes;

        public bool IsChecked { get; private set; }

        [SerializeField] private ChoicesManager _choicesManager;
        
        [SerializeField] private EquipableShower _equipableShower;
        [SerializeField] private EquipableTypes _equipableTypes;

        public void ShowChoices()
        {
            _choicesManager.ShowChoices();
            _equipableShower.Show(this);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Check()
        {
            IsChecked = true;
        }
    }
}
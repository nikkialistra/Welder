using Services;
using UnityEngine;

namespace RoomObjects.Interactables
{
    public class Equipable : MonoBehaviour, IInteractable
    {
        public EquipmentTypes EquipmentTypes => _equipmentTypes;

        public bool IsChecked { get; private set; }

        [SerializeField] private EquipableChoices _equipableChoices;
        
        [SerializeField] private EquipmentTypes _equipmentTypes;

        public void ShowChoices()
        {
            _equipableChoices.ShowChoices(this);
        }

        public void Interact()
        {
            Destroy(gameObject);
        }

        public void Check()
        {
            IsChecked = true;
        }
    }
}
using Services;
using UnityEngine;

namespace RoomObjects.Interactables
{
    public class Equipable : MonoBehaviour, IInteractable
    {
        public EquipmentTypes EquipmentTypes => _equipmentTypes;
        
        [SerializeField] private EquipmentTypes _equipmentTypes;

        public void ShowChoicesWith(ActionHandler actionHandler)
        {
            actionHandler.ShowChoices(this);
        }

        public void Interact()
        {
            Destroy(gameObject);
        }
    }
}
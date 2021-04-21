using UnityEngine;

namespace RoomObjects.Contracts
{
    public interface IInteractable
    {
        Vector3 InteractionPosition { get; }
        Vector3 InteractionLookAtPoint { get; }
        InteractableType GetInteractableType();
        
        GameObject GameObject { get; }
        bool Activated { get; }

        void SelectBlink();
        void Activate();
        void Deactivate();
    }
}
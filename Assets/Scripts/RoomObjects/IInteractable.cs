using UnityEngine;

namespace RoomObjects
{
    public interface IInteractable
    {
        Vector3 InteractionPosition { get; }
        Vector3 InteractionLookAtPoint { get; }
    }
}
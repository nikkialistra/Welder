using RoomObjects.Contracts;

namespace RoomObjects
{
    public class Weldable : RoomObject
    {
        public override InteractableType GetInteractableType() => InteractableType.Weld;
    }
}
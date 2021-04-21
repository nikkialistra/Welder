using RoomObjects.Contracts;

namespace RoomObjects
{
    public class Pipe : RoomObject
    {
        public override InteractableType GetInteractableType() => InteractableType.Weld;
    }
}
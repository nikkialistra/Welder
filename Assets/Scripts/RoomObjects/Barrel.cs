using RoomObjects.Contracts;

namespace RoomObjects
{
    public class Barrel : RoomObject
    {
        public override InteractableType GetInteractableType() => InteractableType.Raise;
    }
}
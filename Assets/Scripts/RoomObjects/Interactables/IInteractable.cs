using Services;

namespace RoomObjects.Interactables
{
    public interface IInteractable
    {
        void Interact();
        void ShowChoicesWith(ActionHandler actionHandler);
    }
}
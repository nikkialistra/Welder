using UnityEngine;

namespace RoomObjects
{
    public class Helmet : MonoBehaviour, IInteractable
    {
        public Vector3 InteractionPosition => _interactionPoint.position;
        public Vector3 InteractionLookAtPoint => _interactionLookAtPoint.position;

        public InteractableType InteractableType => InteractableType.Raise;

        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private Transform _interactionLookAtPoint;
    }
}
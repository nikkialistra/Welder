using UnityEngine;

namespace RoomObjects
{
    public class Helmet : MonoBehaviour, IInteractable
    {
        public Vector3 InteractionPosition => _interactionPoint.position;
        public Vector3 InteractionLookAtPoint => _interactionLookAtPoint.position;

        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private Transform _interactionLookAtPoint;
    }
}
using UnityEngine;

namespace RoomObjects
{
    [RequireComponent(typeof(MeshRenderer))]
    public class RoomObject : MonoBehaviour
    {
        public GameObject GameObject => gameObject;
        public InteractableType InteractableType => _type;
        
        [SerializeField] private InteractionPoint _interactionPoint;
        [SerializeField] private InteractableType _type;

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            _interactionPoint.RoomObject = this;
        }
    }
}
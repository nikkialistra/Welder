using UnityEngine;

namespace RoomObjects
{
    [RequireComponent(typeof(MeshFilter))]
    public class Interactable : MonoBehaviour
    {
        public InteractableType InteractableType => _type;
        public bool Active { get; private set; } = true;
        
        [SerializeField] private InteractionPoint _interactionPoint;
        [SerializeField] private InteractableType _type;

        private MeshRenderer _meshRenderer;

        [SerializeField] private Mesh _meshAfterEquipment;

        private MeshFilter _meshFilter;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }
        
        private void Start()
        {
            _interactionPoint.Interactable = this;
        }

        public void Interact()
        {
            Active = false;
            _meshFilter.mesh = _meshAfterEquipment;
        }
    }
}
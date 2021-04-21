using System.Collections;
using RoomObjects.Contracts;
using UnityEngine;

namespace RoomObjects
{
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class RoomObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private Transform _interactionLookAtPoint;
        
        [Space]
        [SerializeField] private Material _deactivateMaterial;
        [SerializeField] private Material _activateMaterial;
        [SerializeField] private Material _blinkMaterial;
        
        [Space]
        [SerializeField] private float _blinkTime = 0.3f;
        
        public Vector3 InteractionPosition => _interactionPoint.position;
        public Vector3 InteractionLookAtPoint => _interactionLookAtPoint.position;

        public abstract InteractableType GetInteractableType();

        public GameObject GameObject => gameObject;
        
        public bool Activated { get; private set; }

        private MeshRenderer _meshRenderer;

        private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();

        public void SelectBlink()
        {
            if (Activated)
                return;
            
            SetMaterial(_blinkMaterial);
            StartCoroutine(SetDeactivateMaterialAfter());
        }

        public void Activate()
        {
            SetMaterial(_activateMaterial);
            Activated = true;
        }

        public void Deactivate()
        {
            SetMaterial(_deactivateMaterial);
            Activated = false;
        }

        private IEnumerator SetDeactivateMaterialAfter()
        {
            yield return new WaitForSeconds(_blinkTime);
            if (Activated)
                yield break;
            
            SetMaterial(_deactivateMaterial);
        }

        private void SetMaterial(Material material)
        {
            var materials = _meshRenderer.materials;
            materials[0] = material;
            _meshRenderer.materials = materials;
        }
    }
}
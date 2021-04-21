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
        [SerializeField] private Color _deactivateColor = Color.white;
        [SerializeField] private Color _activateColor = Color.yellow;
        [SerializeField] private Color _blinkColor = Color.red;
        
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
            
            SetColor(_blinkColor);
            StartCoroutine(SetDeactivateMaterialAfter());
        }

        public void Activate()
        {
            SetColor(_activateColor);
            Activated = true;
        }

        public void Deactivate()
        {
            SetColor(_deactivateColor);
            Activated = false;
        }

        private IEnumerator SetDeactivateMaterialAfter()
        {
            yield return new WaitForSeconds(_blinkTime);
            if (Activated)
                yield break;
            
            SetColor(_deactivateColor);
        }

        private void SetColor(Color color)
        {
            var materials = _meshRenderer.materials;
            materials[0].color = color;
            _meshRenderer.materials = materials;
        }
    }
}
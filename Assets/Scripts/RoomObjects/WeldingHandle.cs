using UnityEngine;

namespace RoomObjects
{
    [RequireComponent(typeof(LineRenderer))]
    public class WeldingHandle : MonoBehaviour
    {
        [SerializeField] private Transform _lineStartPosition;
        [SerializeField] private Transform _lineEndPosition;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _lineRenderer.SetPosition(0, _lineStartPosition.position);
        }

        private void Update()
        {
            _lineRenderer.SetPosition(1, _lineEndPosition.position);
        }
    }
}
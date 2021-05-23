using UnityEngine;

namespace RoomObjects
{
    [RequireComponent(typeof(LineRenderer))]
    public class WeldingHandle : MonoBehaviour
    {
        [SerializeField] private Transform _lineStartPosition;
        [SerializeField] private Transform _lineEndPosition;
        
        [SerializeField] private ParticleSystem _particleSystem;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _lineRenderer.SetPosition(0, _lineStartPosition.position);
            
            StopWeldingProcess();
        }

        private void Update()
        {
            _lineRenderer.SetPosition(1, _lineEndPosition.position);
        }

        public void ShowWeldingProcess()
        {
            _particleSystem.Play();
            _particleSystem.gameObject.SetActive(true);
        }

        public void StopWeldingProcess()
        {
            _particleSystem.Pause();
            _particleSystem.gameObject.SetActive(false);
        }
    }
}
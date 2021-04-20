using Camera;
using UnityEngine;
using UnityEngine.AI;

namespace Welder
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class WelderMover : MonoBehaviour
    {
        [SerializeField] private Raycaster _raycaster;
    
        private NavMeshAgent _navMeshAgent;

        private void Awake() => _navMeshAgent = GetComponent<NavMeshAgent>();

        private void OnEnable() => _raycaster.RaycastHit += OnRaycastHit;

        private void OnDisable() => _raycaster.RaycastHit -= OnRaycastHit;

        private void OnRaycastHit(Vector3 point) => _navMeshAgent.SetDestination(point);
    }
}

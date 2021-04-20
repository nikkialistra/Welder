using System.Collections;
using Camera;
using UnityEngine;
using UnityEngine.AI;

namespace Welder
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class WelderMover : MonoBehaviour
    {
        [SerializeField] private Raycaster _raycaster;

        [SerializeField] private float _lookAtSpeed;

        private NavMeshAgent _navMeshAgent;

        private void Awake() => _navMeshAgent = GetComponent<NavMeshAgent>();

        private void OnEnable()
        {
            _raycaster.MoveToPoint += OnMoveToPoint;
            _raycaster.MoveToInteractable += OnMoveToInteractable;
        }

        private void OnDisable()
        {
            _raycaster.MoveToPoint -= OnMoveToPoint;
            _raycaster.MoveToInteractable -= OnMoveToInteractable;
        }

        private void OnMoveToPoint(Vector3 point) => _navMeshAgent.SetDestination(point);

        private void OnMoveToInteractable(Vector3 position, Vector3 lookAtPoint)
        {
            _navMeshAgent.SetDestination(position);

            StartCoroutine(LookAtPointAfter(lookAtPoint));
        }

        private IEnumerator LookAtPointAfter(Vector3 point)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.3f);
                if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance) 
                    continue;
                
                StartCoroutine(LookAtPoint(point));
                break;
            }
        }

        private IEnumerator LookAtPoint(Vector3 point)
        {
            var rotationDirection = point - transform.position;
            rotationDirection.y = 0;
            var rotation = Quaternion.LookRotation(rotationDirection);

            while (transform.rotation != rotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _lookAtSpeed * Time.fixedDeltaTime);

                yield return null;
            }
        }
    }
}

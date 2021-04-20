using System;
using System.Collections;
using Camera;
using RoomObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Welder
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class WelderMover : MonoBehaviour
    {
        public event Action<IInteractable> InteractableGot; 
        public event Action InteractableReset; 
        
        [SerializeField] private MouseHandler _mouseHandler;
        
        [Space]
        [SerializeField] private float _lookAtSpeed;

        private Coroutine _lookAtInteractable;

        private NavMeshAgent _navMeshAgent;

        private void Awake() => _navMeshAgent = GetComponent<NavMeshAgent>();

        private void OnEnable()
        {
            _mouseHandler.MoveToPoint += OnMoveToPoint;
            _mouseHandler.MoveToInteractable += OnMoveToInteractable;
        }

        private void OnDisable()
        {
            _mouseHandler.MoveToPoint -= OnMoveToPoint;
            _mouseHandler.MoveToInteractable -= OnMoveToInteractable;
        }

        private void OnMoveToPoint(Vector3 point)
        {
            _navMeshAgent.SetDestination(point);

            ResetInteraction();
        }

        private void ResetInteraction()
        {
            if (_lookAtInteractable != null)
                StopCoroutine(_lookAtInteractable);
            InteractableReset?.Invoke();
        }

        private void OnMoveToInteractable(IInteractable interactable)
        {
            _navMeshAgent.SetDestination(interactable.InteractionPosition);

            _lookAtInteractable = StartCoroutine(LookAtInteractableAfter(interactable));
        }

        private IEnumerator LookAtInteractableAfter(IInteractable interactable)
        {
            foreach (var _ in WaitForStoppingDistance()) yield return _;

            foreach (var _ in LookAt(interactable)) yield return _;

            InteractableGot?.Invoke(interactable);
        }

        private IEnumerable LookAt(IInteractable interactable)
        {
            var rotationDirection = interactable.InteractionLookAtPoint - transform.position;
            rotationDirection.y = 0;
            var rotation = Quaternion.LookRotation(rotationDirection);

            while (!QuaternionAreSame(transform.rotation, rotation))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _lookAtSpeed * Time.fixedDeltaTime);

                yield return null;
            }
        }

        private bool QuaternionAreSame(Quaternion first, Quaternion second) => Mathf.Abs(Quaternion.Dot(first,second)) > 0.99f;

        private IEnumerable WaitForStoppingDistance()
        {
            while (true)
            {
                yield return null;
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                    break;
            }
        }
    }
}

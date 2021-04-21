using System;
using System.Collections;
using RoomObjects.Contracts;
using Services;
using UnityEngine;
using UnityEngine.AI;

namespace Welder
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(WelderInteractor))]
    public class WelderMover : MonoBehaviour
    {
        public event Action<IInteractable> InteractableGot; 
        public event Action InteractableReset; 
        
        public event Action Put; 
        
        [SerializeField] private MouseHandler _mouseHandler;

        [Space]
        [SerializeField] private float _lookAtSpeed;


        private Coroutine _lookAtInteractable;
        private Coroutine _put;

        private NavMeshAgent _navMeshAgent;
        private WelderInteractor _welderInteractor;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _welderInteractor = GetComponent<WelderInteractor>();
        }

        private void OnEnable()
        {
            _mouseHandler.MoveToPoint += OnMoveToPoint;
            _mouseHandler.PutOnPoint += OnPutOnPoint;
            _mouseHandler.MoveToInteractable += OnMoveToInteractable;
        }

        private void OnDisable()
        {
            _mouseHandler.MoveToPoint -= OnMoveToPoint;
            _mouseHandler.PutOnPoint -= OnPutOnPoint;
            _mouseHandler.MoveToInteractable -= OnMoveToInteractable;
        }

        private void OnMoveToPoint(Vector3 point)
        {
            ResetInteraction();
            
            _navMeshAgent.SetDestination(point);
        }

        private void OnPutOnPoint(Vector3 point)
        {
            if (!_welderInteractor.CanPut)
                return;
            
            _navMeshAgent.SetDestination(point);

            if (_put != null)
                StopCoroutine(_put);
            _put = StartCoroutine(PutAfter());
        }

        private IEnumerator PutAfter()
        {
            foreach (var _ in WaitForStoppingDistance()) yield return _;

            Put?.Invoke();
        }

        private void ResetInteraction()
        {
            if (_lookAtInteractable != null)
                StopCoroutine(_lookAtInteractable);
            InteractableReset?.Invoke();
        }

        private void OnMoveToInteractable(IInteractable interactable)
        {
            ResetInteraction();
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

            while (QuaternionAreNotSame(transform.rotation, rotation))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _lookAtSpeed * Time.fixedDeltaTime);

                yield return null;
            }
        }

        private bool QuaternionAreNotSame(Quaternion first, Quaternion second) => Mathf.Abs(Quaternion.Dot(first,second)) < 0.99f;

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

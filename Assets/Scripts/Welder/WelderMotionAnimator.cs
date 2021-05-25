using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Welder
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Movement))]
    public class WelderMotionAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _tableWorkingPoint;
        [SerializeField] private Transform _tableLookingPoint;
        
        private NavMeshAgent _navMeshAgent;
        private CharacterController _characterController;
        private Movement _movement;
        
        private Coroutine _moveToTableCoroutine;
        private Coroutine _lookOnTableCoroutine;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _characterController = GetComponent<CharacterController>();
            _movement = GetComponent<Movement>();
        }

        public void StartWorking()
        {
            if (_moveToTableCoroutine != null)
            {
                StopCoroutine(_moveToTableCoroutine);
            }

            if (_lookOnTableCoroutine != null)
            {
                StopCoroutine(_lookOnTableCoroutine);
            }
            
            _moveToTableCoroutine = StartCoroutine(MoveToTableWorkingPoint());
        }

        private IEnumerator MoveToTableWorkingPoint()
        {
            DisableControl();

            _navMeshAgent.SetDestination(_tableWorkingPoint.position);
            _navMeshAgent.isStopped = false;
            
            // Give time to accelerate
            yield return new WaitForSeconds(0.3f);

            while (_navMeshAgent.velocity.magnitude > 0.01f)
            {
                yield return null;
            }

            _navMeshAgent.isStopped = true;
            
            EnableControl();

            _lookOnTableCoroutine = StartCoroutine(LookAt(_tableLookingPoint.position));
        }
        
        private IEnumerator LookAt(Vector3 point)
        {
            var rotationDirection = point - transform.position;
            rotationDirection.y = 0;
            var rotation = Quaternion.LookRotation(rotationDirection);

            while (QuaternionAreNotSame(transform.rotation, rotation))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _navMeshAgent.angularSpeed * Time.fixedDeltaTime);

                yield return null;
            }
        }

        private bool QuaternionAreNotSame(Quaternion first, Quaternion second) => Mathf.Abs(Quaternion.Dot(first,second)) < 0.99f;

        private void DisableControl()
        {
            _characterController.enabled = false;
            _movement.CanMove = false;
        }

        private void EnableControl()
        {
            _characterController.enabled = true;
            _movement.CanMove = true;
        }
    }
}
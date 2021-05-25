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
        [SerializeField] private Transform _reactionLookingPoint;

        [Space]
        [SerializeField] private float _timeToFallOnFloor;
        [SerializeField] private float _fallOnFloorAngle;
        [SerializeField] private float _fallOnFloorHeight;
        
        [Space]
        [SerializeField] private float _timeToFallOnBoxes;
        [SerializeField] private float _fallOnBoxesAngle;

        private NavMeshAgent _navMeshAgent;
        private CharacterController _characterController;
        private Movement _movement;

        private bool _boxesNotRemoved;
        private bool _floorNotWiped;

        private bool _motionActivated;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _characterController = GetComponent<CharacterController>();
            _movement = GetComponent<Movement>();
        }

        public void StartWorking(bool boxesNotRemoved, bool floorNotWiped)
        {
            if (_motionActivated)
            {
                return;
            }
            
            _motionActivated = true;
            
            _boxesNotRemoved = boxesNotRemoved;
            _floorNotWiped = floorNotWiped;

            StartCoroutine(MoveToTableWorkingPoint());
        }

        private IEnumerator MoveToTableWorkingPoint()
        {
            DisableControl();

            _navMeshAgent.SetDestination(_tableWorkingPoint.position);

            // Give time to accelerate
            yield return new WaitForSeconds(0.3f);

            while (_navMeshAgent.velocity.magnitude > 0.01f)
            {
                yield return null;
            }
            
            _navMeshAgent.enabled = false;
            
            yield return StartCoroutine(LookAt(_tableLookingPoint.position));
            
            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(LookAt(_reactionLookingPoint.position));

            StartCoroutine(TryFallOnItems());
        }

        private void DisableControl()
        {
            _characterController.enabled = false;
            _movement.CanMove = false;
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

        private IEnumerator TryFallOnItems()
        {
            if (_floorNotWiped)
            {
                yield return StartCoroutine(SlipOnFloor());
                yield return StartCoroutine(GetUp());
            }

            if (_boxesNotRemoved)
            {
                yield return StartCoroutine(FallOnBoxes());
            }
        }

        private IEnumerator SlipOnFloor()
        {
            var time = 0f;
            var beginPosition = transform.position;

            while (time <= _timeToFallOnFloor)
            {
                var fraction = time / _timeToFallOnFloor;

                ChangeSlipRotation(fraction);
                ChangeSlipPosition(beginPosition, fraction);

                time += Time.deltaTime;
                Debug.Log(transform.rotation.eulerAngles);

                yield return null;
            }
            
            Debug.Log(transform.rotation.eulerAngles);
        }

        private void ChangeSlipPosition(Vector3 beginPosition, float fraction)
        {
            var position = beginPosition;
            position.y -= _fallOnFloorHeight * fraction;
            
            transform.position = position;
        }

        private void ChangeSlipRotation(float fraction)
        {
            var rotation = transform.rotation.eulerAngles;
            rotation.z = _fallOnFloorAngle * fraction;
            
            transform.eulerAngles = rotation;
        }

        private IEnumerator GetUp()
        {
            yield return null;
        }

        private IEnumerator FallOnBoxes()
        {
            yield return null;
        }
    }
}
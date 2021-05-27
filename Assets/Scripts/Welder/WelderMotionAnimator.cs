using System.Collections;
using Effects;
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
        [SerializeField] private float _fallOnBoxesHeight;
        [SerializeField] private float _fallOnBoxesDistance;
        
        [Space] 
        [SerializeField] private PreparationsEffect _preparationsEffect;
        
        [Space] 
        [SerializeField] private AudioSource _fallDown;
        [SerializeField] private AudioSource _slip;

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
            var success = true;
            
            if (_floorNotWiped)
            {
                success = false;
                
                yield return StartCoroutine(SlipOnFloor());
                yield return StartCoroutine(GetUp());
            }

            if (_boxesNotRemoved)
            {
                success = false;
                
                yield return StartCoroutine(FallOnBoxes());
            }

            if (success)
            {
                _preparationsEffect.ShowSuccess();
            }
        }

        private IEnumerator SlipOnFloor()
        {
            _slip.Play();
            _preparationsEffect.ShowSlipMessage();
            
            var time = 0f;
            var beginPosition = transform.position;

            while (time <= _timeToFallOnFloor)
            {
                var fraction = time / _timeToFallOnFloor;

                ChangeSlipRotation(fraction);
                ChangeSlipPosition(beginPosition, fraction);

                time += Time.deltaTime;

                yield return null;
            }
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
            yield return new WaitForSeconds(1f);
            
            var time = 0f;
            var _currentPosition = transform.position;

            while (time <= _timeToFallOnFloor)
            {
                var fraction = time / _timeToFallOnFloor;

                ChangeGetUpRotation(fraction);
                ChangeGetUpPosition(_currentPosition, fraction);

                time += Time.deltaTime;

                yield return null;
            }
            
            yield return new WaitForSeconds(0.5f);
        }

        private void ChangeGetUpPosition(Vector3 currentPosition, float fraction)
        {
            var position = currentPosition;
            position.y += _fallOnFloorHeight * fraction;
            
            transform.position = position;
        }

        private void ChangeGetUpRotation(float fraction)
        {
            var rotation = transform.rotation.eulerAngles;
            rotation.z = _fallOnFloorAngle * (1 - fraction);
            
            transform.eulerAngles = rotation;
        }

        private IEnumerator FallOnBoxes()
        {
            _fallDown.Play();
            _preparationsEffect.ShowFallMessage();
            
            var time = 0f;
            var beginPosition = transform.position;

            while (time <= _timeToFallOnBoxes)
            {
                var fraction = time / _timeToFallOnBoxes;
                
                ChangeFallPosition(beginPosition, fraction);

                time += Time.deltaTime;

                yield return null;
            }
        }

        private void ChangeFallPosition(Vector3 beginPosition, float fraction)
        {
            var position = beginPosition;
            position.y -= _fallOnBoxesHeight * fraction;
            position.z += _fallOnBoxesDistance * fraction;
            
            transform.position = position;
        }
    }
}
using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public bool CanMove { get; set; } = true;
        
        [SerializeField] private float _walkSpeed = 12f;
        [SerializeField] private float _smoothTime = 0.03f;
        
        [SerializeField] private float _gravity = 15;

        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.3f;
        [SerializeField] private LayerMask _groundMask;

        private Vector3 _velocity;
        private bool _isGrounded;

        private Vector2 _currentDirection = Vector2.zero;
        Vector2 _currentDirectionVelocity = Vector2.zero;

        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!CanMove)
            {
                return;
            }
            
            UpdateFalling();
            UpdateMoving();
        }

        private void UpdateMoving()
        {
            var targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            _currentDirection = Vector2.SmoothDamp(_currentDirection, targetDirection, ref _currentDirectionVelocity, _smoothTime);

            var move = (transform.right * _currentDirection.x + transform.forward * _currentDirection.y) * _walkSpeed;

            _controller.Move(move * (_walkSpeed * Time.deltaTime));
        }

        private void UpdateFalling()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            if (_isGrounded)
            {
                _velocity.y = -2f;
            }
            else
            {
                _velocity.y += _gravity * Time.deltaTime;
            }

            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}
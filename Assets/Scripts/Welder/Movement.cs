using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _speed = 12f;
        [SerializeField] private float _gravity = 15;

        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.3f;
        [SerializeField] private LayerMask _groundMask;

        private Vector3 _velocity;
        private bool _isGrounded;

        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            UpdateFalling();
            UpdateMoving();
        }

        private void UpdateMoving()
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            var move = transform.right * x + transform.forward * z;

            _controller.Move(move * (_speed * Time.deltaTime));
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
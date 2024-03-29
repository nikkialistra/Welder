﻿using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public bool CanMove { get; set; } = true;
        
        [SerializeField] private float _walkSpeed = 12f;
        [SerializeField] private float _smoothTime = 0.03f;

        [Space] 
        [SerializeField] private AudioSource _footsteps;
        
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
                StopFootsteps();
                return;
            }
            
            UpdateMoving();
        }

        private void UpdateMoving()
        {
            var targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            _currentDirection = Vector2.SmoothDamp(_currentDirection, targetDirection, ref _currentDirectionVelocity, _smoothTime);

            var move = (transform.right * _currentDirection.x + transform.forward * _currentDirection.y) * _walkSpeed;

            PlayFootstepsIfNeeded(move);

            _controller.Move(move * (_walkSpeed * Time.deltaTime));
        }

        private void PlayFootstepsIfNeeded(Vector3 move)
        {
            if (move != Vector3.zero)
            {
                if (!_footsteps.isPlaying)
                {
                    _footsteps.Play();
                }
            }
            else
            {
                StopFootsteps();
            }
        }

        private void StopFootsteps()
        {
            _footsteps.Stop();
        }
    }
}
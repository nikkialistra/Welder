using System;
using System.Collections;
using RoomObjects;
using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Movement))]
    public class WelderAnimator : MonoBehaviour
    {
        public Action Welding;
        
        public bool IsWelding { get; private set; } = false;

        [SerializeField] private MouseLook _mouseLook;

        [SerializeField] private WeldingHandle _weldingHandle;

        private readonly int _sitDown = Animator.StringToHash("SitDown");
        private readonly int _getUp = Animator.StringToHash("GetUp");

        private Animator _animator;
        
        private Movement _movement;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _movement = GetComponent<Movement>();
        }

        public void SitDown()
        {
            _movement.CanMove = false;
            _mouseLook.CanRotate = false;
            _animator.SetTrigger(_sitDown);

            IsWelding = true;

            StartCoroutine(ShowWeldingProcessAfter());
        }

        private IEnumerator ShowWeldingProcessAfter()
        {
            yield return new WaitForSeconds(0.5f);

            _weldingHandle.ShowWeldingProcess();
            
            yield return new WaitForSeconds(1f);

            Welding?.Invoke();

            StartCoroutine(GetUpAfter());
        }

        private IEnumerator GetUpAfter()
        {
            yield return new WaitForSeconds(4f);

            _weldingHandle.StopWeldingProcess();
            
            GetUp();
        }

        private void GetUp()
        {
            _animator.SetTrigger(_getUp);

            StartCoroutine(AllowMoveAfter());
        }

        private IEnumerator AllowMoveAfter()
        {
            while (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
            {
                yield return null;
            }

            _movement.CanMove = true;
            _mouseLook.CanRotate = true;
            
            IsWelding = false;
        }
    }
}
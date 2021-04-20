using UnityEngine;
using UnityEngine.AI;

namespace Welder
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class WelderAnimator : MonoBehaviour
    {
        private readonly int _velocity = Animator.StringToHash("velocity");
        private readonly int _raise = Animator.StringToHash("raise");
        
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update() => _animator.SetFloat(_velocity, _navMeshAgent.velocity.magnitude);

        public void Raise() => _animator.SetTrigger(_raise);
    }
}
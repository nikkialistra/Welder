using UnityEngine;
using UnityEngine.AI;

namespace Welder
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class WelderAnimator : MonoBehaviour
    {
        private readonly int _velocity = Animator.StringToHash("velocity");
        private readonly int _raisePut = Animator.StringToHash("raisePut");
        private readonly int _welding = Animator.StringToHash("welding");
        
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update() => _animator.SetFloat(_velocity, _navMeshAgent.velocity.magnitude);

        public void Raise() => _animator.SetTrigger(_raisePut);
        public void Put() => _animator.SetTrigger(_raisePut);

        public void StartWeld() => _animator.SetBool(_welding, true);
        
        public void StopWeld() => _animator.SetBool(_welding, false);
    }
}
using System.Collections;
using UnityEngine;

namespace Welder
{
    public class WelderMotionAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _welder;

        [SerializeField] private Transform _tableWorkingPoint;

        [SerializeField] private float _movingSpeed;
        

        public void StartWorking()
        {
            StartCoroutine(MoveToTableWorkingPoint());
        }

        private IEnumerator MoveToTableWorkingPoint()
        {
            while (_welder.position != _tableWorkingPoint.position)
            {
                _welder.position = Vector3.MoveTowards(_welder.position, _tableWorkingPoint.position,
                    _movingSpeed * Time.deltaTime);
                
                yield return null;
            }
        }
    }
}
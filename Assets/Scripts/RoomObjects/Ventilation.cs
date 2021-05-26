using System.Collections;
using UnityEngine;

namespace RoomObjects
{
    public class Ventilation : MonoBehaviour
    {
        [SerializeField] private Transform _tube;

        [SerializeField] private float _oscillationAmplitude;
        

        public void Enable()
        {
            StartCoroutine(OscillateTube());
        }

        private IEnumerator OscillateTube()
        {
            var moveToRight = true;

            while (true)
            {
                var movement = moveToRight ? _oscillationAmplitude : -_oscillationAmplitude;

                _tube.position += new Vector3(movement, 0, 0);

                moveToRight = !moveToRight;
                
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
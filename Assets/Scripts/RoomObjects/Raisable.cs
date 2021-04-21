using System.Collections;
using RoomObjects.Contracts;
using UnityEngine;

namespace RoomObjects
{
    public class Raisable : RoomObject
    {
        public override InteractableType GetInteractableType() => InteractableType.Raise;

        public void Raise(Transform parent, float height, float time)
        {

            transform.parent = parent;
            StartCoroutine(ChangeHeightOverTime(height, time));
        }
        
        public void Put(float height, float time)
        {
            transform.parent = null;
            StartCoroutine(ChangeHeightOverTime(height, time));
        }

        private IEnumerator ChangeHeightOverTime(float height, float time)
        {
            var speed = Vector3.up * (height / time);

            while (time > 0)
            {
                transform.position += (speed * Time.fixedDeltaTime);
                time -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
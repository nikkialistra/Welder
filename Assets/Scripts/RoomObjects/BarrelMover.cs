using UnityEngine;

namespace RoomObjects
{
    public class BarrelMover : MonoBehaviour
    {
        [SerializeField] private float _distance;

        public void MoveAway()
        {
            transform.position += new Vector3(_distance, 0, 0);
        }
    }
}
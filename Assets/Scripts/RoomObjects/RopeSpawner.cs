using System;
using UnityEngine;

namespace RoomObjects
{
    public class RopeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _ropePartPrefab;

        [SerializeField] [Range(1, 1000)] private int _length = 1;
        [SerializeField] private Transform _lastPosition;

        [SerializeField] private float _partDistance = 0.21f;
        
        [SerializeField] private bool _spawn;
        [SerializeField] private bool _snapFirst;
        [SerializeField] private bool _snapLast;

        private void Start()
        {
            if (_spawn)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            var count = (int) (_length / _partDistance);

            for (var x = 0; x < count; x++)
            {
                var ropePart = Instantiate(_ropePartPrefab,
                    new Vector3(transform.position.x, transform.position.y + _partDistance * (x + 1),
                        transform.position.z), Quaternion.identity, transform);
                
                ropePart.transform.eulerAngles = new Vector3(180, 0, 0);

                ropePart.name = transform.childCount.ToString();

                if (x == 0)
                {
                    Destroy(ropePart.GetComponent<CharacterJoint>());
                    if (_snapFirst)
                    {
                        ropePart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    }
                }
                else
                {
                    ropePart.GetComponent<CharacterJoint>().connectedBody =
                        transform.GetChild(transform.childCount - 2).GetComponent<Rigidbody>();
                }

                if (_snapLast)
                {
                    transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody>().constraints =
                        RigidbodyConstraints.FreezeAll;
                }
            }
        }
    }
}
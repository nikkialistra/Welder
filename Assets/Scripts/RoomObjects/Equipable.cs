using UnityEngine;

namespace RoomObjects
{
    [RequireComponent(typeof(MeshFilter))]
    public class Equipable : MonoBehaviour
    {
        [SerializeField] private Mesh _meshAfterEquipment;

        private MeshFilter _meshFilter;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void TakeOff()
        {
            _meshFilter.mesh = _meshAfterEquipment;
        }
    }
}
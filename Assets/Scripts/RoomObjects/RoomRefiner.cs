using UnityEngine;

namespace RoomObjects
{
    public class RoomRefiner : MonoBehaviour
    {
        [SerializeField] private GameObject _boxesAroundTable;
        [SerializeField] private GameObject _putBoxes;

        [SerializeField] private GameObject _puddle;
        [SerializeField] private GameObject _items;

        private bool _boxesRemoved;
        private bool _floorWiped;
        
        public void RemoveBoxes()
        {
            _boxesAroundTable.SetActive(false);
            _putBoxes.SetActive(true);
        }

        public void WipeFloor()
        {
            _puddle.SetActive(false);
        }

        public void TakeOffItems()
        {
            _items.SetActive(false);
        }
    }
}
using System.Collections.Generic;
using RoomObjects;
using UnityEngine;

namespace Services
{
    public class InteractionPointRepository : MonoBehaviour
    {
        public IEnumerable<InteractionPoint> InteractionPoints => _interactionPoints;
        
        private IEnumerable<InteractionPoint> _interactionPoints;

        private void Start()
        {
            _interactionPoints = FindObjectsOfType<InteractionPoint>();
        }
    }
}
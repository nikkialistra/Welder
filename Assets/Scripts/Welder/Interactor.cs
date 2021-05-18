using RoomObjects;
using Services;
using UnityEngine;

namespace Welder
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private ActionHandler _actionHandler;

        [SerializeField] private float _interactionDistance;
        [SerializeField] private float _checkTimeInterval;
        
        private float _timePassed;
        
        private void FixedUpdate()
        {
            if (!CheckIfTimeIntervalPassed())
            {
                return;
            }
            
            foreach (var interactionPoint in FindObjectsOfType<InteractionPoint>())
            {
                if (Vector3.Distance(transform.position, interactionPoint.transform.position) <= _interactionDistance)
                {
                    _actionHandler.ShowChoices(interactionPoint.RoomObject);
                }
            }
        }

        private bool CheckIfTimeIntervalPassed()
        {
            _timePassed += Time.fixedDeltaTime;

            if (_timePassed < _checkTimeInterval)
            {
                return false;
            }

            _timePassed = 0f;
            return true;
        }
    }
}
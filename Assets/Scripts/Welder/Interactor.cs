using RoomObjects.Interactables;
using UI;
using UnityEngine;

namespace Welder
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float _interactionDistance;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _checkTimeInterval;

        [Space]
        [SerializeField] private ChoicesManager _choicesManager;

        private float _timePassed;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            if (!CheckIfTimeIntervalPassed())
            {
                return;
            }

            SearchForCloseInteractionPoint();
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

        private void SearchForCloseInteractionPoint()
        {
            var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

            if (Physics.Raycast(ray, out var hit, _interactionDistance, _layerMask))
            {
                if (hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    interactable.ShowChoices();
                    return;
                }
            }

            _choicesManager.HideChoices();
        }
    }
}
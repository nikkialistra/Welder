using System.Collections;
using RoomObjects.Interactables;
using UnityEngine;

namespace Welder
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float _interactionDistance;
        [SerializeField] private float _checkTimeInterval;

        [SerializeField] private RectTransform _equipableChoices;

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

            if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance))
            {
                if (hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    interactable.ShowChoices();
                    return;
                }
            }

            DeactivateAllChoices();
        }

        private void DeactivateAllChoices()
        {
            _equipableChoices.gameObject.SetActive(false);
        }
    }
}
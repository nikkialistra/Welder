using RoomObjects.Interactables;
using UnityEngine;

namespace Services
{
    public class Choices : MonoBehaviour
    {
        [SerializeField] private RectTransform _choices;
        
        private ActionOutcome _actionOutcome;

        public void ShowChoices(IChoicesShower choicesShower, IInteractable interactable)
        {
            _choices.gameObject.SetActive(true);

            choicesShower.Show(interactable);
        }
        
        public void HideChoices()
        {
            _choices.gameObject.SetActive(false);
        }
    }
}
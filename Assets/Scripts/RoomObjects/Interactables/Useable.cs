using Services;
using UnityEngine;

namespace RoomObjects.Interactables
{
    public class Useable : MonoBehaviour, IInteractable
    {
        public UseableTypes UseableType => _useableType;

        [SerializeField] private UseableTypes _useableType;
        
        [SerializeField] private Choices _choices;
        [SerializeField] private UseableChoices _useableChoices;

        public void ShowChoices()
        {
            _choices.ShowChoices(_useableChoices, this);
        }

        public void Interact()
        {
            Destroy(gameObject);
        }
    }
}
using UI;
using UnityEngine;

namespace RoomObjects.Interactables
{
    public class Preparable : MonoBehaviour, IInteractable
    {
        public PreparableTypes PreparableType => _preparableType;

        [SerializeField] private PreparableTypes _preparableType;
        
        [SerializeField] private ChoicesManager _choicesManager;
        [SerializeField] private PreparableShower _preparableShower;

        public void ShowChoices()
        {
            _choicesManager.ShowEmptyChoices();
            _preparableShower.Show(this);
        }
    }
}
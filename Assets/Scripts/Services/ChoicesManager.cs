using RoomObjects.Interactables;
using UnityEngine;

namespace Services
{
    public class ChoicesManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _choices;
        
        public void ShowChoices()
        {
            _choices.gameObject.SetActive(true);
        }
        
        public void HideChoices()
        {
            _choices.gameObject.SetActive(false);
        }
    }
}
using UnityEngine;

namespace UI
{
    public class ChoicesManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _choices;

        [SerializeField] private RectTransform _equipableChoices;
        [SerializeField] private RectTransform _useableChoices;

        public void ShowEmptyChoices()
        {
            _equipableChoices.gameObject.SetActive(false);
            _useableChoices.gameObject.SetActive(false);
            
            _choices.gameObject.SetActive(true);
        }
        
        public void HideChoices()
        {
            _choices.gameObject.SetActive(false);
        }
    }
}
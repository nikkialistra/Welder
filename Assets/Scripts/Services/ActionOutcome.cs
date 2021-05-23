using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class ActionOutcome : MonoBehaviour
    {
        [SerializeField] private Image _danger;
        [SerializeField] private Image _correct;

        [SerializeField] private TextMeshProUGUI _text;

        private Coroutine _correctCoroutine;
        private Coroutine _dangerCoroutine;

        public void ShowCorrect(string message = "")
        {
            StopCoroutines();

            DisableAll();
            _correctCoroutine = StartCoroutine(ShowAndDisableAfter(_correct, message));
        }

        public void ShowDanger(string message = "")
        {
            StopCoroutines();

            DisableAll();
            _dangerCoroutine = StartCoroutine(ShowAndDisableAfter(_danger, message));
        }

        private void StopCoroutines()
        {
            if (_correctCoroutine != null)
                StopCoroutine(_correctCoroutine);
            
            if (_dangerCoroutine != null)
                StopCoroutine(_dangerCoroutine);
        }

        private void DisableAll()
        {
            _danger.enabled = false;
            _correct.enabled = false;
        }

        private IEnumerator ShowAndDisableAfter(Image image, string message)
        {
            yield return new WaitForSeconds(0.1f);
            
            image.enabled = true;
            if (message != "")
            {
                _text.text = message;
                _text.enabled = true;
            }
            
            yield return new WaitForSeconds(1.5f);

            image.enabled = false;
            _text.enabled = false;
        }
    }
}
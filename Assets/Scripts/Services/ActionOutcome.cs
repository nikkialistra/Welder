using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class ActionOutcome : MonoBehaviour
    {
        [SerializeField] private Image _danger;
        [SerializeField] private Image _correct;
        
        private Coroutine _correctCoroutine;
        private Coroutine _dangerCoroutine;

        public void ShowCorrect()
        {
            StopCoroutines();

            DisableAll();
            _correctCoroutine = StartCoroutine(ShowAndDisableAfter(_correct));
        }

        public void ShowDanger()
        {
            StopCoroutines();

            DisableAll();
            _dangerCoroutine = StartCoroutine(ShowAndDisableAfter(_danger));
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

        private static IEnumerator ShowAndDisableAfter(Image image)
        {
            yield return new WaitForSeconds(0.1f);
            
            image.enabled = true;
            
            yield return new WaitForSeconds(1.5f);

            image.enabled = false;
        }
    }
}
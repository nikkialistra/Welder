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

            _correctCoroutine = StartCoroutine(ShowAndDisableAfter(_correct));
        }

        public void ShowDanger()
        {
            StopCoroutines();

            _dangerCoroutine = StartCoroutine(ShowAndDisableAfter(_danger));
        }

        private void StopCoroutines()
        {
            if (_correctCoroutine != null)
                StopCoroutine(_correctCoroutine);
            
            if (_dangerCoroutine != null)
                StopCoroutine(_dangerCoroutine);
        }

        private IEnumerator ShowAndDisableAfter(Image image)
        {
            image.enabled = false;
            
            yield return new WaitForSeconds(0.1f);
            
            image.enabled = true;
            
            yield return new WaitForSeconds(1.5f);

            image.enabled = false;
        }
    }
}
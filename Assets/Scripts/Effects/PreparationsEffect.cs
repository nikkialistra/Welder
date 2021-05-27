using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Effects
{
    public class PreparationsEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _slipMessage;
        [SerializeField] private TextMeshProUGUI _fallMessage;

        [Space]
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _continueText;

        private bool _shouldRestart;
        private bool _shouldContinue;

        private void Update()
        {
            LoadSceneIfNeeded();
        }

        private void LoadSceneIfNeeded()
        {
            if (_shouldRestart && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SecondScene");
            }

            if (_shouldContinue && Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("ThirdScene");
            }
        }

        public void ShowSlipMessage()
        {
            _slipMessage.enabled = true;
            
            _shouldRestart = true;
            _restartText.enabled = true;
        }

        public void ShowFallMessage()
        {
            _fallMessage.enabled = true;
            
            _shouldRestart = true;
            _restartText.enabled = true;
        }

        public void ShowSuccess()
        {
            _shouldContinue = true;
            _continueText.enabled = true;
        }
    }
}
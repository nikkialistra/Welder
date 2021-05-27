using System.Collections;
using RoomObjects;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Effects
{
    public class CheckingEffect : MonoBehaviour
    {
        public bool VentilationEnabled => _ventilationEnabled;
        public bool FireExtinguisherChecked => _fireExtinguisherChecked;

        [SerializeField] private TextMeshProUGUI _failMessage;

        [Space]
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _continueText;
        
        [Space] [SerializeField] private Ventilation _ventilation;
        
        [SerializeField] private ActionOutcome _actionOutcome;
        
        [Space] 
        [SerializeField] private ParticleSystem _smoke;
        

        private bool _ventilationEnabled;
        private bool _fireExtinguisherChecked;
        
        private bool _shouldRestart;
        private bool _shouldContinue;

        private void Start()
        {
            _smoke.Pause();
        }

        private void Update()
        {
            LoadSceneIfNeeded();
        }

        private void LoadSceneIfNeeded()
        {
            if (_shouldRestart && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("ThirdScene");
            }

            if (_shouldContinue && Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("FourthScene");
            }
        }

        public void ShowResult()
        {
            StartCoroutine(ShowResultThroughTime());
        }

        public void EnableVentilation()
        {
            _ventilationEnabled = true;
            _ventilation.Enable();
            
            _actionOutcome.ShowCorrect();
        }

        public void CheckFireExtinguisher()
        {
            _fireExtinguisherChecked = true;
            
            _actionOutcome.ShowCorrect();
        }

        private void ShowFailMessage()
        {
            _failMessage.enabled = true;
            
            _shouldRestart = true;
            _restartText.enabled = true;
        }

        private void ShowSuccess()
        {
            _shouldContinue = true;
            _continueText.enabled = true;
        }

        private IEnumerator ShowResultThroughTime()
        {
            yield return new WaitForSeconds(1f);
            
            if (_ventilationEnabled && _fireExtinguisherChecked)
            {
                _actionOutcome.ShowCorrect();
                ShowSuccess();
            }
            else
            {
                _actionOutcome.ShowDanger();
                ShowFailMessage();
                ShowEffects();
            }
        }

        private void ShowEffects()
        {
            _smoke.Play();
            RenderSettings.fog = true;
        }
    }
}
using System;
using System.Collections;
using RoomObjects;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Welder
{
    public class IgnitionEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _failMessage;
        
        [Space]
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _continueText;
        
        [Space]
        [SerializeField] private ActionOutcome _actionOutcome;
        
        [Space] 
        [SerializeField] private ParticleSystem _fire;

        private bool _shouldRestart;
        private bool _shouldContinue;

        private void Start()
        {
            _fire.Pause();
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

        public void ShowFailMessage()
        {
            _failMessage.enabled = true;
            
            _shouldRestart = true;
            _restartText.enabled = true;
        }

        public void ShowSuccess()
        {
            _shouldContinue = true;
            _continueText.enabled = true;
        }

        private void ShowEffects()
        {
            _fire.Play();
            RenderSettings.fog = true;
        }

        public void ShowResult()
        {
            StartCoroutine(MakeFire());
        }

        private IEnumerator MakeFire()
        {
            yield return new WaitForSeconds(2f);
            _fire.Play();
        }
    }
}
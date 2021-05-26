﻿using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Welder
{
    public class IgnitionEffect : MonoBehaviour
    {
        public bool FireActive => _fire.isPlaying;
        
        [SerializeField] private TextMeshProUGUI _failMessage;

        [Space]
        [SerializeField] private TextMeshProUGUI _restartText;

        [SerializeField] private TextMeshProUGUI _continueText;


        [Space]
        [SerializeField] private ActionOutcome _actionOutcome;

        [Space] 
        [SerializeField] private ParticleSystem _fire;

        [SerializeField] private float _timeToPutOutFire;


        private bool _firePutOut;

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

        public void ShowResult()
        {
            StartCoroutine(MakeFire());
        }

        public void PutOutFire()
        {
            _actionOutcome.ShowCorrect();
            _firePutOut = true;
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

        private IEnumerator MakeFire()
        {
            yield return new WaitForSeconds(2f);
            _fire.Play();

            StartCoroutine(GiveTimeToPutOutFire());
        }

        private IEnumerator GiveTimeToPutOutFire()
        {
            var timeLast = 0f;

            while (timeLast <= _timeToPutOutFire)
            {
                if (_firePutOut)
                {
                    ShowSuccess();
                    yield break;
                }
                
                timeLast += Time.deltaTime;
                
                yield return null;
            }

            FailScene();
        }

        private void FailScene()
        {
            var shape = _fire.shape;
            shape.radius = 3;

            _actionOutcome.ShowDanger();
            ShowFailMessage();
        }
    }
}
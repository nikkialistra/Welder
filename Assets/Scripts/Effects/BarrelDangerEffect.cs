using System.Collections;
using RoomObjects;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Effects
{
    public class BarrelDangerEffect : MonoBehaviour
    {
        [SerializeField] private BarrelMover _barrelMover;
        
        [Space]
        [SerializeField] private TextMeshProUGUI _failMessage;

        [Space]
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _continueText;

        [Space]
        [SerializeField] private ActionOutcome _actionOutcome;

        [Space] 
        [SerializeField] private ParticleSystem _fire;

        private bool _barrelsMovedAway;

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
            StartCoroutine(ShowResultAfter());
        }

        private IEnumerator ShowResultAfter()
        {
            yield return new WaitForSeconds(2f);
            if (_barrelsMovedAway)
            {
                _actionOutcome.ShowCorrect();
                ShowSuccess();
                _shouldContinue = true;
            }
            else
            {
                _actionOutcome.ShowDanger();
                MakeFire();
                ShowFailMessage();
                _shouldRestart = true;
            }
        }

        private void MakeFire()
        {
            _fire.Play();
        }

        public void MoveAwayBarrels()
        {
            _barrelMover.MoveAway();
            _barrelsMovedAway = true;
        }

        private void LoadSceneIfNeeded()
        {
            if (_shouldRestart && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("FifthScene");
            }

            if (_shouldContinue && Input.GetKeyDown(KeyCode.T))
            {
                Application.Quit();
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
    }
}
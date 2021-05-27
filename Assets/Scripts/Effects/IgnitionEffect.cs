using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Effects
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

        [Space]
        [SerializeField] private AudioSource _putOutWithSand;
        [SerializeField] private AudioSource _burning;

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
            _putOutWithSand.Play();
            
            _actionOutcome.ShowCorrect();
            _firePutOut = true;
        }

        private void LoadSceneIfNeeded()
        {
            if ((_shouldRestart || _shouldContinue) && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("FourthScene");
            }

            if (_shouldContinue && Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("FifthScene");
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
            _burning.Stop();
            _fire.Stop();
            
            _shouldContinue = true;
            _continueText.enabled = true;
        }

        private IEnumerator MakeFire()
        {
            yield return new WaitForSeconds(2f);
            _burning.Play();
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
                    _actionOutcome.ShowCorrect();
                    ShowSuccess();
                    yield break;
                }
                
                timeLast += Time.deltaTime;
                
                yield return null;
            }

            _actionOutcome.ShowDanger();
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
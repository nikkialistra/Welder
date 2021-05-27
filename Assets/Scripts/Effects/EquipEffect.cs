using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using VisualEffects;
using Welder;

namespace Effects
{
    public class EquipEffect : MonoBehaviour
    {
        [SerializeField] private WelderAnimator _welderAnimator;
        [SerializeField] private Equipment _equipment;
        
        [Space]
        [SerializeField] private ActionOutcome _actionOutcome;

        [Space]
        [SerializeField] private BlindnessEffect _blindnessEffect;
        [SerializeField] private BurningEffect _burningEffect;

        [Space]
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _continueText;
        
        [Space] [SerializeField] private AudioSource _hurted;

        private bool _shouldRestart;
        private bool _shouldContinue;

        private void Start()
        {
            _welderAnimator.Welding += OnWelding;
        }

        private void Update()
        {
            LoadSceneIfNeeded();
        }

        private void LoadSceneIfNeeded()
        {
            if ((_shouldRestart || _shouldContinue) && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("FirstScene");
            }

            if (_shouldContinue && Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("SecondScene");
            }
        }

        private void OnWelding()
        {
            if (_equipment.MaskChecked && _equipment.GlovesChecked)
            {
                _actionOutcome.ShowCorrect();

                _continueText.enabled = true;
                _shouldContinue = true;
            }
            else
            {
                _actionOutcome.ShowDanger("Неисправные средства защиты приводят к повреждению здоровья.");
                ShowOutcomeEffects();
            }
        }

        private void ShowOutcomeEffects()
        {
            _hurted.Play();
            
            if (!_equipment.MaskChecked)
            {
                _blindnessEffect.Show();
            }

            if (!_equipment.GlovesChecked)
            {
                _burningEffect.Show();
            }

            _restartText.enabled = true;
            _shouldRestart = true;
        }
    }
}
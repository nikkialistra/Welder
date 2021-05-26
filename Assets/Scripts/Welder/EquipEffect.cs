using Effects;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Welder
{
    [RequireComponent(typeof(WelderAnimator))]
    [RequireComponent(typeof(Equipment))]
    public class EquipEffect : MonoBehaviour
    {
        [SerializeField] private ActionOutcome _actionOutcome;

        [Space]
        [SerializeField] private BlindnessEffect _blindnessEffect;
        [SerializeField] private BurningEffect _burningEffect;

        [Space]
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _continueText;

        private bool _shouldRestart;
        private bool _shouldContinue;

        private WelderAnimator _welderAnimator;
        private Equipment _equipment;

        private void Awake()
        {
             _welderAnimator = GetComponent<WelderAnimator>();
             _equipment = GetComponent<Equipment>();
        }

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
            if (_shouldRestart && Input.GetKeyDown(KeyCode.R))
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
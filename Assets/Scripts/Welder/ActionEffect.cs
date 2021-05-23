using Effects;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Welder
{
    [RequireComponent(typeof(WelderAnimator))]
    [RequireComponent(typeof(Equipment))]
    public class ActionEffect : MonoBehaviour
    {
        [SerializeField] private ActionOutcome _actionOutcome;

        [SerializeField] private BlindnessEffect _blindnessEffect;
        [SerializeField] private BurningEffect _burningEffect;

        [SerializeField] private TextMeshProUGUI _restartText;
        
        private bool shouldRestart;

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
            if (!shouldRestart)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("FirstScene");
            }
        }

        private void OnWelding()
        {
            if (_equipment.MaskChecked && _equipment.GlovesChecked)
            {
                _actionOutcome.ShowCorrect();
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
            shouldRestart = true;
        }
    }
}
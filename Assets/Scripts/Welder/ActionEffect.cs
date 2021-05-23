using Services;
using UnityEngine;

namespace Welder
{
    [RequireComponent(typeof(WelderAnimator))]
    [RequireComponent(typeof(Equipment))]
    public class ActionEffect : MonoBehaviour
    {
        [SerializeField] private ActionOutcome _actionOutcome;

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

        private void OnWelding()
        {
            if (_equipment.MaskChecked && _equipment.GlovesEquiped)
            {
                _actionOutcome.ShowCorrect();
            }
            else
            {
                _actionOutcome.ShowDanger("Неисправные средства защиты приводят к повреждению здоровья (ожог сетчатки глаз или ожог кожи)");
            }
        }
    }
}
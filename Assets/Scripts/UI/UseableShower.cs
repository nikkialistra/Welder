using System;
using Effects;
using RoomObjects.Interactables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Welder;

namespace UI
{
    public class UseableShower : MonoBehaviour
    {
        [SerializeField] private ObjectUtilizer _objectUtilizer;
        [SerializeField] private Equipment _equipment;
        [SerializeField] private WelderAnimator _welderAnimator;

        [Space]
        [SerializeField] private RectTransform _useableChoices;
        [SerializeField] private TextMeshProUGUI _text;
        
        [Space]
        [SerializeField] private Button _use;

        [Space]
        [SerializeField] private CheckingEffect _checkingEffect;
        [SerializeField] private IgnitionEffect _ignitionEffect;
        [SerializeField] private BarrelDangerEffect _barrelDangerEffect;
        
        
        private bool _checkingScene;
        private bool _ignitionScene;
        private bool _barrelDangerScene;

        private Useable _useable;
        
        private bool _showing;

        private void Start()
        {
            if (_checkingEffect != null)
            {
                _checkingScene = true;
            }

            if (_ignitionEffect != null)
            {
                _ignitionScene = true;
            }

            if (_barrelDangerEffect != null)
            {
                _barrelDangerScene = true;
            }
        }

        private void Update()
        {
            if (!_showing || _useable == null)
            {
                return;
            }

            CheckKeyPresses();
        }

        private void CheckKeyPresses()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Use();
            }
        }

        public void Show(Useable useable)
        {
            switch (useable.UseableType)
            {
                case UseableTypes.WeldingHandle:
                    if (!CheckRequirementsForWeldingHandle())
                    {
                        return;
                    }
                    _text.text = "Использовать (E)";
                    break;
                case UseableTypes.Barrel:
                    if (!CheckRequirementsForBarrel())
                    {
                        return;
                    }
                    _text.text = "Использовать (E)";
                    break;
                case UseableTypes.VentilationSystem:
                    if (_checkingEffect.VentilationEnabled)
                    {
                        return;
                    }
                    _text.text = "Bключить вентиляцию (E)";
                    break;
                case UseableTypes.FireExtinguisher:
                    if (_checkingEffect.FireExtinguisherChecked)
                    {
                        return;
                    }
                    _text.text = "Проверить исправность (E)";
                    break;
                case UseableTypes.Sand:
                    if (!_ignitionEffect.FireActive)
                    {
                        return;
                    }
                    _text.text = "Подавить огонь песком (Е)";
                    break;
                case UseableTypes.Barrels:
                    _text.text = "Попросить убрать бочки не менее, чем на 5 метров (Е)";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _useableChoices.gameObject.SetActive(true);
            
            if (_showing && _useable == useable)
            {
                return;
            }

            _showing = true;
            _useable = useable;

            ShowUseableChoices();
        }

        private bool CheckRequirementsForWeldingHandle()
        {
            if (!_equipment.MaskEquiped || !_equipment.GlovesEquiped)
            {
                return false;
            }

            return true;
        }

        private bool CheckRequirementsForBarrel()
        {
            if (!_objectUtilizer.HoldingWeldingHandle || _welderAnimator.IsWelding)
            {
                return false;
            }

            return true;
        }

        private void Use()
        {
            switch (_useable.UseableType)
            {
                case UseableTypes.WeldingHandle:
                    _use.interactable = false;
                    _objectUtilizer.Use(_useable);
                    break;
                case UseableTypes.Barrel:
                    _use.interactable = false;
                    _objectUtilizer.Use(_useable);
                    
                    PickOutcome();
                    break;
                case UseableTypes.VentilationSystem:
                    _checkingEffect.EnableVentilation();
                    break;
                case UseableTypes.FireExtinguisher:
                    _checkingEffect.CheckFireExtinguisher();
                    break;
                case UseableTypes.Sand:
                    if (_ignitionScene)
                    {
                        _ignitionEffect.PutOutFire();
                    }
                    break;
                case UseableTypes.Barrels:
                    if (_barrelDangerScene)
                    {
                        _barrelDangerEffect.MoveAwayBarrels();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Hide();
        }

        private void PickOutcome()
        {
            if (_checkingScene)
            {
                _checkingEffect.ShowResult();
            }

            if (_ignitionScene)
            {
                _ignitionEffect.ShowResult();
            }
            
            if (_barrelDangerScene)
            {
                _barrelDangerEffect.ShowResult();
            }
        }

        private void Hide()
        {
            _showing = false;
            _useable = null;
            
            _useableChoices.gameObject.SetActive(false);
        }

        private void ShowUseableChoices()
        {
            _use.interactable = true;
        }
    }
}
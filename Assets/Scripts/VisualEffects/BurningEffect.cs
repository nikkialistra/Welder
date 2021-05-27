using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace VisualEffects
{
    [RequireComponent(typeof(Volume))]
    public class BurningEffect : MonoBehaviour, IEffect
    {
        [SerializeField] private float _fullTime;
        [SerializeField] private float _blinkTime;
        
        [SerializeField] private TextMeshProUGUI _message;

        private float _effectCurrentTime;

        private Volume _volume;

        private WhiteBalance _whiteBalance;
        private bool _firstBlinkWasActivated;
        private bool _secondBlinkWasActivated;
        private bool _thirdBlinkWasActivated;

        private void Awake()
        {
            _volume = GetComponent<Volume>();
            _volume.sharedProfile.TryGet(out _whiteBalance);
        }

        public void Show()
        {
            _message.enabled = true;
            StartCoroutine(ShowBurningOverTime());
        }

        public void Reset()
        {
            _whiteBalance.temperature.SetValue(new FloatParameter(1f));
            _whiteBalance.tint.SetValue(new FloatParameter(1f));
        }

        private float GetFraction()
        {
            _effectCurrentTime += Time.deltaTime;
            return _effectCurrentTime / _fullTime;
        }

        private IEnumerator ShowBurningOverTime()
        {
            _firstBlinkWasActivated = false;
            _secondBlinkWasActivated = false;
            _thirdBlinkWasActivated = false;

            var blinkTime = 0f;

            _effectCurrentTime = 0f;
            
            while (_effectCurrentTime < _fullTime)
            {
                var fraction = GetFraction();

                if (ShouldBlink(fraction))
                {
                    blinkTime = _blinkTime;
                }

                blinkTime = SetBurningEffect(blinkTime);

                yield return null;
            }
        }

        private bool ShouldBlink(float fraction)
        {
            if (fraction > 0.20 && !_firstBlinkWasActivated)
            {
                _firstBlinkWasActivated = true;
                return true;
            }

            if (fraction > 0.40 && !_secondBlinkWasActivated)
            {
                _secondBlinkWasActivated = true;
                return true;
            }

            if (fraction > 0.60 && !_thirdBlinkWasActivated)
            {
                _thirdBlinkWasActivated = true;
                return true;
            }

            return false;
        }

        private float SetBurningEffect(float blinkTime)
        {
            if (blinkTime > 0)
            {
                _whiteBalance.temperature.SetValue(new FloatParameter(100f));
                _whiteBalance.tint.SetValue(new FloatParameter(100f));

                blinkTime -= Time.deltaTime;
            }
            else
            {
                _whiteBalance.temperature.SetValue(new FloatParameter(1f));
                _whiteBalance.tint.SetValue(new FloatParameter(1f));
            }

            return blinkTime;
        }
    }
}
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace VisualEffects
{
    [RequireComponent(typeof(Volume))]
    public class BlindnessEffect : MonoBehaviour, IEffect
    {
        [SerializeField] private float _fullTime;

        [SerializeField] private float _blindnessBloomIntensity;
        [SerializeField] private float _blindnessPostExposure;
        
        [SerializeField] private TextMeshProUGUI _message;

        private float _blindnessTotalTime;

        private Volume _volume;

        private Bloom _bloom;
        private ColorAdjustments _colorAdjustments;

        private void Awake()
        {
            _volume = GetComponent<Volume>();

            _volume.sharedProfile.TryGet(out _bloom);
            _volume.sharedProfile.TryGet(out _colorAdjustments);
        }

        public void Show()
        {
            _message.enabled = true;
            StartCoroutine(ShowBlindnessOverTime());
        }

        public void Reset()
        {
            _bloom.intensity.SetValue(new FloatParameter(0));
            _colorAdjustments.postExposure.SetValue(new FloatParameter(0));
        }

        private IEnumerator ShowBlindnessOverTime()
        {
            _blindnessTotalTime = 0f;
            
            while (_blindnessTotalTime < _fullTime)
            {
                var fraction = GetFraction();
                SetBlindnessEffect(fraction);

                yield return null;
            }
        }

        private float GetFraction()
        {
            _blindnessTotalTime += Time.deltaTime;
            return _blindnessTotalTime / _fullTime;
        }

        private void SetBlindnessEffect(float fraction)
        {
            _bloom.intensity.SetValue(new FloatParameter(_blindnessBloomIntensity * fraction));
            _colorAdjustments.postExposure.SetValue(new FloatParameter(_blindnessPostExposure * fraction));
        }
    }
}
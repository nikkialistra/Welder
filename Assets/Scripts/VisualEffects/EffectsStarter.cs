using UnityEngine;

namespace VisualEffects
{
    public class EffectsStarter : MonoBehaviour
    {
        private IEffect[] _effects;

        private void Awake()
        {
            _effects = GetComponents<IEffect>();
        }

        private void Start()
        {
            foreach (var effect in _effects)
            {
                effect.Reset();
            }
        }

        private void OnDisable()
        {
            foreach (var effect in _effects)
            {
                effect.Reset();
            }
        }
    }
}
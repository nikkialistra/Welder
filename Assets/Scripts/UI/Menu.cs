using System;
using UnityEngine;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public event Action Show;
        public event Action Hide;
        
        [SerializeField] private RectTransform _root;

        private bool _showing;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleMenu();
            }
        }

        private void ToggleMenu()
        {
            if (_showing)
            {
                _showing = false;
                HideMenu();
            }
            else
            {
                _showing = true;
                ShowMenu();
            }
        }

        private void HideMenu()
        {
            _root.gameObject.SetActive(false);
            Hide?.Invoke();
        }

        private void ShowMenu()
        {
            _root.gameObject.SetActive(true);
            Show?.Invoke();
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button _firstScene;
        [SerializeField] private Button _secondScene;
        [SerializeField] private Button _thirdScene;
        [SerializeField] private Button _fourthScene;
        [SerializeField] private Button _fifthScene;

        [SerializeField] private Button _quit;

        private void OnEnable()
        {
            _firstScene.onClick.AddListener(LoadFirstScene);
            _secondScene.onClick.AddListener(LoadSecondScene);
            _thirdScene.onClick.AddListener(LoadThirdScene);
            _fourthScene.onClick.AddListener(LoadFourthScene);
            _fifthScene.onClick.AddListener(LoadFifthScene);
            
            _quit.onClick.AddListener(Quit);
        }
        
        private void OnDisable()
        {
            _firstScene.onClick.RemoveListener(LoadFirstScene);
            _secondScene.onClick.RemoveListener(LoadSecondScene);
            _thirdScene.onClick.RemoveListener(LoadThirdScene);
            _fourthScene.onClick.RemoveListener(LoadFourthScene);
            _fifthScene.onClick.RemoveListener(LoadFifthScene);
            
            _quit.onClick.RemoveListener(Quit);
        }

        private void LoadFirstScene()
        {
            SceneManager.LoadScene("FirstScene");
        }

        private void LoadSecondScene()
        {
            SceneManager.LoadScene("SecondScene");
        }

        private void LoadThirdScene()
        {
            SceneManager.LoadScene("ThirdScene");
        }

        private void LoadFourthScene()
        {
            SceneManager.LoadScene("FourthScene");
        }

        private void LoadFifthScene()
        {
            SceneManager.LoadScene("FifthScene");
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}
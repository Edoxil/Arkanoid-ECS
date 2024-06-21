using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Arkanoid
{
    [RequireComponent(typeof(Button))]
    public class ReloadSceneButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
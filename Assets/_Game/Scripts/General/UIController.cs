using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Arkanoid
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private List<UIScreen> _allScreens;
        [SerializeField] private List<TextMeshProUGUI> _allScoreFields;

        private UIScreen _activeScreen;

        public void SwitchScreen<T>() where T : UIScreen
        {
            if (_activeScreen != null)
                _activeScreen.Hide();

            _activeScreen = _allScreens.First(s => s.GetType().Equals(typeof(T)));
            _activeScreen.Show();
        }

        public void SetScore(int score)
        {
            foreach (var textField in _allScoreFields)
            {
                textField.text = $"SCORE: {score}";
            }
        }

    }
}
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Screens
{
    public class StartScreen : Screen
    {
        public Button playButton;

        public void Awake()
        {
            playButton.onClick.AddListener(() => SceneManager.LoadScene("Scenes/GameScreen"));
        }
    }
}
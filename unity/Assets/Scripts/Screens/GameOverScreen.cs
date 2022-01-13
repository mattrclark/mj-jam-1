using Managers.WaveManagement;
using UnityEngine.UI;

namespace Screens
{
    public class GameOverScreen : Screen
    {
        public Text   endText;
        public Button retry;

        public void Awake()
        {
            retry.onClick.AddListener(WaveManager.Instance.Restart);
        }

        public void OnGameOver(int wavesCompleted)
        {
            endText.text = $"You managed to complete {wavesCompleted} waves!";
        }
    }
}
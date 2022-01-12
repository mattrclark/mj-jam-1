using UnityEngine.UI;

namespace Screens
{
    public class OverlayScreen : Screen
    {
        public Text healthText;
        public Text waveCountText;
        public Text enemyCountText;
        
        public void UpdateHealthText(string hp)
        {
            healthText.text = hp;
        }
        
        public void UpdateWaveCountText(int wave)
        {
            waveCountText.text = $"{wave}";
        }
        
        public void UpdateEnemyCountText(int enemyCount)
        {
            enemyCountText.text = $"{enemyCount}";
        }

        public void Win()
        {
            waveCountText.text = "WIN!";
        }
        
        public void Lose()
        {
            waveCountText.text = "LOSE!";
        }
    }
}
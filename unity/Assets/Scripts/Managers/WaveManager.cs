using UnityEngine;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        private int currentWave;

        public void StartWave()
        {
        }

        public void EndWave()
        {
        }

        public void NextWave()
        {
            currentWave++;
        }
    }
}
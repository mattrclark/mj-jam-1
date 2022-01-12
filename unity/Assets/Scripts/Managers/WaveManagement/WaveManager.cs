using System.Collections;
using Entities.AttackableEntities.Enemies;
using Entities.AttackableEntities.Player;
using Entities.Items;
using Managers.WaveManagement.WaveData;
using Screens;
using UnityEngine;

namespace Managers.WaveManagement
{
    /*
     * TODO: Add xp for killing enemies. This can used for auto upgrades as players play.
     * The more xp players gain, the better weapons and faster shooting speeds they get.
     * This needs to scale with the difficultly of the waves.
     * TODO: Add difficulty in wave variants.
     * TODO: Add 2 gamemodes -> Endless & Standard 
     * Standard: Limited number of waves with a boss at the end.
     *      Xp/Score & Time are recorded for leaderboard.
     * Endless: Use some waves as a start then add more enemies for each one at random.
     *      Keep going until the player dies. Save score of their xp gain.
     */

    public interface IWaveContext
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }

    public class WaveManager : MonoBehaviour, IWaveContext
    {
        [SerializeField] private bool isEnabled;

        // Spawners
        public EnemySpawner  spawnerGo;
        public Transform     enemiesParent;
        public Player        player;
        public UpgradeScreen upgradeScreen;
        public OverlayScreen overlay;
        public PauseScreen   pauseScreen;

        private EnemySpawner spawner;

        public HealthPickup healthPickupGo;

        private int currentWave;

        private int enemiesKilled;

        private Wave[]       waves;
        private HealthPickup currentWavePickup;
        private bool         paused;

        public static WaveManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);

            DontDestroyOnLoad(gameObject);

            spawner             = Instantiate(spawnerGo, Vector3.zero, Quaternion.identity);
            spawner.enemyParent = enemiesParent;

            var wv = new WaveVariantA(this, spawner);

            waves = wv.Waves;
            
            player.OnHealthUpdate.Add("updateOverlay", overlay.UpdateHealthText);

            overlay.UpdateHealthText($"{player.Health}/{player.MaxHealth}");
            
            StartWave();
        }

        private void StartWave()
        {
            if (!isEnabled)
                return;

            waves[currentWave].InitiateSequence();

            if (currentWave > 0 && (currentWavePickup == null || currentWavePickup.IsDisposed))
                currentWavePickup = Instantiate(healthPickupGo, Vector2.zero, Quaternion.identity);

            overlay.UpdateWaveCountText(currentWave + 1);
            overlay.UpdateEnemyCountText(waves[currentWave].TotalEnemies);
        }

        public void EnemyKilled()
        {
            if (!isEnabled)
                return;

            enemiesKilled++;
            overlay.UpdateEnemyCountText(waves[currentWave].TotalEnemies - enemiesKilled);

            if (enemiesKilled >= waves[currentWave].TotalEnemies)
                NextWave();
        }

        private void NextWave()
        {
            currentWave++;
            enemiesKilled = 0;

            if (currentWave >= waves.Length)
                overlay.Win();
            else
                StartWave();
        }

        private void NextWaveSet()
        {
            
        }

        public void OnGameOver()
        {
            overlay.Lose();
        }

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) 
                return;
            
            if (paused)
            {
                Time.timeScale      = 1;
                pauseScreen.Hide();
            }
            else
            {
                Time.timeScale      = 0;
                pauseScreen.Show();
            }

            paused = !paused;
        }
    }
}
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
        public CameraController cameraController;
        public EnemySpawner     spawnerGo;
        public Transform        enemiesParent;
        public Player           playerGo;
        public UpgradeScreen    upgradeScreen;
        public OverlayScreen    overlay;
        public PauseScreen      pauseScreen;
        public GameOverScreen   gameOverScreen;
        public HealthPickup     healthPickupGo;

        private int          currentWave;
        private HealthPickup currentWavePickup;
        private int          enemiesKilled;
        private bool         paused;
        private Player       player;
        private EnemySpawner spawner;
        private Wave[]       waves;

        public        int         CompletedWaves { get; private set; }
        public static WaveManager Instance       { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);

            DontDestroyOnLoad(gameObject);

            spawner             = Instantiate(spawnerGo, Vector3.zero, Quaternion.identity);
            spawner.enemyParent = enemiesParent;
            StartUp();
        }

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
                return;

            if (paused)
            {
                Time.timeScale = 1;
                pauseScreen.Hide();
            }
            else
            {
                Time.timeScale = 0;
                pauseScreen.Show();
            }

            paused = !paused;
        }

        private void StartUp()
        {
            player                    = Instantiate(playerGo, Vector3.zero, Quaternion.identity);
            player.OnHealthUpdate.Add("updateOverlay", overlay.UpdateHealthText);
            
            cameraController.followMe = player.transform;
            overlay.UpdateHealthText($"{player.Health}/{player.MaxHealth}");
            
            Time.timeScale = 1;
            CompletedWaves = 0;

            StartWaveSet();
        }

        private void StartWave()
        {
            if (!isEnabled)
                return;

            waves[currentWave].InitiateSequence();

            if (currentWave > 0 && (currentWavePickup == null || currentWavePickup.IsDisposed))
                currentWavePickup = Instantiate(healthPickupGo, Vector2.zero, Quaternion.identity, enemiesParent);

            overlay.UpdateWaveCountText(CompletedWaves + 1);
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
            CompletedWaves++;
            enemiesKilled = 0;

            if (currentWave >= waves.Length)
                OnWaveSetEnd();
            else
                StartWave();
        }

        private void OnWaveSetEnd()
        {
            void OnSelected()
            {
                upgradeScreen.Hide();
                Time.timeScale = 1;
                StartWaveSet();
            }

            Time.timeScale = 0;
            upgradeScreen.ConfigureOptionA("HP UP", () =>
                                                    {
                                                        player.IncreaseMaxHealth(1);
                                                        OnSelected();
                                                    });
            upgradeScreen.ConfigureOptionB("DAMAGE UP", () =>
                                                        {
                                                            player.IncreaseWeaponDamage(0.5f);
                                                            OnSelected();
                                                        });
            upgradeScreen.ConfigureOptionC("FIRE RATE UP", () =>
                                                           {
                                                               player.IncreaseFireRate(0.05f);
                                                               OnSelected();
                                                           });
            upgradeScreen.Show();
        }

        private void StartWaveSet()
        {
            WaveVariant wv;

            if (CompletedWaves == 0)
            {
                wv = new WaveVariantA(this, spawner);
            }
            else
            {
                if (Random.Range(0f, 1f) > 0.5f)
                    wv = new WaveVariantB(this, spawner);
                else
                    wv = new WaveVariantC(this, spawner);
            }

            waves       = wv.Waves;
            currentWave = 0;
            StartWave();
        }

        public void OnGameOver()
        {
            Time.timeScale = 0;
            overlay.Hide();
            gameOverScreen.OnGameOver(CompletedWaves);
            gameOverScreen.Show();
        }

        public void Restart()
        {
            for (var i = 0; i < enemiesParent.childCount; i++)
                Destroy(enemiesParent.GetChild(i).gameObject);

            if (!player.IsDisposed)
                Destroy(player.gameObject);

            gameOverScreen.Hide();
            overlay.Show();
            
            StartUp();
        }
    }
}
using System.Collections;
using Entities.AttackableEntities.Enemies;
using Entities.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.WaveManagement
{
    public interface IWaveContext
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }

    public class WaveManager : MonoBehaviour, IWaveContext
    {
        [SerializeField] private bool isEnabled;

        // Spawners
        public EnemySpawner northSpawner;
        public EnemySpawner southSpawner;
        public EnemySpawner eastSpawner;
        public EnemySpawner westSpawner;

        public HealthPickup healthPickupGo;

        public Text waveText;
        public Text enemiesLeftText;

        private int currentWave;

        private int enemiesKilled;

        private Wave[]       waves;
        private HealthPickup currentWavePickup;

        public static WaveManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);

            DontDestroyOnLoad(gameObject);

            waves = new[]
                    {
                        new Wave(this,
                                 new SpawnerSequence(northSpawner)
                                    .AddEnemy(EnemyType.Drone, 1)),
                        new Wave(this,
                                 new SpawnerSequence(southSpawner)
                                    .AddEnemy(EnemyType.Drone, 1)),
                        new Wave(this,
                                 new SpawnerSequence(westSpawner)
                                    .AddEnemy(EnemyType.Turret, 1),
                                 new SpawnerSequence(eastSpawner)
                                    .AddEnemy(EnemyType.Drone, 1)),
                        new Wave(this,
                                 new SpawnerSequence(northSpawner)
                                    .AddEnemy(EnemyType.Turret, 1),
                                 new SpawnerSequence(southSpawner)
                                    .AddEnemy(EnemyType.Turret, 1),
                                 new SpawnerSequence(westSpawner)
                                    .AddEnemy(EnemyType.Drone, 1)),
                        new Wave(this,
                                 new SpawnerSequence(northSpawner)
                                    .AddEnemy(EnemyType.Drone, 2)
                                    .AddPause(2f)
                                    .AddEnemy(EnemyType.Turret, 2)
                                    .AddPause(2f)
                                    .AddEnemy(EnemyType.Drone, 1),
                                 new SpawnerSequence(southSpawner)
                                    .AddEnemy(EnemyType.Drone, 2)
                                    .AddPause(2f)
                                    .AddEnemy(EnemyType.Drone, 4),
                                 new SpawnerSequence(eastSpawner)
                                    .AddEnemy(EnemyType.Drone, 2)
                                    .AddPause(2f)
                                    .AddEnemy(EnemyType.Turret, 1),
                                 new SpawnerSequence(westSpawner)
                                    .AddEnemy(EnemyType.Drone, 2)
                                    .AddPause(2f)
                                    .AddEnemy(EnemyType.Turret, 1))
                    };
            
            StartWave();
        }

        private void StartWave()
        {
            if (!isEnabled)
                return;
            
            waves[currentWave].InitiateSequence();

            if (currentWave > 0 && (currentWavePickup == null || currentWavePickup.IsDisposed))
                currentWavePickup = Instantiate(healthPickupGo, Vector2.zero, Quaternion.identity);

            waveText.text        = $"{currentWave + 1}";
            enemiesLeftText.text = $"{waves[currentWave].TotalEnemies}";
        }

        public void EndWave()
        {
        }

        public void EnemyKilled()
        {
            if (!isEnabled)
                return;
            
            enemiesKilled++;
            enemiesLeftText.text = $"{waves[currentWave].TotalEnemies - enemiesKilled}";

            if (enemiesKilled >= waves[currentWave].TotalEnemies)
                NextWave();
        }

        private void NextWave()
        {
            currentWave++;
            enemiesKilled = 0;

            if (currentWave >= waves.Length)
                waveText.text = "WIN!";
            else
                StartWave();
        }

        public void OnGameOver()
        {
            waveText.text = "LOSE!";
        }
    }
}
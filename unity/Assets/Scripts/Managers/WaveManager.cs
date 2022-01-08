using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities.AttackableEntities.Enemies;
using Entities.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public interface IWaveContext
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }

    public class WaveManager : MonoBehaviour, IWaveContext
    {
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

        private Wave[]     waves;
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
                                 new SpawnerSequence(eastSpawner)
                                    .AddEnemy(EnemyType.Drone, 1)),
                        new Wave(this,
                                 new SpawnerSequence(westSpawner)
                                    .AddEnemy(EnemyType.Drone, 1)),
                        new Wave(this,
                                 new SpawnerSequence(northSpawner)
                                    .AddEnemy(EnemyType.Drone, 2)
                                    .AddPause(5f)
                                    .AddEnemy(EnemyType.Drone, 1),
                                 new SpawnerSequence(southSpawner)
                                    .AddEnemy(EnemyType.Drone, 2)
                                    .AddPause(2f)
                                    .AddEnemy(EnemyType.Drone, 4))
                    };

            StartWave();
        }

        public void StartWave()
        {
            waves[currentWave].InitiateSequence();

            if(currentWave > 0 && (currentWavePickup == null || currentWavePickup.IsDisposed))
                currentWavePickup = Instantiate(healthPickupGo, Vector2.zero, Quaternion.identity);

            waveText.text        = $"{currentWave + 1}";
            enemiesLeftText.text = $"{waves[currentWave].TotalEnemies}";
        }

        public void EndWave()
        {
        }

        public void EnemyKilled()
        {
            enemiesKilled++;
            enemiesLeftText.text = $"{waves[currentWave].TotalEnemies - enemiesKilled}";

            if (enemiesKilled >= waves[currentWave].TotalEnemies)
                NextWave();
        }

        public void NextWave()
        {
            currentWave++;
            enemiesKilled = 0;

            if (currentWave >= waves.Length)
                waveText.text = "WIN!";
            else
                StartWave();
        }
    }

    public class Wave
    {
        private readonly IWaveContext      context;
        private readonly SpawnerSequence[] sequences;

        public Wave(IWaveContext context, params SpawnerSequence[] sequences)
        {
            this.context   = context;
            this.sequences = sequences;
        }

        public int TotalEnemies => sequences.Sum(s => s.TotalEnemies);

        public void InitiateSequence()
        {
            foreach (var sequence in sequences)
                context.StartCoroutine(sequence.Execute());
        }
    }

    public interface ISpawnerSequenceContext
    {
        void SpawnEnemy(EnemyType type);
    }

    public class SpawnerSequence : ISpawnerSequenceContext
    {
        private readonly List<SpawnerItem> items;

        private readonly ISpawner spawner;


        public SpawnerSequence(ISpawner spawner)
        {
            items        = new List<SpawnerItem>();
            this.spawner = spawner;
        }

        public static SpawnerSequence Empty => new SpawnerSequence(null);

        public int TotalEnemies => items.Sum(i =>
                                             {
                                                 if (i is EnemySpawnerItem e)
                                                     return e.TotalEnemies;
                                                 return 0;
                                             });

        void ISpawnerSequenceContext.SpawnEnemy(EnemyType type)
        {
            spawner.SpawnEnemy(type);
        }

        public IEnumerator Execute()
        {
            return items.Select(item => item.Execute()).GetEnumerator();
        }

        public SpawnerSequence AddEnemy(EnemyType type, int number)
        {
            items.Add(new EnemySpawnerItem(this, type, number));

            return this;
        }

        public SpawnerSequence AddPause(float time)
        {
            items.Add(new PauseSpawnerItem(time));
            return this;
        }
    }

    public abstract class SpawnerItem
    {
        public abstract IEnumerator Execute();
    }

    public class EnemySpawnerItem : SpawnerItem
    {
        private readonly ISpawnerSequenceContext context;
        private readonly EnemyType               type;

        public EnemySpawnerItem(ISpawnerSequenceContext context, EnemyType type, int totalEnemies)
        {
            this.context = context;
            this.type    = type;
            TotalEnemies = totalEnemies;
        }

        public int TotalEnemies { get; }

        public override IEnumerator Execute()
        {
            for (var i = 0; i < TotalEnemies; i++)
                context.SpawnEnemy(type);

            yield break;
        }
    }

    public class PauseSpawnerItem : SpawnerItem
    {
        private readonly float time;

        public PauseSpawnerItem(float time)
        {
            this.time = time;
        }

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(time);
        }
    }
}
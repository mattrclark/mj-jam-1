using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities.AttackableEntities.Enemies;
using UnityEngine;

namespace Managers.WaveManagement
{
    public interface ISpawnerSequenceContext
    {
        void SpawnEnemy(EnemyType type, Vector2 position);
    }
    
    public class WaveSequence : ISpawnerSequenceContext
    {
        private readonly List<SpawnerItem> items;

        private readonly ISpawner spawner;


        public WaveSequence(ISpawner spawner)
        {
            items        = new List<SpawnerItem>();
            this.spawner = spawner;
        }

        public static WaveSequence Empty => new WaveSequence(null);

        public int TotalEnemies => items.Sum(i =>
                                             {
                                                 if (i is EnemySpawnerItem e)
                                                     return e.TotalEnemies;
                                                 return 0;
                                             });

        void ISpawnerSequenceContext.SpawnEnemy(EnemyType type, Vector2 position)
        {
            spawner.SpawnEnemy(type, position);
        }

        public IEnumerator Execute()
        {
            return items.Select(item => item.Execute()).GetEnumerator();
        }

        public WaveSequence AddEnemy(EnemyType type, int number, Vector2 position)
        {
            items.Add(new EnemySpawnerItem(this, type, number, position));

            return this;
        }

        public WaveSequence AddPause(float time)
        {
            items.Add(new PauseSpawnerItem(time));
            return this;
        }
    }
}
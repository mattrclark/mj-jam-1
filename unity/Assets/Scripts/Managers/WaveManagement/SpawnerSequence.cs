using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities.AttackableEntities.Enemies;

namespace Managers.WaveManagement
{
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
}
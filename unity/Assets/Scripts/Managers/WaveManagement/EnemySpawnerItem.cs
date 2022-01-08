using System.Collections;
using Entities.AttackableEntities.Enemies;

namespace Managers.WaveManagement
{
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
}
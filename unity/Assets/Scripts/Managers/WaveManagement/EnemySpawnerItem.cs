using System.Collections;
using Entities.AttackableEntities.Enemies;
using UnityEngine;

namespace Managers.WaveManagement
{
    public class EnemySpawnerItem : SpawnerItem
    {
        private readonly ISpawnerSequenceContext context;
        private readonly EnemyType               type;
        private readonly Vector2                 position;

        public EnemySpawnerItem(ISpawnerSequenceContext context, EnemyType type, int totalEnemies, Vector2 position)
        {
            this.context  = context;
            this.type     = type;
            this.position = position;
            TotalEnemies  = totalEnemies;
        }

        public int TotalEnemies { get; }

        public override IEnumerator Execute()
        {
            for (var i = 0; i < TotalEnemies; i++)
                context.SpawnEnemy(type, position);

            yield break;
        }
    }
}
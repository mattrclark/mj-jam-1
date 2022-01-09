using UnityEngine;

namespace Entities.AttackableEntities.Enemies
{
    public interface ISpawner
    {
        void SpawnEnemy(EnemyType type, Vector2 position);
    }

    public class EnemySpawner : Entity, ISpawner
    {
        public Transform  enemyParent;
        public GameObject droneGo;
        public GameObject turretGo;

        public void SpawnEnemy(EnemyType type, Vector2 position)
        {
            void Spawn(GameObject go) => Instantiate(go,
                                                     position,
                                                     Quaternion.identity,
                                                     enemyParent);

            switch (type)
            {
                case EnemyType.Drone:
                    Spawn(droneGo);
                    break;
                case EnemyType.Turret:
                    Spawn(turretGo);
                    break;
                default:
                    goto case EnemyType.Drone;
            }
        }
    }

    public enum EnemyType
    {
        Drone,
        Turret,
        Charger
    }
}
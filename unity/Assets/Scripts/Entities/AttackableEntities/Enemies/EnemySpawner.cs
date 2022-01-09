using UnityEngine;

namespace Entities.AttackableEntities.Enemies
{
    public interface ISpawner
    {
        void SpawnEnemy(EnemyType type);
    }

    public class EnemySpawner : Entity, ISpawner
    {
        public Transform  enemyParent;
        public GameObject droneGo;
        public GameObject turretGo;

        // TODO: Remove me... Temp
        private float currentTime;

        // public void FixedUpdate()
        // {
        //     if (!(Time.fixedTime > currentTime + 5f))
        //         return;
        //     
        //     SpawnEnemy(EnemyType.Drone);
        //     currentTime = Time.fixedTime;
        // }

        public void SpawnEnemy(EnemyType type)
        {
            void Spawn(GameObject go) => Instantiate(go,
                                                     transform.position 
                                                   + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)),
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
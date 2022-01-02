using UnityEngine;

namespace Entities.Enemies
{
    public class EnemySpawnerController : MonoBehaviour
    {
        public Transform  enemyParent;
        public GameObject droneGo;

        // TODO: Remove me... Temp
        private float currentTime;

        public void FixedUpdate()
        {
            if (!(Time.fixedTime > currentTime + 5f))
                return;
            
            SpawnEnemy(EnemyType.Drone);
            currentTime = Time.fixedTime;
        }

        public void SpawnEnemy(EnemyType type)
        {
            void Spawn(GameObject go) => Instantiate(go, transform.position, Quaternion.identity, enemyParent);

            switch (type)
            {
                case EnemyType.Drone:
                    Spawn(droneGo);
                    break;
                default:
                    goto case EnemyType.Drone;
            }
        }
    }

    public enum EnemyType
    {
        Drone
    }
}
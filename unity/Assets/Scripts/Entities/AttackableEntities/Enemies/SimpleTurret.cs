using Entities.Weapons;
using Helpers;
using UnityEngine;

namespace Entities.AttackableEntities.Enemies
{
    public class SimpleTurret : Enemy
    {
        public  EnemyBullet bulletGo;
        private Transform   player;

        private float lastAttackTime;
        private float reloadTime;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player")?.transform;

            lastAttackTime = Time.fixedTime + 2f;
            reloadTime   = 1f;

            Initialise(5, 5);
        }

        protected override float DamageValue => 0f;

        private void FixedUpdate()
        {
            if (player == null)
                return;
            
            var playerPos = player.position;
            
            transform.LookAt2D(playerPos);
            
            Attack(playerPos - transform.position);
        }

        protected override void OnKilled()
        {
            if (Random.Range(0f, 1f) > 0.8f)
                Instantiate(healthPickup.gameObject, transform.position, Quaternion.identity);
        }

        private bool CanAttack => Time.fixedTime > lastAttackTime + reloadTime;

        private void Attack(Vector2 direction)
        {
            if (!CanAttack)
                return;

            lastAttackTime = Time.fixedTime;

            var bullet = Instantiate(bulletGo, transform.position, Quaternion.identity);

            bullet.GetComponent<SpriteRenderer>().color = Color.red;
            
            bullet.Initialise(1f, direction, 8f, 5f);
        }
    }
}
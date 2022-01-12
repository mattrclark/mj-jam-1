using Entities.AttackableEntities.Enemies;
using Entities.AttackableEntities.Player;
using Entities.Items;
using UnityEngine;

namespace Entities.Weapons
{
    public class EnemyBullet : Entity
    {
        private float       damage;
        private Vector2     velocity;
        private Rigidbody2D rb;
        private float       maxSpeed;

        public void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Initialise(float damage, Vector2 velocity, float maxSpeed, float ttl)
        {
            this.damage   = damage;
            this.velocity = velocity;
            this.maxSpeed = maxSpeed;

            WaitForSeconds(ttl, KillMe);
        }

        public void FixedUpdate()
        {
            rb.MovePosition(rb.position + velocity.normalized * maxSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out var player))
                player.Damage(damage);

            if (!other.TryGetComponent<Enemy>(out _)  &&
                !other.TryGetComponent<Bullet>(out _) &&
                !other.TryGetComponent<EnemyBullet>(out _) &&
                !other.TryGetComponent<HealthPickup>(out _))
                KillMe();
        }
    }
}
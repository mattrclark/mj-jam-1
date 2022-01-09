using Entities.Items;
using Managers.WaveManagement;
using UnityEngine;

namespace Entities.AttackableEntities.Enemies
{
    public abstract class Enemy : AttackableEntity
    {
        public HealthPickup   healthPickup;
        public SpriteRenderer bodySr;

        protected abstract float DamageValue { get; }

        protected override void OnDamaged(float value)
        {
            var originalColor = bodySr.color;

            bodySr.color = Color.red;

            WaitForSeconds(0.1f, () => bodySr.color = originalColor);

            if (!IsAlive)
                WaveManager.Instance.EnemyKilled();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<Player.Player>(out var p))
                p.Damage(DamageValue);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<Player.Player>(out var p))
                p.Damage(DamageValue);
        }
    }
}
using UnityEngine;

namespace Entities.Weapons
{
    public abstract class Weapon : Entity
    {
        public abstract void Attack(Vector2 direction);

        public virtual void IncreaseDamage(float damageIncrease)
        {
        }

        public virtual void IncreaseFireRate(float fireRateChange)
        {
        }
    }

    public class Pistol : Weapon
    {
        private const float  Ttl          = 2f;
        private       float  weaponDamage = 1f;
        private const float  BulletSpeed  = 10f;
        public        Bullet bulletType;
        private       float  lastAttackTime;
        private       float  reloadTime;

        private bool CanAttack => Time.fixedTime > lastAttackTime + reloadTime;

        public override void IncreaseDamage(float damageIncrease)
        {
            weaponDamage += damageIncrease;
        }

        public override void IncreaseFireRate(float fireRateChange)
        {
            reloadTime -= fireRateChange;
        }

        public void Awake()
        {
            lastAttackTime = 0;
            reloadTime     = 0.5f;
        }

        public override void Attack(Vector2 direction)
        {
            if (!CanAttack)
                return;

            lastAttackTime = Time.fixedTime;

            var bullet = Instantiate(bulletType, transform.position, Quaternion.identity);

            bullet.Initialise(weaponDamage, direction, BulletSpeed, Ttl);
        }
    }
}
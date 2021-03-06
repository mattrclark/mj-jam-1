using UnityEngine;

namespace Entities.AttackableEntities
{
    public abstract class AttackableEntity : Entity
    {
        public float Health    { get; private set; }
        public float MaxHealth { get; private set; }

        private int   iFrames;
        private float lastHitFrame;

        public bool IsAlive => Health > 0;

        protected void Initialise(float health, int iFrames)
        {
            MaxHealth    = health;
            Health       = health;
            this.iFrames = iFrames;
            lastHitFrame = 0;
        }

        public void Heal(float value)
        {
            Health += value;
            Health =  Mathf.Clamp(Health, 0, MaxHealth);
            OnHeal(value);
        }

        public void Damage(float damage)
        {
            if (damage == 0 || Time.frameCount <= lastHitFrame + iFrames)
                return;

            Health -= damage;

            lastHitFrame = Time.frameCount;

            OnDamaged(damage);

            if (!IsAlive)
                KillMe();
        }

        public void IncreaseMaxHealth(int value)
        {
            Health    += value;
            MaxHealth += value;
            OnHeal(value);
        }

        protected abstract void OnDamaged(float value);

        protected virtual void OnHeal(float value)
        {
        }
    }
}
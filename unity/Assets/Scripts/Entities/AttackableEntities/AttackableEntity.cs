using UnityEngine;

namespace Entities.AttackableEntities
{
    public abstract class AttackableEntity : Entity
    {
        private float health;

        private int   iFrames;
        private float lastHitFrame;

        public bool IsAlive => health > 0;

        protected void Initialise(float health, int iFrames)
        {
            this.health  = health;
            this.iFrames = iFrames;
            lastHitFrame = 0;
        }

        public void Damage(float damage)
        {
            if (Time.frameCount <= lastHitFrame + iFrames)
                return;
            
            health -= damage;

            lastHitFrame = Time.frameCount;
            
            OnDamaged();

            if (!IsAlive)
                KillMe();
        }

        protected abstract void OnDamaged();
    }
}
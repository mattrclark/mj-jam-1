using Entities.AttackableEntities.Player;
using UnityEngine;

namespace Entities.Items
{
    public class HealthPickup : Entity
    {
        private float value;

        public void Awake()
        {
            value = 1f;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<Player>(out var player))
                return;
            
            player.Heal(value);
            KillMe();
        }
    }
}
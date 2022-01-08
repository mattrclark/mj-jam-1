using Entities.Items;
using Managers.WaveManagement;
using UnityEngine;
using Values;

namespace Entities.AttackableEntities.Enemies
{
    public class Enemy : AttackableEntity
    {
        private const float MaxSpeed    = 2f;
        private const float DamageValue = 1;

        private Rigidbody2D rb;

        private Animator  animator;
        private Vector2   movement;
        private Transform player;

        public HealthPickup   healthPickup;
        public SpriteRenderer bodySr;

        private void Awake()
        {
            rb       = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player   = GameObject.FindWithTag("Player")?.transform;
            movement = new Vector2();

            Initialise(5, 5);
        }

        private void Update()
        {
            movement = (player.transform.position - transform.position).normalized;

            animator.SetFloat(AnimationNameStore.Horizontal, movement.x);
            animator.SetFloat(AnimationNameStore.Vertical,   movement.y);
            animator.SetFloat(AnimationNameStore.Speed,      movement.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement.normalized * MaxSpeed * Time.fixedDeltaTime);
        }

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

        protected override void OnKilled()
        {
            if (Random.Range(0f, 1f) > 0.8f)
                Instantiate(healthPickup.gameObject, transform.position, Quaternion.identity);
        }
    }
}
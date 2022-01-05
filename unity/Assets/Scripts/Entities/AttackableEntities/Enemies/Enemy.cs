using UnityEngine;

namespace Entities.AttackableEntities.Enemies
{
    public class Enemy : AttackableEntity
    {
        // ReSharper disable once InconsistentNaming
        private const float maxSpeed = 2f;

        private Rigidbody2D rb;

        private Animator  animator;
        private Vector2   movement;
        private Transform player;

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

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical",   movement.y);
            animator.SetFloat("Speed",      movement.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement.normalized * maxSpeed * Time.fixedDeltaTime);
        }

        protected override void OnDamaged()
        {
            var originalColor = bodySr.color;

            bodySr.color = Color.red;

            WaitForSeconds(0.1f, () => bodySr.color = originalColor);
        }
    }
}
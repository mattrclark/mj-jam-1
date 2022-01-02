using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        private const float maxSpeed = 2f;

        private Rigidbody2D rb;

        private Animator  animator;
        private Vector2   movement;
        private Transform player;

        private void Awake()
        {
            rb       = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player   = GameObject.FindWithTag("Player")?.transform;
            movement = new Vector2();
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
    }
}
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float maxSpeed = 5f;

        private Rigidbody2D rb;

        public Animator animator;

        private Vector2 movement;

        private void Awake()
        {
            rb       = GetComponent<Rigidbody2D>();
            movement = new Vector2();
        }

        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

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
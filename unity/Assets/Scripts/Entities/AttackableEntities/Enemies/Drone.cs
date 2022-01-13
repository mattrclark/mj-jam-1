using Managers.WaveManagement;
using UnityEngine;
using Values;

namespace Entities.AttackableEntities.Enemies
{
    public class Drone : Enemy
    {
        private float maxSpeed;

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

            Initialise(5, 5);

            maxSpeed = Mathf.Min(2f + WaveManager.Instance.CompletedWaves * 0.2f, 4f);
        }

        protected override float DamageValue => Mathf.Min(3f, Mathf.Ceil(WaveManager.Instance.CompletedWaves / 10f));

        private void Update()
        {
            if (player != null)
                movement = (player.transform.position - transform.position).normalized;

            animator.SetFloat(AnimationNameStore.Horizontal, movement.x);
            animator.SetFloat(AnimationNameStore.Vertical,   movement.y);
            animator.SetFloat(AnimationNameStore.Speed,      movement.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement.normalized * maxSpeed * Time.fixedDeltaTime);
        }

        protected override void OnKilled()
        {
            if (Random.Range(0f, 1f) > 0.9f)
                Instantiate(healthPickup.gameObject, transform.position, Quaternion.identity, transform.parent);
        }
    }
}
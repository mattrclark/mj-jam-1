using Entities.Weapons;
using Helpers;
using Managers.WaveManagement;
using UnityEngine;
using Values;

namespace Entities.AttackableEntities.Player
{
    public class Player : AttackableEntity
    {
        // ReSharper disable once InconsistentNaming
        private const float maxSpeed = 5f;

        public Animator  bodyAnimator;
        public Animator  weaponAnimator;
        public Transform weaponParent;

        public SpriteRenderer bodySr;
        public Weapon         weaponGo;

        private Vector2 attackDirection;
        private Weapon  currentWeapon;
        private Vector2 movement;

        private Event<string> onHealthUpdate;
        private Rigidbody2D   rb;

        public IEvent<string> OnHealthUpdate => onHealthUpdate;

        private void Awake()
        {
            onHealthUpdate  = new Event<string>("OnHealthUpdate");
            rb              = GetComponent<Rigidbody2D>();
            movement        = new Vector2();
            attackDirection = new Vector2();

            currentWeapon = Instantiate(weaponGo, new Vector2(0, 0.75f), Quaternion.identity, weaponParent);

            Initialise(10, 30);
        }

        private void Update()
        {
            void ChangeBodyAnimator(Animator animator, float x, float y, float mag)
            {
                animator.SetFloat(AnimationNameStore.Horizontal, x);
                animator.SetFloat(AnimationNameStore.Vertical,   y);
                animator.SetFloat(AnimationNameStore.Speed,      mag);
            }

            movement.x        = Input.GetAxisRaw(InputNameStore.MovementX);
            movement.y        = Input.GetAxisRaw(InputNameStore.MovementY);
            attackDirection.x = Input.GetAxisRaw(InputNameStore.AttackX);
            attackDirection.y = Input.GetAxisRaw(InputNameStore.AttackY);

            var bodyDirection   = new Vector2(movement.x,        movement.y);
            var weaponDirection = new Vector2(attackDirection.x, attackDirection.y);

            if (weaponDirection.sqrMagnitude == 0)
            {
                weaponDirection.x = movement.x;
                weaponDirection.y = movement.y;
            }

            ChangeBodyAnimator(weaponAnimator, weaponDirection.x, weaponDirection.y, weaponDirection.sqrMagnitude);

            if (bodyDirection.sqrMagnitude == 0 && weaponDirection.sqrMagnitude != 0)
            {
                bodyDirection.x = weaponDirection.x;
                bodyDirection.y = weaponDirection.y;
            }

            ChangeBodyAnimator(bodyAnimator, bodyDirection.x, bodyDirection.y, bodyDirection.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement.normalized * maxSpeed * Time.fixedDeltaTime);

            if (attackDirection.sqrMagnitude > 0)
                currentWeapon.Attack(attackDirection);
        }

        protected override void OnDamaged(float value)
        {
            var originalColor = bodySr.color;

            bodySr.color = Color.red;

            WaitForSeconds(0.1f, () => bodySr.color = originalColor);

            UpdateHealthText();

            if (!IsAlive)
                WaveManager.Instance.OnGameOver();
        }

        protected override void OnHeal(float value)
        {
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            onHealthUpdate.Invoke($"{Health}/{MaxHealth}");
        }
    }
}
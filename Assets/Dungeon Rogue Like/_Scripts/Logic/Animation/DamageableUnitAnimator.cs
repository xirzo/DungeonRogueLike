using Dungeon.Logic.Health;
using UnityEngine;

namespace Dungeon.Logic.Animation
{
    public class DamageableUnitAnimator : EntityAnimator
    {
        private IDamageable _health;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _health);

            _health.OnDamaged += ctx => Animator.SetTrigger("Damaged");
            _health.OnHealed += ctx => Animator.SetTrigger("Healed");
            _health.OnDied += () => Animator.SetTrigger("Died");
        }

        private void OnDestroy()
        {
            _health.OnDamaged -= ctx => Animator.SetTrigger("Damaged");
            _health.OnHealed -= ctx => Animator.SetTrigger("Healed");
            _health.OnDied -= () => Animator.SetTrigger("Died");
        }
    }
}

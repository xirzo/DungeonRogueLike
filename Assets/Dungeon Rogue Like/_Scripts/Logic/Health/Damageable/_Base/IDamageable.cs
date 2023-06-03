using System;
using UnityEngine.Events;

namespace Dungeon.Logic.Health
{
    public interface IDamageable
    {
        public event Action<float> OnHealthChanged;
        public event Action<float> OnDamaged;
        public event Action<float> OnHealed;
        public event Action OnDied;

        public void Damage(object sender, float damage);
        public void Heal(object sender, float health);
        public void Die(object sender);
    }
}

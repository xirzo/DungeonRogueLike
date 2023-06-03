using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Dungeon.Logic.Health
{
    public class EntityHealth : MonoBehaviour, IDamageable
    {
        public event Action<float> OnHealthChanged;
        public event Action<float> OnDamaged;
        public event Action<float> OnHealed;
        public event Action OnDied;

        [Header("Entity Health Properties:")]
        [Space(20)]
        [SerializeField, Range(0, 10000)] private float _currentHealth = 100f;
        [SerializeField, Range(0, 10000)] private float _maxHealth = 100f;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;

        public void Damage(object sender, float damage)
        {
            if (damage >= _currentHealth)
            {
                Die(sender);
            }
            else _currentHealth -= damage;

            OnHealthChanged?.Invoke(_currentHealth);
            OnDamaged?.Invoke(_currentHealth);
        }
        public void Heal(object sender, float health)
        {
            if (_currentHealth + health >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else _currentHealth += health;

            OnHealthChanged?.Invoke(_currentHealth);
            OnHealed?.Invoke(_currentHealth);
        }
        public void Die(object sender)
        {
            _currentHealth = 0;

            OnDied?.Invoke();
            StartCoroutine(DieCoroutine(0.5f));
        }

        private IEnumerator DieCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
    }
}

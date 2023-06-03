using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dungeon.Logic.Health;

namespace Dungeon.UI.InGame
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private EntityHealth _entityHealth;
        [SerializeField] private List<Image> _healthbars;
        [SerializeField] private float _barFillSpeed = 2f;

        private float _target;
        private float _maxHealth;

        private void Awake()
        {
            _target = 1;
            _maxHealth = _entityHealth.MaxHealth;

            UpdateHealthBar(_entityHealth.CurrentHealth);

            _entityHealth.OnHealthChanged += ctx => UpdateHealthBar(ctx);
        }
        private void OnDestroy()
        {
            _entityHealth.OnHealthChanged -= ctx => UpdateHealthBar(ctx);
            StopCoroutine(PlayeAnimation());
        }

        private void UpdateHealthBar(float health)
        {
            _target = health / _maxHealth;
            StartCoroutine(PlayeAnimation());
        }

        private IEnumerator PlayeAnimation()
        {
            foreach (var bar in _healthbars)
            {
                while (bar.fillAmount != _target)
                {
                    bar.fillAmount = Mathf.MoveTowards(bar.fillAmount, _target, _barFillSpeed * Time.deltaTime);
                    yield return null;
                }

                bar.fillAmount = _target;
            }
        }
    }
}

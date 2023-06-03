using UnityEngine;
using Dungeon.Logic.Health;

namespace Dungeon.Logic.Sound
{
    [RequireComponent(typeof(IDamageable))]
    public class DamageableSoundPlayer : SoundPlayer
    {
        [Header("Health Bar Sound Player Properties:")]
        [Space(20)]
        [SerializeField] private AudioClip _hurt;
        [SerializeField] private AudioClip _healed;
        [SerializeField] private AudioClip _died;

        private IDamageable _health;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _health);

            _health.OnDamaged += ctx => PlayClip(_hurt);
            _health.OnHealed += ctx => PlayClip(_died);
            _health.OnDied += () => PlayClip(_hurt);
        }

        private void OnDestroy()
        {
            _health.OnDamaged -= ctx => PlayClip(_hurt);
            _health.OnHealed -= ctx => PlayClip(_died);
            _health.OnDied -= () => PlayClip(_hurt);
        }
    }
}

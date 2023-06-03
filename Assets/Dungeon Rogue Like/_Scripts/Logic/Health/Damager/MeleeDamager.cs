using UnityEngine;

namespace Dungeon.Logic.Health
{
    public class MeleeDamager : MonoBehaviour, IDamager
    {
        [Header("Weapon Damager Properties:")]
        [Space(20)]
        [SerializeField, Range(0, 10000)] private float _damage = 10;

        public void Damage(IDamageable damageable)
        {
            damageable.Damage(this, _damage);
        }
    }
}

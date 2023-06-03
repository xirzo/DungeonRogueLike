using Dungeon.Logic.Inventory.ScriptableObjects.Useables.Weapons;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items.Useable.Weapons
{
    public abstract class Weapon : Useable
    {
        public new WeaponData Data { get { return (WeaponData)base.Data; } }

        public float TimeBetweenAttacks => Data.TimeBetweenAttacks;
        public bool CanAttack => _canAttack;

        private bool _canAttack = true;
        private float _elapsedTime;

        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
        }

        protected virtual void Update()
        {
            HandleAttackTimer();
        }

        public abstract void Attack();

        public override void Use()
        {
            if (_canAttack == true)
            {
                Attack();

                _canAttack = false;
                _elapsedTime = TimeBetweenAttacks;
            }
        }
        private void HandleAttackTimer()
        {
            if (_elapsedTime > 0)
            {
                _elapsedTime -= Time.deltaTime;
                return;
            }

            _canAttack = true;
        }

    }
}
using System;
using System.Collections;
using Dungeon.Logic.Interaction;
using Dungeon.Logic.Inventory.ScriptableObjects.Useables.Weapons.Ranged;
using Dungeon.Logic.ObjectPooling;
using Dungeon.Logic.Oritentation;
using Dungeon.Logic.Projectiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dungeon.Logic.Inventory.Items.Useable.Weapons.Ranged
{
    public class Ranged : Weapon
    {
        public override event Action OnUsed;

        public new RangedData Data { get { return (RangedData)base.Data; } }

        [Space]
        [SerializeField] private IOrienation _orientation;
        [SerializeField] private Projectile _projectilePrefab;
        [Space]
        [SerializeField] private Transform _muzzle;
        [SerializeField] private Transform _projectileContainer;

        private UnlimitedObjectPool<Projectile> _pool;

        private int _initialProjectileQuantity => Data.InitialProjectileQuantity;
        private float _timeBeforeDisappear => Data.TimeBeforeDisappear;
        private float _spread => Data.Spread;

        protected override void Awake()
        {
            base.Awake();

            _pool = new UnlimitedObjectPool<Projectile>(_projectilePrefab, _initialProjectileQuantity, _projectileContainer, _muzzle, true);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            StopAllCoroutines();
        }

        public override void Attack()
        {
            Shoot();
        }

        private void Shoot()
        {
            Vector3 targetPoint = _orientation.GetLookPoint();
            Vector3 direction = targetPoint - _muzzle.position;

            float xSpead = Random.Range(-_spread, _spread);
            float ySpead = Random.Range(-_spread, _spread);

            Vector3 spreadedDirection = direction + new Vector3(xSpead, ySpead, 0);

            Projectile projectile = _pool.Unpool();

            projectile.OnObstacleHit += () => _pool.Pool(projectile);

            projectile.transform.position = _muzzle.position;
            projectile.transform.forward = spreadedDirection.normalized;

            projectile.Launch(spreadedDirection);
            OnUsed?.Invoke();

            StartCoroutine(PoolCoroutine(projectile));
        }

        private IEnumerator PoolCoroutine(Projectile projectile)
        {
            float timeRemaining = 0;

            while (timeRemaining < _timeBeforeDisappear)
            {
                timeRemaining += Time.deltaTime;
                yield return null;
            }

            projectile.Rigidbody.linearVelocity = Vector2.zero;
            projectile.Rigidbody.angularVelocity = 0;
            projectile.OnObstacleHit -= () => _pool.Pool(projectile);

            _pool.Pool(projectile);
        }

        public override void Interact(IInteractor interactor)
        {
             interactor.Self.TryGetComponent(out _orientation);
        }
    }
}

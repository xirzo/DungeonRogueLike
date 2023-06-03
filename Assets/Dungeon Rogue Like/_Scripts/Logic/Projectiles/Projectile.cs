using System;
using Dungeon.Logic.Health;
using UnityEngine;

namespace Dungeon.Logic.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        public event Action OnObstacleHit;
        public LayerMask ObstacleLayer => _obstacleLayer;
        public float Speed => _speed;
        public Rigidbody2D Rigidbody => _rigidbody;


        [SerializeField] private LayerMask _obstacleLayer;
        [Space]
        [SerializeField, Min(0)] private float _speed = 10f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            TryGetComponent(out _rigidbody);
        }

        public void Launch(Vector3 direction)
        {
            _rigidbody.AddForce(transform.right * _speed, ForceMode2D.Impulse);
        }


        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.TryGetComponent(out EntityHealth targetHealth))
            {
               // _damager.Damage(targetHealth);
            }

            if (_obstacleLayer >> collision.gameObject.layer != 0)
            {
                OnObstacleHit?.Invoke();
            }
        }
    }
}

using System;
using UnityEngine;

namespace Dungeon.Logic.Projectiles
{
    public interface IProjectile
    {
        public event Action OnObstacleHit;
        public LayerMask ObstacleLayer { get; }
        public Rigidbody2D Rigidbody { get; }
        public float Speed { get; }
        public void Launch(Vector3 direction);
    }
}

using System;
using UnityEngine;
using Pathfinding;

namespace Dungeon.Logic.Movement
{
    [RequireComponent(typeof(AIDestinationSetter))]
    [RequireComponent(typeof(AIPath))]
    public class TargetMovement : MonoBehaviour, ITargetMovement
    {
        public event Action OnTargetReached;

        public Vector2 Movement => _movement;
        public float MovementX => _movementX;
        public float MovementY => _movementY;
        public Transform Target => _target;

        private Vector2 _movement;
        private float _movementX;
        private float _movementY;

        private Transform _target;
        private AIDestinationSetter _destitaionSetter;
        private AIPath _path;

        private void Awake()
        {
            TryGetComponent(out _destitaionSetter);
            TryGetComponent(out _path);

            _path.OnReached += () => OnTargetReached?.Invoke();
        }

        private void Update()
        {
            _movement = _path.velocity;
            _movementX = _path.velocity.x;
            _movementY = _path.velocity.y;
        }

        private void OnDestroy()
        {
            _path.OnReached -= () => OnTargetReached?.Invoke();
        }

        public void MoveTo(Transform target)
        {
            _target = target;
            _destitaionSetter.target = _target;
        }

        public void ClearTarget()
        {
            _target = null;
            _destitaionSetter.target = null;
        }
    }
}

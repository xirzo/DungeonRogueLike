using UnityEngine;
using Dungeon.Logic.Movement;

namespace Dungeon.Logic.Animation
{
    [RequireComponent(typeof(IRigidbodyMovement))]
    public class RigidbodyMovementAnimator : EntityAnimator
    {
        [Header("Rigidbody Movement Animator Properties:")]
        [Space(20)]

        private IRigidbodyMovement _movement;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _movement);
        }

        private void Update()
        {
            Animator.SetFloat("Velocity Magnitude", _movement.Velocity.magnitude);
        }
    }
}

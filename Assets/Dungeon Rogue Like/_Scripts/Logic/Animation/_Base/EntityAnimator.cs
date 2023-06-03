using UnityEngine;

namespace Dungeon.Logic.Animation
{
    [RequireComponent(typeof(Animator))]
    public class EntityAnimator : MonoBehaviour
    {
        public Animator Animator => _animator;

        private Animator _animator;

        protected virtual void Awake()
        {
            TryGetComponent(out _animator);
        }
    }
}

using UnityEngine;

namespace Dungeon.Logic.Movement
{
    public interface ITargetMovement
    {
        public void MoveTo(Transform target);
    }
}

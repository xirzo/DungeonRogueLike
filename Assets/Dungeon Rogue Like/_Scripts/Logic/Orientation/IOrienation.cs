using UnityEngine;

namespace Dungeon.Logic.Oritentation
{
    public interface IOrienation
    {
        public float RaycastDistance { get; }
        public bool TryToGetLookCollider(out Collider2D collider);
        public Vector2 GetLookPoint();
    }
}

using System;
using UnityEngine;

namespace Dungeon.Logic.Oritentation
{
    public class EntityOrientation : MonoBehaviour, IOrienation
    {
        public float RaycastDistance => _raycastDistance;

        [SerializeField, Min(0)] private float _raycastDistance = 5f;

        public bool TryToGetLookCollider(out Collider2D collider)
        {
            throw new NotImplementedException();
        }

        public Vector2 GetLookPoint()
        {
            throw new NotImplementedException();
        }
    }
}

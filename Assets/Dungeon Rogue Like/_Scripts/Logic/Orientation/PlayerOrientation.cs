using Dungeon.Logic.Inputs;
using Dungeon.Logic.Oritentation;
using UnityEngine;

namespace Game.Orientation
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerOrientation : MonoBehaviour, IOrienation
    {
        public float RaycastDistance => _raycastDistance;

        [Space]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _itemHolder;
        [Space]
        [SerializeField, Min(0)] private float _raycastDistance = 5f;

        private PlayerInput _inputs;

        private Vector2 _mousePosition;

        private float _rotationX;
        private float _rotationY;


        private void Awake()
        {
            TryGetComponent(out _inputs);
        }

        private void Update()
        {
            UpdateInput();
        }

        public bool TryToGetLookCollider(out Collider2D collider)
        {
            RaycastHit2D hit = Physics2D.Raycast(_itemHolder.position, _mousePosition, _raycastDistance);

            if (hit == true)
            {
                collider = hit.collider;
                return true;
            }

            collider = null;
            return false;
        }

        public Vector2 GetLookPoint()
        {
            RaycastHit2D hit = Physics2D.Raycast(_itemHolder.position, _mousePosition, _raycastDistance);
            return hit.point;
        }

        private void UpdateInput()
        {
            _mousePosition = _camera.ScreenToWorldPoint(_inputs.Actions.Player.Mouse.ReadValue<Vector2>());
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_itemHolder.position, _mousePosition);
            }
        }
    }
}

using UnityEngine;
using Dungeon.Logic.Inputs;

namespace Dungeon.Logic.Rotation
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerArmRotator : UnitArmRotator
    {
        [SerializeField] private Camera _camera;

        private PlayerInput _inputs;

        private Vector2 _mousePosition;

        private void Awake()
        {
            TryGetComponent(out _inputs);
        }

        private void Update()
        {
            _mousePosition = _camera.ScreenToWorldPoint(_inputs.Actions.Player.Mouse.ReadValue<Vector2>());
            RotateArm(_mousePosition);
        }
    }
}

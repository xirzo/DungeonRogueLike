using UnityEngine;
using Dungeon.Logic.Inputs;

namespace Dungeon.Logic.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IRigidbodyMovement
    {
        public Vector2 Velocity => _velocity;

        [Header("Movement Properties:")]
        [Space(20)]
        [SerializeField,Range(0,100)] private float _movementSpeed = 3f;
        [SerializeField,Range(0,100)] private float _movementSpeedMultiplier = 10f;
        [Space]
        [SerializeField, Range(0, 10)] private float _movementSmooth = 2f;

        private PlayerInput _inputs;
        private Rigidbody2D _rigidbody;

        private Vector2 _currentInput;
        private Vector2 _desiredInput;
        private Vector2 _smoothVelocity;
        private Vector2 _velocity;

        private void Awake()
        {
            TryGetComponent(out _inputs);
            TryGetComponent(out _rigidbody);
        }

        private void UpdateInput()
        {
            _desiredInput = _inputs.Actions.Player.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            _currentInput = Vector2.SmoothDamp(_currentInput, _desiredInput, ref _smoothVelocity, _movementSmooth * Time.deltaTime);
            _velocity = _currentInput * _movementSpeed * _movementSpeedMultiplier * Time.deltaTime;
            _rigidbody.linearVelocity = _velocity;
        }

        private void Update()
        {
            UpdateInput();
        }

        private void FixedUpdate()
        {
            Move();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Dungeon.Logic.Rotation
{
    public class UnitArmRotator : MonoBehaviour
    {
        [Header("Unit Arm Rotation Properties:")]
        [Space(20)]
        [SerializeField] private Transform _target;
        [Space(20)]
        [SerializeField, Range(0.1f, 10000f)] private float _rotationSpeed = 4;
        [Space(10)]
        [SerializeField] private List<SpriteRenderer> _characterRenderers;
        [Space(10)]
        [SerializeField] private List<SpriteRenderer> _weaponRenderers;
        [Space(10)]
        [SerializeField] private SpriteRenderer _armRenderer;
        [SerializeField] private Transform _armPivot;


        private Vector2 _direction;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        protected void RotateArm(Vector2 worldPosition)
        {
            if (!(worldPosition.normalized == worldPosition))
            {
                worldPosition = (worldPosition - (Vector2)transform.position).normalized;
            }

            float rotationZ = Mathf.Atan2(worldPosition.y, worldPosition.x) * Mathf.Rad2Deg;
            _armRenderer.flipY = Mathf.Abs(rotationZ) > 90f;

            foreach (SpriteRenderer characterRenderer in _characterRenderers)
            {
                characterRenderer.flipX = _armRenderer.flipY;
            }
            foreach (SpriteRenderer weaponRenderer in _weaponRenderers)
            {
                weaponRenderer.flipY = _armRenderer.flipY;
            }

            Quaternion desiredRotation = Quaternion.Euler(0, 0, rotationZ);

            _armPivot.rotation = Quaternion.Lerp(_armPivot.rotation, desiredRotation, Time.deltaTime * _rotationSpeed);
        }

        private void Update()
        {
            if (_target != null)
            {
                _direction = (_target.position);
                RotateArm(_direction);
            }
        }
    }
}

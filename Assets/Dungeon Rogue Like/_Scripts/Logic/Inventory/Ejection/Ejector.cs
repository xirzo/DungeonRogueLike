using System;
using System.Collections;
using Dungeon.Logic.Inventory.Items;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Ejection
{
    [DisallowMultipleComponent]
    public class Ejector : MonoBehaviour, IEjector
    {
        public event Action OnTriedToEject;
        public event Action<Item> OnEjected;

        public Transform EjectionPoint => _ejectionPoint;
        public Transform DefaultItemContainer => _itemHolder;

        [SerializeField, Min(0)] private float _ejectingSpeed = 4f;
        [SerializeField] private Transform _ejectionPoint;
        [SerializeField] private Transform _itemHolder;

        private Transform _container;

        protected void TryToEject()
        {
            OnTriedToEject?.Invoke();
        }

        public void Eject(Item item)
        {
            StartCoroutine(EjectCoroutine(item));
        }

        protected virtual void Awake()
        {
            if (_container == null)
            {
                _container = transform.parent;
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator EjectCoroutine(Item item)
        {
            item.transform.position = _container.position;

            Vector3 startScale = Vector3.zero;
            Vector3 endScale = Vector3.one;
            item.transform.localScale = startScale;

            item.transform.parent = _container;

            float step = _ejectingSpeed * Time.fixedDeltaTime;
            float time = 0;


            while (time <= 1.0f)
            {
                time += step;

                item.transform.position = Vector3.Lerp(item.transform.position, _ejectionPoint.position, time);
                item.transform.rotation = Quaternion.Lerp(item.transform.rotation, _ejectionPoint.rotation, time);
                item.transform.localScale = Vector3.Lerp(startScale, endScale, time);

                yield return null;
            }

            item.transform.position = _ejectionPoint.position;
            item.transform.rotation = _ejectionPoint.rotation;
            item.transform.transform.localScale = endScale;

            item.Rigidbody.velocity = Vector3.zero;
            item.Rigidbody.isKinematic = false;

            foreach (var collider in item.Colliders)
            {
                collider.enabled = true;
            }

            OnEjected?.Invoke(item);

            yield return null;
        }
    }
}

using System;
using System.Collections;
using Dungeon.Logic.Interaction;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items.Useable.Picking
{
    [RequireComponent(typeof(IInteractor))]
    public class ItemPicker : MonoBehaviour, IPicker
    {

        /// <summary>
        /// днаюбкъерэ юмхлюжхч ондмърхъ х р.д. (бгюхлндеиярбсер я INTERACTOR)
        /// </summary>

        public event Action<Item> OnPickedUp;
        public Transform ItemHolder => _itemHolder;

        [SerializeField] private Transform _itemHolder;
        [SerializeField, Min(0)] private float _pickingUpSpeed = 5f;

        private IInteractor _interactor;

        private void Awake()
        {
            TryGetComponent(out _interactor);
        }

        private void Start()
        {
            _interactor.OnInteracted += ctx => TryToPickUp(ctx);
        }

        private void OnDestroy()
        {
            _interactor.OnInteracted += ctx => TryToPickUp(ctx);

            StopAllCoroutines();
        }

        private void PickUp(Item item)
        {
            StartCoroutine(PickUpCoroutine(item));
        }

        public void TryToPickUp(IInteractable interactable)
        {
            if (interactable.Self.TryGetComponent(out Item item))
            {
                PickUp(item);
            }
        }

        private IEnumerator PickUpCoroutine(Item item)
        {
            item.Rigidbody.isKinematic = true;

            Vector3 startScale =  item.transform.localScale;
            Vector3 endScale = Vector3.zero;

            item.gameObject.transform.SetParent(_itemHolder);

            float step = _pickingUpSpeed * Time.fixedDeltaTime;
            float time = 0;

            while (time <= 1.0f)
            {
                if (item != null)
                {
                    time += step;

                    item.transform.position = Vector3.Lerp(item.transform.position, _itemHolder.position, time);
                    item.transform.rotation = Quaternion.Lerp(item.transform.rotation, _itemHolder.rotation, time);
                    item.transform.localScale = Vector3.Lerp(startScale, endScale, time);

                    yield return null;
                }

                yield return null;
            }

            item.transform.position = _itemHolder.position;
            item.transform.rotation = _itemHolder.rotation;
            item.transform.localScale = endScale;

            foreach (var collider in item.Colliders)
            {
                collider.enabled = false;
            }

            OnPickedUp?.Invoke(item);
        }
    }
}
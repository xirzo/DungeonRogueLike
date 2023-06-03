using Dungeon.Logic.Interaction;
using Dungeon.Logic.Inventory.ScriptableObjects;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour, IInteractable
    {
        public ItemData Data => _data;
        public GameObject Self => gameObject;
        public Rigidbody Rigidbody => _rigidbody;
        public Collider[] Colliders => _colliders;
        public bool IsEquipped { get; set; }
        public bool IsPooled { get; set; }

        [SerializeField] private ItemData _data;
        [Space]

        private Rigidbody _rigidbody;
        private Collider[] _colliders;


        protected virtual void Awake()
        {
            TryGetComponent(out _rigidbody);
            _colliders = GetComponentsInChildren<Collider>();
        }
        public abstract void Interact(IInteractor interactor);
    }
}

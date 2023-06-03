using System;
using System.Collections.Generic;
using Dungeon.Logic.Interaction;
using Dungeon.Logic.Inventory.Ejection;
using Dungeon.Logic.Inventory.Items;
using Dungeon.Logic.Inventory.Items.Useable.Picking;
using Dungeon.Logic.Inventory.Items.Useable.Selection;
using Dungeon.Logic.Inventory.ObjectPooling;
using UnityEngine;

namespace Dungeon.Logic.Inventory
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(IEjector))]
    [RequireComponent(typeof(ISelector))]
    [RequireComponent(typeof(IPicker))]
    [RequireComponent(typeof(IInteractor))]
    public class Inventory : MonoBehaviour
    {
        public event Action<Item> OnPickedUp;
        public event Action OnEjected;

        public bool HasInitialItems => _initialItems != null;

        [SerializeField, Min(0)] private int _capacity = 10;
        [Space]
        [SerializeField] private List<Item> _initialItems;
        public ItemObjectPool<Item> Pool { get; private set; }

        private IEjector _ejector;
        private ISelector _selector;
        private IPicker _picker;
        private IInteractor _interactor;

        protected virtual void Awake()
        {
            TryGetComponent(out _ejector);
            TryGetComponent(out _selector);
            TryGetComponent(out _picker);
            TryGetComponent(out _interactor);

            Pool = new ItemObjectPool<Item>(_ejector.DefaultItemContainer, _capacity, _initialItems);

            Pool.OnPooled += item => OnPickedUp?.Invoke(item);
            Pool.OnInitiallyPooled += item => OnInitiallyPooled(item);

            Pool.Initialize();
        }

        protected virtual void Start()
        {
            _ejector.OnTriedToEject += () => OnTriedToEject();
            _ejector.OnEjected += item => OnEjected?.Invoke();
            _picker.OnPickedUp += ctx => TryToPickUp(ctx);
        }

        protected virtual void OnDestroy()
        {
            Pool.OnPooled -= item => OnPickedUp?.Invoke(item);
            Pool.OnInitiallyPooled -= item => OnInitiallyPooled(item);
            _ejector.OnEjected -= transform => OnEjected?.Invoke();
            _ejector.OnTriedToEject -= () => OnTriedToEject();
            _picker.OnPickedUp -= ctx => TryToPickUp(ctx);
        }

        private void OnInitiallyPooled(Item item)
        {
            _interactor.Interact(item);
        }

        private void TryToPickUp(Item item)
        {
            Pool.TryTopool(item);
            Debug.Log("1");
        }

        private void OnTriedToEject()
        {
            if (_selector.CurrentlySelected != null)
            {
                if (Pool.TryToUnpool(_selector.CurrentlySelected))
                {
                    _ejector.Eject(_selector.CurrentlySelected);
                }
            }
        }
    }
}

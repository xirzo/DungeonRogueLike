using System;
using Dungeon.Logic.Inventory.ObjectPooling;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items.Useable.Selection
{
    [RequireComponent(typeof(Inventory))]
    [DisallowMultipleComponent]
    public class Selector : MonoBehaviour, ISelector
    {
        public event Action<Item, Item> OnSelected;
        public Item CurrentlySelected => _currentlySelected;
        public Item PreviouslySelected => _previouslySelected;

        private int _index;

        private Item _currentlySelected;
        private Item _previouslySelected;

        private Inventory _inventory;
        private ItemObjectPool<Item> _pool;

        protected virtual void Awake()
        {
            TryGetComponent(out _inventory);
        }

        protected virtual void Start()
        {
            _pool = _inventory.Pool;
            _inventory.OnPickedUp += OnPickedUp;
            _inventory.OnEjected += OnEjected;

            if (_inventory.HasInitialItems == true)
                SelectByIndex(0);
        }

        protected virtual void OnDestroy()
        {
            _inventory.OnPickedUp -= OnPickedUp;
            _inventory.OnEjected -= OnEjected;
        }

        private void OnPickedUp(Item item)
        {
            if (_currentlySelected == null)
            {
                Select(item);
            }
        }

        private void OnEjected()
        {
            _currentlySelected = null;

            Item itemAtStartIndex = _pool.GetElement(_index);

            if (itemAtStartIndex != null)
            {
                Select(itemAtStartIndex);
                return;
            }

            int closetIndex = _pool.GetClosetIndexInPool(_index);

            if (closetIndex == -1)
            {
                _index = 0;
                return;
            }

            SelectByIndex(closetIndex);
        }

        public void Select(Item item)
        {
            if (item == null) return;

            if (_currentlySelected != null)
            {
                _previouslySelected = _currentlySelected;
            }

            _index = _pool.GetElementIndex(item);
            _currentlySelected = item;
            _currentlySelected.IsEquipped = true;

            if (_previouslySelected == _currentlySelected)
            {
                _previouslySelected = null;
            }

            OnSelected?.Invoke(_previouslySelected, _currentlySelected);
        }

        public void Select(int offset)
        {
            Item item = _pool.GetElement(_index + offset);

            if (item == null)
            {
                //Debug.Log($"There is no item with index: {_index + offset}");
                return;
            }

            Select(item);
        }


        public void SelectByIndex(int index)
        {
            _index = index;
            Item item = _pool.GetElement(_index);

            Select(item);
        }
    }
}

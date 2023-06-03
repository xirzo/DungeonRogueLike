using System;
using System.Collections.Generic;
using System.Linq;
using Dungeon.Logic.Inventory.Items;
using Dungeon.Logic.ObjectPooling;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dungeon.Logic.Inventory.ObjectPooling
{
    public class ItemObjectPool<T> : ILimitedObjectPool<T> where T : Item
    {
        public event Action<T> OnPooled;
        public event Action<T> OnUnpooled;
        public event Action<T> OnInitiallyPooled;

        private List<T> _pool;
        private List<T> _initialElements;
        private Transform _container;

        private int _capacity;

        public ItemObjectPool(Transform container = null, int capacity = 1, List<T> initialElements = null)
        {
            _container = container;
            _capacity = capacity;
            _initialElements = initialElements;
        }

        public void Initialize()
        {
            _pool = new List<T>();

            if (_initialElements != null)
            {
                if (_initialElements.Count > _capacity)
                    throw new Exception($"Number of initial elements is higher than capacity!");

                for (int i = 0; i < _initialElements.Count; i++)
                {
                    T createdElement = Instantiate(_initialElements[i]);
                    createdElement.Rigidbody.isKinematic = true;
                    OnInitiallyPooled?.Invoke(createdElement);
                }
            }
        }

        public T GetElement(int index)
        {
            try
            {
                if (HasElementWithIndex(index) == false)
                    return null;

                return _pool[index];
            }

            catch
            {
                return null;
            }
        }
        public bool HasElementWithIndex(int index)
        {
            try
            {
                if (_pool[index] != null)
                    return true;
            }
            catch
            {
                return false;
            }

            return false;
        }
        public int GetClosetIndexInPool(int startIndex)
        {
            int index = startIndex;

            for (int i = 0; i < _pool.Count; i++)
            {
                if (HasElementWithIndex(index + 1))
                {
                    index = index + 1;
                    return index;
                }
            }

            for (int i = startIndex; i > 0; i--)
            {
                if (HasElementWithIndex(index - 1))
                {
                    index = index - 1;
                    return index;
                }
            }

            return -1;
        }
        public int GetElementIndex(T element)
        {
            return _pool.IndexOf(element);
        }
        public bool TryTopool(T element)
        {
            if (IsFull() == false)
            {
                Pool(element);
                return true;
            }

            //Debug.LogWarning($"There is space to pool element with name: {element.name}");
            return false;
        }
        public bool TryToUnpool(out T element, Type elementType)
        {
            foreach (T item in _pool)
            {
                if (item.IsEquipped == true
                    && item.GetType() == elementType)
                {
                    element = item;
                    Unpool(item);
                    return true;
                }
            }

            //Debug.LogWarning($"There is element with such type: {elementType}");
            element = null;
            return false;
        }
        public bool TryToUnpool(T element)
        {
            T foundItem = _pool.Find(item => item == element);

            if (foundItem != null)
            {
                if (element.IsEquipped == true)
                {
                    Unpool(foundItem);
                    return true;
                }
            }

            //Debug.LogWarning($"There is no element such as: {element}");
            return false;
        }
        public bool TryUnpoolLast(out T element)
        {
            T lastElement;

            try
            {
                lastElement = _pool.Last();
            }

            catch
            {
                //Debug.LogWarning($"There is no elements in pool!");
                element = null;
                return false;
            }

            element = lastElement;
            Unpool(element);
            return true;
        }
        public bool IsFull()
        {
            if (_pool.Count >= _capacity)
                return true;

            return false;
        }
        public bool HasEmptySlot(out int index)
        {
            if (IsFull() == true)
            {
                index = -1;
                return false;
            }

            for (int i = 0; i < _capacity; i++)
            {
                if (_pool[i] == null)
                {
                    index = i;
                    return true;
                }
            }

            index = -1;
            return false;
        }
        private void Pool(T element)
        {
            _pool.Add(element);
            element.gameObject.SetActive(false);
            element.IsPooled = true;
            OnPooled?.Invoke(element);

            //Debug.Log($"{element} was pooled, it`s index is {_pool.IndexOf(element)}!");
        }
        private void Unpool(T element)
        {
            _pool.Remove(element);
            element.gameObject.SetActive(true);
            element.IsPooled = false;
            OnUnpooled?.Invoke(element);
            //Debug.Log($"{element} was unpooled!");
        }
        private T Instantiate(T prefab)
        {
            T createdObject = Object.Instantiate(prefab, _container.position, _container.rotation, _container);
            Pool(createdObject);

            return createdObject;
        }
    }
}
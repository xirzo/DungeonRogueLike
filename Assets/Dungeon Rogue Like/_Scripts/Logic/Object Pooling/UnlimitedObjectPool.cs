using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dungeon.Logic.ObjectPooling
{
    public class UnlimitedObjectPool<T> : IUnlimitedObjectPool<T> where T : MonoBehaviour
    {
        public event Action<T> OnPooled;
        public event Action<T> OnUnpooled;

        public T Prefab => _prefab;
        public bool AutoExpand {get; set;}

        private List<T> _pool;

        private T _prefab;
        private Transform _container;
        private Transform _pointToInstantiate;

        public UnlimitedObjectPool(T prefab, int initialQuantity = 1, Transform container = null, Transform pointToSpawn = null, bool autoExpand = true)
        {
            _prefab = prefab;
            _container = container;
            _pointToInstantiate = pointToSpawn;
            AutoExpand = autoExpand;

            Initialize(initialQuantity);
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
        public int GetElementIndex(T element)
        {
            return _pool.IndexOf(element);
        }
        public void Pool(T element)
        {
            _pool.Add(element);
            element.gameObject.SetActive(false);
            OnPooled?.Invoke(element);

            //Debug.Log($"{element} was pooled, it`s index is {_pool.IndexOf(element)}!");
        }
        public T Unpool()
        {
            foreach (T element in _pool)
            {
                if (element.gameObject.activeInHierarchy == false)
                {
                    Unpool(element);
                    return element;
                }
            }

            if (AutoExpand == true)
            {
                T createdElement = Instantiate();
                Unpool(createdElement);

                return createdElement;
            }

            throw new Exception($"There is no pooled element: {typeof(T)}");
        }
        public T UnpoolLast()
        {
            T lastElement;

            try
            {
                lastElement = _pool.Last();
            }

            catch
            {
                T createdElement = Instantiate();
                Unpool(createdElement);
                return createdElement;
            }

            Unpool(lastElement);
            return lastElement;
        }
        private void Unpool(T element)
        {
            _pool.Remove(element);
            element.gameObject.SetActive(true);
            OnUnpooled?.Invoke(element);

            //Debug.Log($"{element} was unpooled!");
        }
        private T Instantiate()
        {
            T createdObject = Object.Instantiate(Prefab, _pointToInstantiate.position, _pointToInstantiate.rotation, _container);
            Pool(createdObject);

            return createdObject;
        }
        private void Initialize(int initialQuantity)
        {
            _pool = new List<T>();

            for (int i = 0; i < initialQuantity; i++)
            {
                Instantiate();
            }
        }
    }
}
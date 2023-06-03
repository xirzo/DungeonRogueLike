using System;
using UnityEngine;

namespace Dungeon.Logic.ObjectPooling
{
    public interface IObjectPool<T> where T : MonoBehaviour
    {
        public event Action<T> OnPooled;
        public event Action<T> OnUnpooled;

        public T GetElement(int index);
        public bool HasElementWithIndex(int index);
        public int GetElementIndex(T element);
    }
}
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Dungeon.Logic.ObjectPooling
{
    public interface ILimitedObjectPool<T> : IObjectPool<T> where T : MonoBehaviour
    {

        public int GetClosetIndexInPool(int startIndex);
        public bool TryTopool(T element);
        public bool TryToUnpool(T element);
        public bool TryUnpoolLast(out T element);
        public bool IsFull();
        public bool HasEmptySlot(out int index);
    }
}
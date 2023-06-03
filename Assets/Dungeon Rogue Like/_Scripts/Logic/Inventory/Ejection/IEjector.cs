using System;
using Dungeon.Logic.Inventory.Items;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Ejection
{
    public interface IEjector
    {
        public event Action OnTriedToEject;
        public event Action<Item> OnEjected;
        public Transform EjectionPoint { get; }
        public Transform DefaultItemContainer { get; }
        public void Eject(Item item);
    }
}

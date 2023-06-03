using System;
using Dungeon.Logic.Interaction;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items.Useable.Picking
{
    public interface IPicker
    {
        public event Action<Item> OnPickedUp;
        public Transform ItemHolder { get; }
        public void TryToPickUp(IInteractable target);
    }
}

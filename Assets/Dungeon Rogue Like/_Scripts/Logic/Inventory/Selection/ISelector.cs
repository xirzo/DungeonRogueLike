using System;

namespace Dungeon.Logic.Inventory.Items.Useable.Selection
{
    public interface ISelector
    {
        public event Action<Item, Item> OnSelected;
        public Item CurrentlySelected { get; }
        public Item PreviouslySelected { get; }
        public void Select(Item item);
    }
}

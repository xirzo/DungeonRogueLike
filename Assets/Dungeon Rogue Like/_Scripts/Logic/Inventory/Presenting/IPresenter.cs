using System;

namespace Dungeon.Logic.Inventory.Items.Useable.Presenting
{
    public interface IPresenter
    {
        public event Action OnPresented;
        public event Action OnDepresented;
        public Item CurrentlySelected { get; }
        public Item PreviouslySelected { get; }
        public void Present(Item previousItem, Item currentItem);
    }
}

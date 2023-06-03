using System;

namespace Dungeon.Logic.Inventory.Items.Useable.Use
{
    public interface IUser
    {
        public event Action OnUsed;
        public Useable CurrentlySelected { get; }
        public void TryToUse();
    }
}

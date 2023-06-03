using System;

namespace Dungeon.Logic.Inventory.Items.Useable
{
    public abstract class Useable : Item
    {
        public abstract event Action OnUsed;
        public abstract void Use();
    }
}

using System;

namespace Dungeon.Logic.Interfaces
{
    public interface IToggleable
    {
        public event Action<bool> OnToggled;
        public bool Toggled { get; }
        public void Toggle();
    }
}

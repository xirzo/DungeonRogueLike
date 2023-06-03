using System;
using Dungeon.Logic.Inventory.Items.Useable.Selection;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items.Useable.Use
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ISelector))]
    public class User : MonoBehaviour, IUser
    {
        public event Action OnUsed;
        public Useable CurrentlySelected => (Useable)_selector.CurrentlySelected;

        private ISelector _selector;

        protected virtual void Awake()
        {
            TryGetComponent(out _selector);
        }

        private void Use()
        {
            CurrentlySelected.Use();
            OnUsed?.Invoke();
        }

        public void TryToUse()
        {
            if (CurrentlySelected == null)
                return;

            Use();
        }
    }
}

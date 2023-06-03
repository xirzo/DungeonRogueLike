using Dungeon.Logic.Inputs;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items.Useable.Use
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerUser : User
    {
        private PlayerInput _inputs;

        protected override void Awake()
        {
            base.Awake();

            TryGetComponent(out _inputs);
        }

        private void Start()
        {
            _inputs.Actions.Player.Use.performed += ctx => TryToUse(); 
        }

        private void OnDestroy()
        {
            _inputs.Actions.Player.Use.performed -= ctx => TryToUse();
        }
    }
}

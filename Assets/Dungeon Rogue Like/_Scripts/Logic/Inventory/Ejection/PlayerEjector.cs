using Dungeon.Logic.Inputs;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Ejection
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerEjector : Ejector
    {
        private PlayerInput _inputs;

        protected override void Awake()
        {
            base.Awake();

            TryGetComponent(out _inputs);
        }

        private void Start()
        {
            _inputs.Actions.Player.Drop.performed += ctx => TryToEject();
        }

        private void OnDestroy()
        {
            _inputs.Actions.Player.Drop.performed -= ctx => TryToEject();
        }
    }
}

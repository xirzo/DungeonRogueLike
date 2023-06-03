using Dungeon.Logic.Inputs;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items.Useable.Selection
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerSelector : Selector
    {
        private PlayerInput _inputs;

        private float _scroll;

        protected override void Awake()
        {
            base.Awake();

            TryGetComponent(out _inputs);
        }

        protected override void Start()
        {
            base.Start();

        }

        private void Update()
        {
            _scroll = _inputs.Actions.Player.Scroll.ReadValue<Vector2>().normalized.y;

            if (_scroll > 0)
            {
                Select(1);
            }

            if (_scroll < 0)
            {
                Select(-1);
            }

        }
    }
}

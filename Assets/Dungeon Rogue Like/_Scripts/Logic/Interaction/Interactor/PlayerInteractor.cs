using Dungeon.Logic.Inputs;
using UnityEngine;

namespace Dungeon.Logic.Interaction
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInteractor : RaycastInteractor
    {
        private PlayerInput _inputs;

        protected override void Awake()
        {
            base.Awake();

            TryGetComponent(out _inputs);
        }

        private void Start()
        {
            _inputs.Actions.Player.Interact.performed += ctx => TryToInteract();
        }

        private void OnDestroy()
        {
            _inputs.Actions.Player.Interact.performed -= ctx => TryToInteract();
        }
    }
}

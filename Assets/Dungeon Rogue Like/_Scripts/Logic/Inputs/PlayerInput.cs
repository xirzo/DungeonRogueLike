using UnityEngine;

namespace Dungeon.Logic.Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        public InputActions Actions { get; private set; }

        private void Awake()
        {
            Actions = new InputActions();
            Actions.Enable();
        }

        private void OnDestroy()
        {
            Actions.Disable();
        }
    }
}

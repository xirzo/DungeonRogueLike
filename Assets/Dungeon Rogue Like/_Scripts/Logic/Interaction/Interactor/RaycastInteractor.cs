using Dungeon.Logic.Oritentation;
using UnityEngine;

namespace Dungeon.Logic.Interaction
{
    [RequireComponent(typeof(IOrienation))]
    public class RaycastInteractor : Interactor
    {
        private IOrienation _orientation;

        protected virtual void Awake()
        {
            TryGetComponent(out _orientation);
        }

        protected void TryToInteract()
        {
            if (_orientation.TryToGetLookCollider(out Collider2D collider) == true)
            {
                if (collider.attachedRigidbody != null)
                {
                    if (collider.attachedRigidbody.TryGetComponent(out IInteractable target))
                    {
                        Interact(target);
                    }
                }
            }
        }
    }
}

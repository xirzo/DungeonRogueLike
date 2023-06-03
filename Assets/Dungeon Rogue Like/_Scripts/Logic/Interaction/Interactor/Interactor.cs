using System;
using Dungeon.Logic.Interaction;
using UnityEngine;

namespace Dungeon.Logic.Interaction
{
    public class Interactor : MonoBehaviour, IInteractor
    {
        public event Action<IInteractable> OnInteracted;
        public GameObject Self => gameObject;

        public void Interact(IInteractable target)
        {
            target.Interact(this);
            OnInteracted?.Invoke(target);
        }
    }
}

using Dungeon.Logic.Interaction;
using UnityEngine;

namespace Dungeon.Logic.Interaction
{
    public interface IInteractable
    {
        public GameObject Self { get; }
        public void Interact(IInteractor interactor);
    }
}
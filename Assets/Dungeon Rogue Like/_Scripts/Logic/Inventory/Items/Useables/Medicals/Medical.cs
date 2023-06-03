using System;
using Dungeon.Logic.Interaction;

namespace Dungeon.Logic.Inventory.Items.Useable.Medicals
{
    public class Medical : Useable
    {
        public override event Action OnUsed;

        public override void Interact(IInteractor interactor)
        {
            throw new NotImplementedException();
        }

        public override void Use()
        {
            throw new NotImplementedException();
        }
    }
}

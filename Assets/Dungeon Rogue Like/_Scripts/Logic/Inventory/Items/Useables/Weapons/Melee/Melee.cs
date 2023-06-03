using System;
using Dungeon.Logic.Interaction;
using Dungeon.Logic.Inventory.ScriptableObjects.Useables.Weapons.Melee;

namespace Dungeon.Logic.Inventory.Items.Useable.Weapons.Melee
{
    public class Melee : Weapon
    {
        public override event Action OnUsed;

        public new MeleeData Data { get { return (MeleeData)base.Data; } }


        public override void Attack()
        {
            throw new NotImplementedException();
        }

        public override void Interact(IInteractor interactor)
        {
            throw new NotImplementedException();
        }
    }
}

using UnityEngine;

namespace Dungeon.Logic.Inventory.ScriptableObjects.Useables.Weapons.Ranged
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Items/Weapons/Ranged")]
    public class RangedData : WeaponData
    {
        public int InitialProjectileQuantity => _initialProjectileQuantity;
        public float TimeBeforeDisappear => _timeBeforeDisappear;
        public float Spread => _spread;

        [Space]
        [SerializeField, Min(0)] private int _initialProjectileQuantity = 4;
        [SerializeField, Min(0)] private float _timeBeforeDisappear = 3f;
        [SerializeField, Min(0)] private float _spread = 3f;
    }
}

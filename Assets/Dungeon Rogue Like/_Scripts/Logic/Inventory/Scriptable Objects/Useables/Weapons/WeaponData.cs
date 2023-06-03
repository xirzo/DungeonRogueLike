using UnityEngine;

namespace Dungeon.Logic.Inventory.ScriptableObjects.Useables.Weapons
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Items/Weapon")]
    public class WeaponData : UseableData
    {
        public float TimeBetweenAttacks => _timeBetweenAttacks;

        [Space]
        [SerializeField, Min(0)] private float _timeBetweenAttacks = 1.2f;

    }
}

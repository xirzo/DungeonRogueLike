using UnityEngine;

namespace Dungeon.Logic.Inventory.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Items/Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        public string Name => _name;
        public string Description => _description;

        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
    }
}

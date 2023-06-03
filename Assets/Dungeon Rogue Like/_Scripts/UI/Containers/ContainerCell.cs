using Dungeon.Logic.Inventory.Items;
using TMPro;
using UnityEngine;

namespace Dungeon.UI.Containers
{
    public class ContainerCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameField;
        public void Render(Item item)
        {
            _nameField.text = item.Data.Name;
        }
    }
}

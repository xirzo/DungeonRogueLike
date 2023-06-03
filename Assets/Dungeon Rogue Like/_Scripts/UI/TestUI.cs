using UnityEngine;
using UnityEngine.UIElements;

namespace Dungeon.UI
{
    public class TestUI : MonoBehaviour
    {
        private VisualElement _root;

        private Button _start;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _start = _root.Q<Button>("ButtonStart");

            _start.clicked += OnStartClicked;
        }

        private void OnStartClicked()
        {
            Debug.Log("Started!");
        }

        private void OnDestroy()
        {
            _start.clicked -= OnStartClicked;
        }
    }
}

using UnityEngine;
using UnityEngine.UIElements;

namespace Dungeon.UI
{
    [RequireComponent(typeof(UIElement))]
    public class UIElement : MonoBehaviour
    {
        protected UIDocument Document => _document;
        protected VisualElement Root { get; private set; }

        private UIDocument _document;

        protected virtual void Awake()
        {
            TryGetComponent(out _document);
            Root = Document.rootVisualElement;
        }
    }
}

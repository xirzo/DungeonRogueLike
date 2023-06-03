using System;
using System.Collections;
using Dungeon.Logic.Inventory.Items.Useable.Selection;
using UnityEngine;

namespace Dungeon.Logic.Inventory.Items.Useable.Presenting
{
    [RequireComponent(typeof(ISelector))]
    [DisallowMultipleComponent]
    public class Presenter : MonoBehaviour, IPresenter
    {
        public event Action OnPresented;
        public event Action OnDepresented;
        public Item CurrentlySelected => _currentlySelected;
        public Item PreviouslySelected => _previouslySelected;

        [SerializeField, Min(0)] private float _presentingSpeed = 4f;

        private Item _currentlySelected;
        private Item _previouslySelected;

        private ISelector _selector;

        public void Present(Item previousItem, Item currentItem)
        {
            _previouslySelected = previousItem;
            _currentlySelected = currentItem;


            if (_previouslySelected != null)
            {
                Depresent();
            }

            Present();
        }

        private void Present()
        {
            _currentlySelected.gameObject.SetActive(true);
            StartCoroutine(PresentCoroutine(_currentlySelected.transform.localScale, Vector3.one));

            if (_previouslySelected != null)
            {
                if (_previouslySelected.IsPooled == false)
                {
                    _previouslySelected.gameObject.SetActive(true);
                    _currentlySelected = null;
                }
            }

            OnPresented?.Invoke();

            //Debug.Log($"Presented: {_currentlySelected}");
        }

        private void Depresent()
        {
            StartCoroutine(PresentCoroutine(_currentlySelected.transform.localScale, Vector3.zero));
            _previouslySelected.gameObject.SetActive(false);

            OnDepresented?.Invoke();

            //Debug.Log($"Depresented: {_currentlySelected}");
        }


        private void Awake()
        {
            TryGetComponent(out _selector);
            _selector.OnSelected += Present;
        }

        private void OnDestroy()
        {
            _selector.OnSelected -= Present;
            StopAllCoroutines();
        }

        private IEnumerator PresentCoroutine(Vector3 startScale, Vector3 endScale)
        {
            float step = _presentingSpeed * Time.fixedDeltaTime;
            float time = 0;

            while (time <= 1.0f)
            {
                time += step;

                _currentlySelected.transform.localScale = Vector3.Lerp(startScale, endScale, time);

                yield return null;
            }

            _currentlySelected.transform.localScale = endScale;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using View.UI;

namespace View
{
    public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private bool _enableSwap;
        [SerializeField] private bool _enableDraggingTo = true;
        [SerializeField] private bool _enableDraggingFrom = true;

        private bool _isFree = true;
        private DraggebleObject _draggebleObject;
        private ItemCollectionView _parentCollection;

        public event System.Action OnCellSelected;
        public event System.Action OnCellDeselected;
        public event System.Action OnCellMouseDown;
        public event System.Action OnCellMouseUp;

        public RectTransform RectTransform { get; private set; }
        public DraggebleObject DraggebleObject => _draggebleObject;
        public bool IsFree => _isFree;
        public ItemCollectionView ParentCollection => _parentCollection;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            _isFree = true;
        }

        public void SetParentCollection(ItemCollectionView parentCollection)
        {
            _parentCollection = parentCollection;
        }

        public bool ObjectAddedCondition(DraggebleObject draggebleObject)
        {
            if (!_enableDraggingTo || !_isFree)
            {
                return false;
            }
            return true;
        }

        public bool ObjectSwapCondition(DraggebleObject draggebleObject)
        {
            if (!_isFree && _enableSwap && draggebleObject.Cell != this)
                return true;
            return false;
        }

        public void ClearCell()
        {
            if (_draggebleObject != null)
            {
                _draggebleObject.Realese();
            }
            _draggebleObject = null;
            _isFree = true;
        }

        public void EnableDragging(bool enable)
        {
            _enableDraggingFrom = enable;
            _enableDraggingTo = enable;
        }

        public void EnableDraggingTo(bool enable)
        {
            _enableDraggingTo = enable;
        }

        public void Swap(DraggebleObject draggebleObject)
        {
            draggebleObject.Cell.SetItem(_draggebleObject);
            SetItem(draggebleObject);
        }

        public void SetItem(DraggebleObject draggebleObject)
        {
            _draggebleObject = draggebleObject;
            _draggebleObject.transform.parent = transform;
            _draggebleObject.transform.localPosition = Vector3.zero;
            _draggebleObject.Cell = this;
            _isFree = false;
        }

        public void DestroyCell()
        {
            OnCellSelected = null;
            OnCellDeselected = null;
            Destroy(gameObject);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnCellMouseUp?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnCellMouseDown?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnCellSelected?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnCellDeselected?.Invoke();
        }
    }
}
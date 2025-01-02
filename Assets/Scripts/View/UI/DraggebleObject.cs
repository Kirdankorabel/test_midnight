using Model;
using Model.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class DraggebleObject : MonoBehaviour, IPointerDownHandler, IRealesed
    {
        public static event System.Action<DraggebleObject> OnRealesed;
        public static event System.Action<DraggebleObject> OnObjectSelected;

        [SerializeField] private Image _image;

        public event System.Action OnDragStarted;

        public Cell Cell { get; set; }
        public ItemModel Item { get; set; }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnObjectSelected?.Invoke(this);
            OnDragStarted?.Invoke();
        }

        public void Realese()
        {
            OnRealesed?.Invoke(this);
            OnDragStarted = null;
        }
    }
}
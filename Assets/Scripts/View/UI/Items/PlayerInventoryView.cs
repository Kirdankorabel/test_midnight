using TMPro;
using UnityEngine;

namespace View.UI
{
    public class PlayerInventoryView : ItemCollectionView, ICloseable
    {
        [Header("InventoryPanel")]
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Vector3 _leftPosition;
        [SerializeField] private Vector3 _midlePosition;

        public bool IsOpen => gameObject.active;

        public override void Construct()
        {
            _closeButton.onClick.AddListener(Close);
            base.Construct();
        }

        public void PlaceMidle()
        {
            Closer.AddCloseablle(this);
            _rect.anchoredPosition = _midlePosition;
        }

        public void PlaceLeft()
        {
            gameObject.SetActive(true);
            _rect.anchoredPosition = _leftPosition;
        }
    }
}
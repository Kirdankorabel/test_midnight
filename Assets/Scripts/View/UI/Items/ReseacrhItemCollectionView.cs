using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class ReseacrhItemCollectionView : ItemCollectionView
    {
        [SerializeField] private int _componentCellCount;
        [SerializeField] private Button _craftButton;
        [SerializeField] private Image _productImage;

        public event System.Action OnItemCreated;

        public int ComponentCellCount => _componentCellCount;

        public override void Construct()
        {
            base.Construct();
            _craftButton.onClick.AddListener(Craft);
        }

        private void Craft()
        {
            OnItemCreated?.Invoke();
        }

        public List<string> GetItemIds()
        {
            var itemIds = new List<string>();
            for (var i = 0; i < _componentCellCount; i++)
            {
                var id = _collectionController.GetItem(i).IsNullItem ? string.Empty : _collectionController.GetItem(i).ItemId;
                itemIds.Add(id);
            }
            return itemIds;
        }

        public void UpdateProductImage(Sprite sprite)
        {
            _productImage.sprite = sprite;
        }

        public void SetItems()
        {
        }
    }
}
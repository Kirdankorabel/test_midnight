using Controller.Items;
using DataContainer;
using Model.Items;
using System.Collections.Generic;
using View.UI;

namespace Controller
{
    public class ItemCollectionController : AbstractItemCollectionConroller
    {
        private ItemCollectionView _view;
        protected ItemCollectionModel _itemContainer;
        protected List<CellData> _cellData;
        protected bool _checkItem;

        public override int Size => _itemContainer.Size;
        public override ItemCollectionModel ItemCollectionModel => _itemContainer;

        public ItemCollectionController(ItemCollectionModel itemContainer)
        {
            _itemContainer = itemContainer;
            _itemContainer.OnItemAdded += OnItemAddedHeandler;
            _itemContainer.OnItemRemoved += OnItemRemovedHeandler;
        }

        public override void SetCellData(List<CellData> cellData)
        {
            _cellData = cellData;
            _checkItem = true;
        }

        public override bool ReleseItem(int cellIndex)
        {
            if (!_checkItem || _cellData[cellIndex].EnableDraggingFrom)
            {
                _itemContainer.SetItemToPosition(ItemModel.NullItem, cellIndex);
                return true;
            }
            return false;
        }

        public override void ReleseItem(ItemModel itemModel)
        {
            _itemContainer.SetItemToPosition(ItemModel.NullItem, _itemContainer.Items.IndexOf(itemModel));
        }

        public override bool TryToSetItem(ItemModel item, int cellIndex)
        {
            if (!_checkItem || (_cellData[cellIndex].EnableDraggingTo && CellChecker.CheckCell(_cellData[cellIndex], item)))
            {
                _itemContainer.SetItemToPosition(item, cellIndex);
                return true;
            }
            return false;
        }

        public override bool SwapItem(ItemModel item, int cellIndex)
        {
            if (!_checkItem || (_cellData[cellIndex].EnableDraggingTo && CellChecker.CheckCell(_cellData[cellIndex], item)))
            {
                _itemContainer.SetItemToPosition(item, cellIndex);
                return true;
            }
            return false;
        }

        public override ItemModel GetItem(int i)
        {
            return _itemContainer.GetItem(i);
        }

        public ItemModel GetItem(string itemId)
        {
            return _itemContainer.GetItem(itemId);
        }


        private void OnItemAddedHeandler((ItemModel, int) data)
        {
            if(_view != null)
            {
                _view.SetItemToCell(data.Item1.ItemId, data.Item2);
            }
        }

        private void OnItemRemovedHeandler(int cell)
        {
            if (_view != null)
            {
                _view.ReleaseItem(cell);
            }
        }

        public override void AddItem(ItemData itemData)
        {
            for (var i = 0; i < _itemContainer.Items.Count; i++)
            {
                if (_itemContainer.Items[i].IsNullItem)
                {
                    _itemContainer.SetItemToPosition(new ItemModel(itemData), i);
                    return;
                }
            }
        }

        public override void AddItem(ItemModel item)
        {
            for (var i = 0; i < _itemContainer.Items.Count; i++)
            {
                if (_itemContainer.Items[i].IsNullItem)
                {
                    _itemContainer.SetItemToPosition(item, i);
                    return;
                }
            }
        }

        public override List<ItemModel> GetItems(List<string> itemIds)
        {
            var result = new List<ItemModel>();
            ItemModel item;
            foreach (var itemId in itemIds)
            {
                if(!string.IsNullOrEmpty(itemId))
                {
                    item = GetItem(itemId);
                    if (!item.IsNullItem)
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }
    }
}
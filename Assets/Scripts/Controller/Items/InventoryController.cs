using Controller.Items;
using DataContainer;
using Model.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using View.UI;

namespace Controller
{
    public class InventoryController : AbstractItemCollectionConroller
    {
        private PlayerInventoryView _inventoryView;
        private ItemCollectionModel _itemCollectionModel;

        public override int Size => _itemCollectionModel.Size;
        public override ItemCollectionModel ItemCollectionModel => _itemCollectionModel;

        public InventoryController() { }

        public void InitializeNew()
        {
            SetItemCollection(new ItemCollectionModel("plyer inventory", 100));
           //TODO динамически расширять
        }

        public void SetLoaded(ItemCollectionModel itemCollectionModel)
        {
            SetItemCollection(itemCollectionModel);
        }

        public InventoryController SetItemCollection(ItemCollectionModel itemCollection)
        {
            _itemCollectionModel = itemCollection;
            _itemCollectionModel.OnItemAdded += OnItemAddedHeandler;
            _itemCollectionModel.OnItemRemoved += OnItemRemovedHeandler;
            UpdateInventoryView();
            return this;
        }

        public InventoryController SetView(PlayerInventoryView playerInventoryView)
        {
            _inventoryView = playerInventoryView;
            _inventoryView.OnOpened += UpdateInventoryView;
            return this;
        }

        public override void SetCellData(List<CellData> cellData)
        {
            throw new NotImplementedException();
        }

        public override bool ReleseItem(int cellIndex)
        {
            _itemCollectionModel.SetItemToPosition(ItemModel.NullItem, cellIndex);
            return true;
        }

        public override bool TryToSetItem(ItemModel item, int cellIndex)
        {
            _itemCollectionModel.SetItemToPosition(item, cellIndex);
            return true;
        }

        public override bool SwapItem(ItemModel item, int cellIndex)
        {
            _itemCollectionModel.SetItemToPosition(item, cellIndex);
            return true;
        }

        public override void ReleseItem(ItemModel item)
        {
            var position = _itemCollectionModel.Items.IndexOf(item);
            _itemCollectionModel.ReleseItem(item);
        }

        public override ItemModel GetItem(int i)
        {
            return _itemCollectionModel.GetItem(i);
        }

        public override void AddItem(ItemData itemData)
        {
            for(var i = 0; i < _itemCollectionModel.Items.Count; i++)
            {
                if (_itemCollectionModel.Items[i].IsNullItem)
                {
                    _itemCollectionModel.SetItemToPosition(new ItemModel(itemData), i);
                    return;
                }
            }
        }

        public bool CheckItems(List<string> itemIds)
        {
            var itemCollection = new List<ItemModel>();
            itemIds.ForEach(itemId =>
            {
                var addedItem = _itemCollectionModel.Items.Find(item => !item.IsNullItem && item.ItemId.Equals(itemId) && item.IsFreeItem);
                if (addedItem != null)
                {
                    addedItem.IsFreeItem = false;
                    itemCollection.Add(addedItem);
                }
                else
                {
                    itemCollection.ForEach(i => i.IsFreeItem = true);
                    itemCollection.Clear();
                    return;
                }
            });
            return itemCollection.Count == itemIds.Count;
        }

        public void OpenInventoryLeft()
        {
            _inventoryView.UpdateView(this);
            _inventoryView.PlaceLeft();
        }

        private void OnItemAddedHeandler((ItemModel, int) data)
        {
            _inventoryView.SetItemToCell(data.Item1.ItemId, data.Item2);
        }

        private void UpdateInventoryView()
        {
            _inventoryView.UpdateView(this);
            _inventoryView.PlaceMidle();
        }

        private void OnItemRemovedHeandler(int cell)
        {
            _inventoryView.ItemRemoveHeandler(cell);
        }

        public override void AddItem(ItemModel item)
        {
            for (var i = 0; i < _itemCollectionModel.Items.Count; i++)
            {
                if (_itemCollectionModel.Items[i].IsNullItem)
                {
                    _itemCollectionModel.SetItemToPosition(item, i);
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
        public ItemModel GetItem(string itemId)
        {
            return _itemCollectionModel.GetItem(itemId);
        }
    }
}
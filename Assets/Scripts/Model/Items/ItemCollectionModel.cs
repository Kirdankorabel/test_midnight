using Core;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Model.Items
{
    [System.Serializable]
    public class ItemCollectionModel
    {
        [SerializeField] private List<ItemModel> items;
        [SerializeField] private string id;

        public List<ItemModel> Items => items;
        public int Size => items.Count;
        public string Id => id;

        public event System.Action<(ItemModel, int)> OnItemAdded;
        public event System.Action<int> OnItemRemoved;

        public ItemCollectionModel() { }

        public ItemCollectionModel(string id, int count)
        {
            items = new List<ItemModel>();
            for (int i = 0; i < count; i++)
            {
                items.Add(ItemModel.NullItem);
            }
            this.id = id;
        }

        public void SetItemToPosition(ItemModel item, int position)
        {
            items[position] = item;
            if (!item.IsNullItem)
            {
                GameLog.AddMassage($"Item: {item.ItemId} - added to collection: {id}");
                OnItemAdded?.Invoke((item, position));
            }
            else
            {
                GameLog.AddMassage($"Item was destroyed - collection: {id}, cell: {position}");
                OnItemRemoved?.Invoke(position);
            }
        }

        public ItemModel GetItem(int index)
        {
            return items[index];
        }

        public ItemModel ReleseItem(ItemModel item)
        {
            for (var i = 0; i < items.Count; ++i)
            {
                if (item.Equals(items[i]))
                {
                    SetItemToPosition(ItemModel.NullItem, i);
                }
            }
            return item;
        }

        public void ReleseItem(int position)
        {
            SetItemToPosition(ItemModel.NullItem, position);
        }

        public ItemModel GetItem(string itemId)
        {
            var item = items.Find(i => !i.IsNullItem && i.ItemId.Equals(itemId));
            if(item == null)
            {
                item = ItemModel.NullItem;
                GameLog.AddMassage($"Item: {itemId} - not found in collection: {id}");
            }
            return item;
        }
    }
}
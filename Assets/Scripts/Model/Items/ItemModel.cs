using DataContainer;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Items
{
    [System.Serializable]
    public class ItemModel
    {
        public static ItemModel NullItem => new ItemModel(true);

        [SerializeField] private float creationTime;
        [SerializeField] private ItemData data;
        [SerializeField] private List<string> components;
        [SerializeField] private bool isNullItem;
        [SerializeField] private bool isFreeItem = true;

        public ItemData Data => data;
        public string ItemId => data.ItemId;
        public bool IsNullItem => isNullItem;
        public float CreationTime => creationTime;
        public bool IsFreeItem
        {
            get => isFreeItem;
            set => isFreeItem = value;
        }


        public ItemModel(ItemData data)
        {
            this.data = data;
            isNullItem = false;
        }

        private ItemModel(bool isNull)
        {
            isNullItem = isNull;
            data = new ItemData();
        }

        public ItemModel SetComponents(List<string> components)
        {
            this.components = components;
            return this;
        }
    }
}
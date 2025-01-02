using System.Collections.Generic;
using UnityEngine;

namespace DataContainer
{
    [System.Serializable]
    public class ItemData: DataItem
    {
        [SerializeField] private int defaultPrise;
        [SerializeField] private Sprite sprite;
        [SerializeField] private ItemType type;
        [SerializeField] private List<ItemTag> tags;

        public int DefaultPrise => defaultPrise;
        public Sprite Sprite => sprite;
        public ItemType ItemType => type;
        public List<ItemTag> Tags => tags;

        public ItemData() { }
    }
}
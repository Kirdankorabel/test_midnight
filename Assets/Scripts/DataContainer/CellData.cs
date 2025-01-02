using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataContainer
{
    [System.Serializable]
    public class CellData
    {
        [SerializeField] private List<ItemTag> targetTags;
        [SerializeField] private List<ItemType> targetTypes;
        [SerializeField] private bool enableDraggingTo = true;
        [SerializeField] private bool enableDraggingFrom = true;

        public List<ItemTag> TargetTags => targetTags;
        public List<ItemType> TargetTypes => targetTypes;
        public bool EnableDraggingTo => enableDraggingTo;
        public bool EnableDraggingFrom => enableDraggingFrom;
    }
}

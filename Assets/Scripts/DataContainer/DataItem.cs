using UnityEngine;

namespace DataContainer
{
    public class DataItem
    {
        [SerializeField] private string _itemId;
        public string ItemId => _itemId;
    }
}
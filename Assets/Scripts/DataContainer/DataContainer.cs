using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataContainer
{
    public class DataContainer<T> : ScriptableObject where T : DataItem
    {
        [SerializeField] private List<T> _data;

        public List<T> Data => _data;

        public int ItemCount => _data.Count;

        public T GetItem(int index)
        {
            return _data[index];
        }

        public T GetItem(string itemId)
        {
            return _data.Find(item => item.ItemId.Equals(itemId));
        }

        public bool ContainsItem(string itemId) 
        {
            return _data.Any(item => item.ItemId.Equals(itemId));
        }
    }
}
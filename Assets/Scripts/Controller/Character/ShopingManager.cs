using DataContainer;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class ShopingManager
    {
        private ItemDataContainer _dataContainer;

        public ShopingManager(ItemDataContainer itemDataContainer)
        {
            _dataContainer = itemDataContainer;
        }

        public List<string> GetRandomItems(int count)
        {
            var result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                result.Add(_dataContainer.GetItem(Random.Range(0, _dataContainer.ItemCount)).ItemId);
            }
            return result;
        }
    }
}
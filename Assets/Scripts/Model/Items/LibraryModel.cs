using DataContainer;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class LibraryModel
    {
        [SerializeField] private List<ItemData> customItems;

        public List<ItemData> CustomItems => customItems;

        public LibraryModel() { }

        public void Initialize()
        {
            customItems = new List<ItemData>();
        }
    }
}
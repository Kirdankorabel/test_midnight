using System.Collections.Generic;
using UnityEngine;

namespace DataContainer
{
    [System.Serializable]
    public class RecipeData : DataItem
    {
        [SerializeField] private string productId;
        [SerializeField] private List<string> components;
        [SerializeField] private string descriptionId;

        public string DescriptionId => descriptionId;
        public string ProductId => productId;
        public List<string> Components => components;
    }
}
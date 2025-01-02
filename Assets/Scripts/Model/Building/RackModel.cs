using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class RackModel
    {
        [SerializeField] private string id;
        [SerializeField] private List<RackCell> potionModelList;
        [SerializeField] private List<string> _targetItems;

        public List<RackCell> Potions => potionModelList;
        public List <string> TargetItems => _targetItems;

        public RackModel(string id, int cellCount)
        {
            this.id = id;
            potionModelList = new List<RackCell>();
            for (int i = 0; i < cellCount; i++)
            {
                potionModelList.Add(new RackCell());
            }
        }

        public RackModel SetTargetItems(List<string> targetItems)
        {
            _targetItems = targetItems;
            return this;
        }
    }
}
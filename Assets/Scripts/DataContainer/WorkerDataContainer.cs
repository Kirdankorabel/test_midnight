using System.Collections.Generic;
using UnityEngine;

namespace DataContainer
{
    [CreateAssetMenu(fileName = "WorkerDataContainer", menuName = "ScriptableObjects/DataContainer/WorkerDataContainer", order = 1)]
    public class WorkerDataContainer : ScriptableObject
    {
        [SerializeField] private int _maxLevel;
        [SerializeField] private List<int> _expForLevel;
        [SerializeField] private List<int> _priceForLevel;
        [SerializeField] private List<float> _timeMultipler;
        [SerializeField] private List<string> _names;
        [SerializeField] private List<Sprite> _icons;
        [SerializeField] private List<GameObject> _prefabs;

        public int MaxLevel => _maxLevel;
        public List<int> ExpForLevel => _expForLevel;
        public List<int> PriceFoLevel => _priceForLevel;
        public List<float> TimeMultipler => _timeMultipler;
        public List<string> Names => _names;
        public List <GameObject> Prefabs => _prefabs;
        public List<Sprite> Icons => _icons;
    }
}
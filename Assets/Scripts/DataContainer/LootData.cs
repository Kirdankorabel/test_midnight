using UnityEngine;

namespace DataContainer
{
    [System.Serializable]
    public class LootData : DataItem
    {
        [SerializeField] private float _chance;

        public float Chance => _chance;
    }
}
using UnityEngine;

namespace DataContainer
{
    [CreateAssetMenu(fileName = "LootDataContainer", menuName = "ScriptableObjects/DataContainer/LootDataContainer", order = 1)]
    public class LootDataContainer : DataContainer<LootData>
    {
    }
}
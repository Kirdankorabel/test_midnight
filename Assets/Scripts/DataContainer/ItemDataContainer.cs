using UnityEngine;

namespace DataContainer
{
    [CreateAssetMenu(fileName = "ItemDataContainer", menuName = "ScriptableObjects/DataContainer/ItemDataContainer", order = 1)]
    public class ItemDataContainer : DataContainer<ItemData>
    {
    }
}
using UnityEngine;

namespace DataContainer
{
    [CreateAssetMenu(fileName = "RoomDataContainer", menuName = "ScriptableObjects/DataContainer/RoomDataContainer", order = 1)]
    public class RoomDataContainer : DataContainer<RoomData>
    {
    }
}
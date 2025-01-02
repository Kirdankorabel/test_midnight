using UnityEngine;

namespace DataContainer
{
    [System.Serializable ]
    public class RoomData : DataItem
    {
        [SerializeField] private int _prise;
        [SerializeField] private RoomType _type;
        [SerializeField] private bool _isOpen;
        [SerializeField] private int _positionCount;

        public int Price => _prise;
        public RoomType Type => _type;
        public bool IsOpen => _isOpen;
        public int PositionCount => _positionCount;
    }
}
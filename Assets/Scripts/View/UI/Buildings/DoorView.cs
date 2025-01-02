using UnityEngine;

namespace View.Game
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] private Vector3 _openPosition;
        [SerializeField] private Vector3 _closePosition;
        [SerializeField] private Vector3 _openRotation;
        [SerializeField] private Vector3 _closeRotation;
        [SerializeField] private Transform _doorTransform;
        [SerializeField] private int _rooms = 1;
        
        int _roomCount = 0;

        public void Open()
        {
            _roomCount++;
            if(_roomCount >= _rooms)
            {
                _doorTransform.localPosition = _openPosition;
                _doorTransform.localEulerAngles = _openRotation;
            }
        }

        public void Close()
        {
            _doorTransform.localPosition = _closeRotation;
            _doorTransform.localEulerAngles = _closePosition;
        }
    }
}
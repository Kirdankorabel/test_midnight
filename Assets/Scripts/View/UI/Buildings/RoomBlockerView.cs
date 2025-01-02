using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View.Game
{
    public class RoomBlockerView : MonoBehaviour
    {
        [SerializeField] private List<DoorView> _doorViews;

        public event System.Action OnClick;
        public event System.Action OnRoomOpened;

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            OnClick?.Invoke();
        }

        public void OpenRoom()
        {
            _doorViews.ForEach(doorView => doorView.Open());
            gameObject.SetActive(false); 
            OnRoomOpened?.Invoke();
        }
    }
}
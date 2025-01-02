using Controller.Building;
using System.Collections.Generic;
using UnityEngine;

namespace View.Game
{
    public class WorkshopView : MonoBehaviour
    {
        [SerializeField] private List<RoomView> _rooms;

        public List<RoomView> Rooms => _rooms;

        public void Construct()
        {
            _rooms.ForEach(room => AddRoom(room));
        }

        private void AddRoom(RoomView room)
        {
            room.Construct();
        }
    }
}
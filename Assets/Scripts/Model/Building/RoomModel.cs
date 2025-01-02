using DataContainer;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Building
{
    [System.Serializable]
    public class RoomModel
    {
        [SerializeField] private string id;
        [SerializeField] private bool isOpen;
        [SerializeField] private List<PlaceableObjectModel> buildings;
        [SerializeField] private RoomType roomType;
        [SerializeField] private int price;

        public event System.Action OnBuildingPlacead;

        public string Id => id;
        public bool IsOpen => isOpen;
        public List<PlaceableObjectModel> Buildings => buildings;
        public RoomType RoomType => roomType;
        public int Price => price;

        public RoomModel() { }

        public RoomModel(RoomData roomData)
        {
            roomType = roomData.Type;
            id = roomData.ItemId;
            roomType = roomData.Type;
            price = roomData.Price;
            isOpen = roomData.IsOpen;

            buildings = new List<PlaceableObjectModel>();
            for(int i = 0; i < roomData.PositionCount; i++)
            {
                buildings.Add(null);
            }
        }

        public void SetBuilding(PlaceableObjectModel building, int position)
        {
            buildings[position] = building;
            OnBuildingPlacead?.Invoke();
        }

        public void Open(bool isOpen)
        {
            this.isOpen = isOpen;
        }
    }
}
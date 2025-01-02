using Controller.Characters;
using DataContainer;
using System.Collections.Generic;
using UnityEngine;
using View;
using View.Game;
using View.UI;

namespace Controller.Building
{
    public class WorkshopController : ICloseable
    {
        private RoomController _selectedRoom;
        private List<RoomController> _rooms = new List<RoomController>();
        private WorkersController _workerManager;
        private InventoryController _inventoryController;
        private RoomOpener _roomOpener;

        private BuildingPanel _buildingPanel;
        private WorkshopView _view;
        private QuestionPanel _questionPanel;
        private GameLibrary _gameLibrary;
        private PlaceableObjectData _selectedBuilding;

        public event System.Action<bool> OnBuildingModeEnabled;

        public bool WorkshopIsOpen { get; private set; }
        public bool BuildingMode {get;private set;}
        public WorkersController WorkersController => _workerManager;
        public List<RoomController> RoomControllers => _rooms;
        public bool IsOpen => BuildingMode;

        public void Construct()
        {
            _inventoryController = GameContext.DIContainer.Resolve<InventoryController>();
            _gameLibrary = GameContext.DIContainer.Resolve<GameLibrary>();
            _view.Construct();
        }

        public WorkshopController SetRoomOpener(RoomOpener roomOpener)
        {
            _roomOpener = roomOpener;
            _roomOpener.SetRooms(_rooms);
            return this;
        }

        public WorkshopController SetWorkersController(WorkersController workerManager)
        {
            _workerManager = workerManager;
            return this;
        }

        public WorkshopController SetView(WorkshopView workshopView)
        {
            _view = workshopView;
            workshopView.Rooms.ForEach(room => AddRoomController(room));
            return this;
        }

        public WorkshopController SetQuestionPanel(QuestionPanel questionPanel)
        {
            _questionPanel = questionPanel;
            _questionPanel.OnAccepted += PlaceBuilding;
            return this;
        }

        public WorkshopController SetBuildingPanel(BuildingPanel buildingPanel)
        {
            _buildingPanel = buildingPanel;
            _buildingPanel.OnBuildingSelected += SelectBuilding;
            return this;
        }

        public void ChangeBuildingMode()
        {
            BuildingMode = !BuildingMode;
            if(BuildingMode)
            {
                Closer.AddCloseablle(this);
            }
            _rooms.ForEach(r => r.OpenBuildingMode(BuildingMode));
        }

        public void AddRoomController(RoomView room)
        {
            var roomController = new RoomController();
            roomController.SetView(room);
            _rooms.Add(roomController);
            roomController.OnPlacedSelected += (index) => RoomSelectHeandler(roomController, index);
        }

        public void ChangeDoorState()
        {
            WorkshopIsOpen = !WorkshopIsOpen;
        }

        public PlaceableObjectController GetBuilding(string buildingId)
        {
            PlaceableObjectController placeableObjectController;
            for(var i = 0; i < _rooms.Count; i++)
            {
                placeableObjectController = _rooms[i].GetBuildingController(buildingId);
                if(placeableObjectController != null)
                {
                    return placeableObjectController;
                }
            }
            return null;
        }

        public int GetMaxWorkersCount()
        {
            return 3 * _rooms.FindAll(r => r.RoomType == RoomType.workshop && r.IsOpen).Count;
        }

        public int GetMaxVisitorsCount()
        {
            return 3 * _rooms.FindAll(r => r.RoomType == RoomType.store && r.IsOpen).Count;
        }

        private void RoomSelectHeandler(RoomController roomController, int position)
        {
            _selectedRoom = roomController;
            _buildingPanel.Open(_selectedRoom.RoomView.GetBuildingPlace(position).PlaceableObjectTypes);
        }

        private void SelectBuilding(PlaceableObjectData placeableObjectData)
        {
            var money = GameContext.DIContainer.Resolve<AccountController>().Balance;
            _selectedBuilding = placeableObjectData;
            _questionPanel.Open($"Place: {placeableObjectData.PlaceableObjectType} for {placeableObjectData.Price}", 
                                money >= placeableObjectData.Price);
        }

        private void PlaceBuilding()
        {
            _selectedRoom.PlaceBuilding(_selectedBuilding);
        }

        public void Close()
        {
            if(BuildingMode)
            {
                ChangeBuildingMode();
            }
        }
    }
}
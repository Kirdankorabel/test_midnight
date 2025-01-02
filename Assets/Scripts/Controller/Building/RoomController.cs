using DataContainer;
using Model.Building;
using System.Collections.Generic;
using View.Game;

namespace Controller.Building
{
    public class RoomController
    {
        private int _position = -1;
        private RoomModel _roomModel;
        private RoomView _roomView;
        private AccountController _accountController;
        private List<PlaceableObjectController> _buildingControllers;

        public event System.Action<int> OnBuildingPlaced;
        public event System.Action<int> OnPlacedSelected;
        public event System.Action<RoomController> OnRoomOpened;

        public RoomView RoomView => _roomView;
        public string RoomId => _roomModel.Id;
        public bool IsOpen => _roomModel.IsOpen;
        public RoomType RoomType => _roomModel.RoomType;
        public int Price => _roomModel.Price;

        public RoomController() { }

        public void InitiazileNew(RoomData roomData)
        {
            _roomModel = new RoomModel(roomData);
            _buildingControllers = new List<PlaceableObjectController>();
            _accountController = GameContext.DIContainer.Resolve<AccountController>();
        }

        public void SetLoaded(RoomModel roomModel)
        {
            _roomModel = roomModel;
            if(_buildingControllers != null)
            {
                _buildingControllers.ForEach(controller => controller.Destroy());
            }
            _buildingControllers = new List<PlaceableObjectController>();
            _accountController = GameContext.DIContainer.Resolve<AccountController>();
            for (var i = 0; i < roomModel.Buildings.Count; i++)
            {
                PlaceLoaded(roomModel.Buildings[i]);
            }
            if (_roomView.RoomBlocker != null && _roomModel.IsOpen)
            {
                _roomView.RoomBlocker.OpenRoom();
            }
        }

        public RoomModel GetDataToSave()
        {
            _buildingControllers.ForEach(controller => controller.SaveData());
            return _roomModel;
        }

        public void SetView(RoomView roomView)
        {
            _roomView = roomView;
            _roomView.OnPlaceSelected += SelectPlace;
            if(_roomView.RoomBlocker != null)
            {
                _roomView.RoomBlocker.OnClick += () => OnRoomOpened?.Invoke(this);
            }
        }

        public void PlaceBuilding(PlaceableObjectData placeableObjectData)
        {
            if(_position < 0 || _accountController.Balance < placeableObjectData.Price)
            {
                return;
            }
            _accountController.ChangeBalance(-placeableObjectData.Price);
            var buildingModel = new PlaceableObjectModel($"{_roomView.name}.{_position}")
                .SetType(placeableObjectData.ItemId)
                .SetPosition(_position)
                .SetItemCollection(new Model.Items.ItemCollectionModel(placeableObjectData.ItemId, placeableObjectData.CollectionSize));
            _roomModel.SetBuilding(buildingModel, _position);
            var controller = BuildingControllerFactory.CreateController(buildingModel).SetModel(buildingModel);
            controller.Construct();
            _buildingControllers.Add(controller);
            _roomView.PlaceBuilding(controller, _position);
            _position = -1;
        }

        public void SelectPlace(int index)
        {
            _position = index;
            OnPlacedSelected?.Invoke(index);
        }

        public PlaceableObjectController GetBuildingController(string buidingId)
        {
            return _buildingControllers.Find(controller => controller.PlObjectId.Equals(buidingId));
        }

        public void OpenBuildingMode(bool value)
        {
            if (_roomModel.IsOpen)
            {
                _roomView.Enable(value);
            }
        }

        public void OpenRoom()
        {
            _roomModel.Open(true);
            _roomView.RoomBlocker.OpenRoom();
        }

        private void PlaceLoaded(PlaceableObjectModel buildingModel)
        {            
            if(string.IsNullOrEmpty(buildingModel.Id))
            {
                return;
            }
            _roomModel.SetBuilding(buildingModel, buildingModel.Positiion);
            var controller = BuildingControllerFactory.CreateController(buildingModel).SetModel(buildingModel);
            controller.Construct();
            _buildingControllers.Add(controller);
            _roomView.PlaceBuilding(controller, buildingModel.Positiion);
        }
    }
}
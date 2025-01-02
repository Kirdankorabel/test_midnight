using Controller.Building;
using System.Collections.Generic;
using View.UI;

namespace Controller
{
    public class RoomOpener
    {
        private AccountController _accountController;
        private List<RoomController> _roomControllers;
        private QuestionPanel _roomOpenerView;
        private RoomController _roomController;
        private WorkshopController _workshopController;

        public RoomOpener(QuestionPanel roomOpenerView, int roomPrice)
        {
            _roomOpenerView = roomOpenerView;
            _accountController = GameContext.DIContainer.Resolve<AccountController>();
            _roomOpenerView.OnAccepted += OpenRoom;
        }

        public RoomOpener SetRooms(List<RoomController> rooms)
        {
            _roomControllers = rooms;
            _roomControllers.ForEach(roomController => roomController.OnRoomOpened += OpenRoomOpenerView);
            return this;
        }

        private void OpenRoomOpenerView(RoomController roomController)
        {
            _workshopController = _workshopController == null ? GameContext.DIContainer.Resolve<WorkshopController>() : _workshopController;

            if (_workshopController.BuildingMode)
            {
                _roomController = roomController;
                _roomOpenerView.Open(roomController.RoomId, _accountController.Balance >= _roomController.Price);
            }
        }

        private void OpenRoom()
        {
            if(_accountController.Balance >= _roomController.Price)
            {
                _roomController.OpenRoom();
                _accountController.ChangeBalance(-_roomController.Price);
            }
        }
    }
}
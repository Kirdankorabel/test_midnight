using Controller.Building;
using Controller.Characters;
using DI;
using UnityEngine;
using View;
using View.Game;
using View.UI;

public class WorkshopControllerInstaller : Installer
{
    [SerializeField] private WorkshopView _workshopView;
    [SerializeField] private CharacterFactory _characterFactory;
    [SerializeField] private WorkersView _workersView;
    [SerializeField] private BuildingPanel _buildingPanel;
    [SerializeField] private QuestionPanel _roomOpenerView;
    [SerializeField] private QuestionPanel _buildingPlaceQuestionPanel;

    public override void Install(DIContainer dIContainer)
    {
        var workersController = new WorkersController()
            .SetFactory(_characterFactory)
            .SetView(_workersView);

        var controller = new WorkshopController()
            .SetView(_workshopView)
            .SetWorkersController(workersController)
            .SetBuildingPanel(_buildingPanel)
            .SetQuestionPanel(_buildingPlaceQuestionPanel)
            .SetRoomOpener(new Controller.RoomOpener(_roomOpenerView, 300));
        dIContainer.RegisterInstance(controller);
    }
}

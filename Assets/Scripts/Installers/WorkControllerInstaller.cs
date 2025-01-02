using Controller.Building;
using Controller.Characters;
using DataContainer;
using DI;
using UnityEngine;
using View.UI;

public class WorkControllerInstaller : Installer
{
    [SerializeField] private WorkView _view;
    [SerializeField] private LootPanel _panel;
    [SerializeField] private LootDataContainer _dataContainer;

    public override void Install(DIContainer dIContainer)
    {
        var controller = new WorkController()
            .SetView(_view)
            .SetLootController(new LootController(_panel, _dataContainer));

        dIContainer.RegisterInstance(controller);
    }
}

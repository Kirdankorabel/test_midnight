using Controller;
using DI;
using UnityEngine;
using View.UI;

public class InventoryControllerInstaller : Installer
{
    [SerializeField] private PlayerInventoryView _inventoryView;

    public override void Install(DIContainer dIContainer)
    {
        var inventory = new InventoryController().SetView(_inventoryView);
        dIContainer.RegisterInstance(inventory);
    }
}

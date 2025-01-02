using Controller.Characters;
using DataContainer;
using DI;
using UnityEngine;

public class ShopingManagerInstaller : Installer
{
    [SerializeField] private ItemDataContainer _dataContainer;

    public override void Install(DIContainer dIContainer)
    {
        dIContainer.RegisterInstance(new ShopingManager(_dataContainer));
    }
}

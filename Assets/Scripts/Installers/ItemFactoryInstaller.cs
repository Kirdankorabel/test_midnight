using DI;
using UnityEngine;
using View;

public class ItemFactoryInstaller : Installer
{
    [SerializeField] private ItemFactory itemFactory;

    public override void Install(DIContainer dIContainer)
    {
        dIContainer.RegisterInstance(itemFactory);
    }
}

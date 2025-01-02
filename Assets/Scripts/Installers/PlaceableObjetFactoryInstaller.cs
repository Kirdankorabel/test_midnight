using DI;
using UnityEngine;
using View.Factory;

public class PlaceableObjetFactoryInstaller : Installer
{
    [SerializeField] private PlaceableObjectFactory _instance;

    public override void Install(DIContainer dIContainer)
    {
        dIContainer.RegisterInstance(_instance);
    }
}

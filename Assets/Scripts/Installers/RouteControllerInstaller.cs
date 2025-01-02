using Controller.Characters;
using DI;
using System.Collections.Generic;
using UnityEngine;

public class RouteControllerInstaller : Installer
{
    public override void Install(DIContainer dIContainer)
    {
        dIContainer.RegisterInstance(new RouteController());
    }
}

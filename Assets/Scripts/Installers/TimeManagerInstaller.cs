using DI;
using UnityEngine;
using View;

public class TimeManagerInstaller : Installer
{
    [SerializeField] private TimeManager _timeManager;

    public override void Install(DIContainer dIContainer)
    {
        dIContainer.RegisterInstance(_timeManager);
    }
}

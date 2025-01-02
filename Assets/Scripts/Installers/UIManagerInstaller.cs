using DI;
using UnityEngine;
using View.UI;

public class UIManagerInstaller : Installer
{
    [SerializeField] private UIManager _uIManager;
    public override void Install(DIContainer dIContainer)
    {
        dIContainer.RegisterInstance(_uIManager);
    }
}

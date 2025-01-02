using Controller.Characters;
using DI;
using UnityEngine;
using View;

public class VisitorControllerInstaller : Installer
{
    [SerializeField] private CharacterFactory _visitorFactory;
    [SerializeField] private Vector3 _position;

    public override void Install(DIContainer dIContainer)
    {
        var controller = new VisitController();
        controller.SetFactory(_visitorFactory, _position);
        dIContainer.RegisterInstance(controller);
    }
}

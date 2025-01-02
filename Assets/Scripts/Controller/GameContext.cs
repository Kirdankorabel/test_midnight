using DI;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public static DIContainer DIContainer;

    [SerializeField] private List<Installer> _installers;

    public void LoadContext()
    {
        DIContainer = new DIContainer();
        _installers.ForEach(installer => installer.Install(DIContainer));
    }
}

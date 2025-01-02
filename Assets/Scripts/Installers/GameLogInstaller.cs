using Core;
using DI;
using UnityEngine;
using View.UI;

public class GameLogInstaller : Installer
{
    [SerializeField] private GameLogView _gameLogView;

    public override void Install(DIContainer dIContainer)
    {
        var gameLog = new GameLog();
        GameLog.SetGameLogView(_gameLogView);
    }
}

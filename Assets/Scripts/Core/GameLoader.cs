using Controller;
using Controller.Building;
using Controller.Characters;
using Core;
using DataContainer;
using Model.Building;
using SaveManagment;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;
using View.Game.Characters;
using View.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private string _autosavename = "Autosave";
    [SerializeField] private bool _load = true;
    [SerializeField] private bool _save = true;
    [SerializeField] private List<RoutePointRegistrator> _routePointRegistrators;
    [SerializeField] private RoomDataContainer _roomData;
    [SerializeField] private SavePanel _savePanel;
    [SerializeField] private LoadPanel _loadPanel;

    private WorkshopController _workshopController;
    private InventoryController _inventoryController;
    private WorkController _workController;
    private AccountController _accountController;
    private VisitController _visitController;
    private TimeManager _timeManager;
    
    public void Initialize()
    {
        _workshopController = GameContext.DIContainer.Resolve<WorkshopController>();
        _inventoryController = GameContext.DIContainer.Resolve<InventoryController>();
        _workController = GameContext.DIContainer.Resolve<WorkController>();
        _accountController = GameContext.DIContainer. Resolve<AccountController>();
        _visitController = GameContext.DIContainer.Resolve<VisitController>();
        _timeManager = GameContext.DIContainer.Resolve<TimeManager>();

        _routePointRegistrators.ForEach(r => r.RegistratePoints());
        _savePanel.OnSaved += (saveName) => MyLoader.Save(GetgameData(), saveName);
        _loadPanel.OnGameSelected += LoadGame;
        _workshopController.Construct();

        if (Game.GameData != null)
        {
            SetLoadedGame(Game.GameData);
        }
        else
        {
            InitializeNew();
        }
    }

    private void SetLoadedGame(GameData gameData)
    {
        _inventoryController.SetLoaded(gameData.playerInventory);
        for(int i = 0; i < gameData.rooms.Count; i++)
        {
            _workshopController.RoomControllers[i].SetLoaded(gameData.rooms[i]);
        }
        _workController.SetLoaded(gameData.work);
        _workshopController.WorkersController.SetLoaded(gameData.workers);
        _accountController.SetLoaded(gameData.account);
        _visitController.SetLoaded(gameData.visitors);
        _timeManager.SetData(gameData.time);
    }

    private void InitializeNew()
    {
        _inventoryController.InitializeNew();
        for(var i = 0; i < _roomData.ItemCount; i++)
        {
            _workshopController.RoomControllers[i].InitiazileNew(_roomData.GetItem(i));
        }
        _workshopController.WorkersController.InitializeNew();
        _workController.InitializeNew();
        _accountController.InitializeNew(10000);
        _timeManager.InitializeNew();
    }

    private GameData GetgameData()
    {
        GameData gameData = new GameData();
        gameData.playerInventory = _inventoryController.ItemCollectionModel;
        gameData.rooms = new List<RoomModel>();
        for (int i = 0; i < _workshopController.RoomControllers.Count; i++)
        {
            gameData.rooms.Add(_workshopController.RoomControllers[i].GetDataToSave());
        }
        gameData.workers = _workshopController.WorkersController.GetWorkersModel();
        gameData.work =  _workController.Model;
        gameData.account = _accountController.Model;
        gameData.visitors = _visitController.GetVisitors();
        gameData.time = _timeManager.Time;
        gameData.dateTime = DateTime.Now.ToString();

        return gameData;
    }

    private void OnApplicationQuit()
    {
        if(_save)
        {
            MyLoader.Save(GetgameData(), _autosavename);
        }
    }

    private void LoadGame(GameData gameData)
    {//TODO сделать без перезагрузки
        Game.GameData = gameData;
        SceneManager.LoadScene("GameScene");
    }
}

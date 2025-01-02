using System.Collections.Generic;
using UnityEngine;
using View.UI;

public class SceneEntryPoint : MonoBehaviour 
{
    [SerializeField] private GameContext _gameContext;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private GameLoader _gameLoader;
    [SerializeField] private List<ConstructableMonoBehaviour> _factories;

    private void Awake()
    {
        _gameContext.LoadContext();
        _factories.ForEach(factory => factory.Construct());
        _UIManager.InitializeUI();

        _gameLoader.Initialize();
    }
}

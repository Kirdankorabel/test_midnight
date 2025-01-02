using SaveManagment;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View.UI;

namespace View.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        [Header("Panels")]
        [SerializeField] private LoadPanel _loadOanel;

        [SerializeField] private string _gameSceneName = "GameScene";

        private void Awake()
        {
            _loadOanel.Construct();

            Core.Game.GameDataList = MyLoader.LoadAll();
            Core.Game.GameDataList.Sort((x, y) => y.GetDateTime.CompareTo(x.GetDateTime));


            _loadButton.onClick.AddListener(OpenLoadPanel);
            _continueButton.onClick.AddListener(ContinueGame);
            _newGameButton.onClick.AddListener(StartNewGame);
            _loadOanel.OnGameSelected += LoadGame;

            _continueButton.interactable = Core.Game.GameDataList.Count > 0;
            _loadButton.interactable = Core.Game.GameDataList.Count > 0;
        }

        private void OpenLoadPanel()
        {
            _loadOanel.SetData(Core.Game.GameDataList);
            _loadOanel.Open();
        }

        private void ContinueGame()
        {
            Core.Game.GameData = Core.Game.GameDataList[0];
            //Core.Game.GameDataList.Clear();
            SceneManager.LoadScene(_gameSceneName);
        }

        private void StartNewGame()
        {
            //Core.Game.GameDataList.Clear();
            SceneManager.LoadScene(_gameSceneName);
        }

        private void LoadGame(GameData gameData)
        {
            Core.Game.GameData = gameData;
            //Core.Game.GameDataList.Clear();
            SceneManager.LoadScene(_gameSceneName);
        }
    }
}
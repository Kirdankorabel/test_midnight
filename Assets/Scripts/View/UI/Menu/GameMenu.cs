using SaveManagment;
using UnityEngine;
using DI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace View.UI
{
    public class GameMenu : ConstructableMonoBehaviour, ICloseable
    {
        [Header("Buttons")]
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _continueGameButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _quitButton;
        [Header("Panels")]
        [SerializeField] private SavePanel _savePanel;
        [SerializeField] private LoadPanel _loadPanel;
        [SerializeField] private string _startSceneName = "StartScene";
        
        bool ICloseable.IsOpen => gameObject.active;

        public override void Construct()
        {
            _savePanel.Construct();
            _loadPanel.Construct();

            _openButton.onClick.AddListener(Open);
            _continueGameButton.onClick.AddListener(Close);
            _quitButton.onClick.AddListener(Application.Quit);
            _saveButton.onClick.AddListener(OpenSavePanel);
            _loadButton.onClick.AddListener(OpenLoadPanel);
            _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(_startSceneName));
        }

        private void Open()
        {
            Closer.AddCloseablle(this);
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject?.SetActive(false);
        }

        private void OpenLoadPanel()
        {
            Core.Game.GameDataList = MyLoader.LoadAll();
            _loadPanel.SetData(Core.Game.GameDataList);
            _loadPanel.Open();
        }

        private void OpenSavePanel()
        {
            Core.Game.GameDataList = MyLoader.LoadAll();
            _savePanel.SetData(Core.Game.GameDataList);
            _savePanel.Open();
        }
    }
}
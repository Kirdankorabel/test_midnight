using SaveManagment;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DI;

namespace View.UI
{
    public class LoadPanel : ScrollPanel<GameData>, ICloseable
    {
        [SerializeField] private SaveView _saveView;
        [SerializeField] private Button _loadButton;

        private List<SaveView> _saveViews = new List<SaveView>();
        private GameData _gameData;

        public event System.Action<GameData> OnGameSelected;

        bool ICloseable.IsOpen => gameObject.active;

        void Start()
        {
            SaveView.OnSelected += SetGameData;
            _loadButton.onClick.AddListener(Load);
        }

        public override void Open()
        {
            _gameData = _data[0];
            base.Open();
        }

        private void SetGameData(GameData data)
        {
            _gameData = data;
            _saveView.SetData(data);
        }

        private void Load()
        {
            OnGameSelected?.Invoke(_gameData);
            gameObject.SetActive(false);
        }
    }
}
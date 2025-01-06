using SaveManagment;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DI;

namespace View.UI
{
    public class SavePanel : ScrollPanel<GameData>, ICloseable
    {
        [SerializeField] private TMP_InputField _saveNameInput;//regex
        [SerializeField] private Button _saveButton;
        [SerializeField] private QuestionPanel _questionPanel;

        public event System.Action<string> OnSaved;

        public override void Construct()
        {
            base.Construct();
            _questionPanel.OnAccepted += Save;
            _saveButton.onClick.AddListener(TryToSave);
            _saveNameInput.onValueChanged.AddListener(OnNameChangeHeandler);
        }

        public override void Open()
        {
            _data.Sort((x, y) => y.dateTime.CompareTo(x.dateTime));
            base.Open();
        }

        private void OnNameChangeHeandler(string name)
        {
            _saveButton.interactable = !string.IsNullOrEmpty(name);
        }

        private void TryToSave()
        {
            if (_data.Any(data => data.name.Equals(_saveNameInput.text)))
            {
                _questionPanel.Open($"A save with the name {_saveNameInput.text} exists. Save anyway?", true);
            }
            else
            {
                Save();
            }
        }

        private void Save()
        {
            OnSaved?.Invoke(_saveNameInput.text);
            _saveNameInput.text = string.Empty;
            gameObject.SetActive(false);
        }
    }
}
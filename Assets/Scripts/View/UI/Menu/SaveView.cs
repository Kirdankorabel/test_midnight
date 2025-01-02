using SaveManagment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.UI;

namespace View.UI
{
    public class SaveView : HorizontalPanel<GameData>
    {
        public static event System.Action<GameData> OnSelected;

        [SerializeField] private TMP_Text _dataText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _dayText;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _image;

        private GameData _gameData;

        private void Start()
        {
            _selectButton.onClick.AddListener(Select);
        }

        public override void SetData(GameData gameData)
        {
            _dataText.text = gameData.dateTime;
            _dayText.text = ((int)gameData.time + 1).ToString();
            _nameText.text = gameData.name;
            _gameData = gameData;
        }

        public void Deselect()
        {
            _image.color = Color.white;
        }

        public void EnableButton(bool value)
        {
            _selectButton.interactable = value;
        }

        private void Select()
        {
            OnSelected?.Invoke(_gameData);
        }
    }
}
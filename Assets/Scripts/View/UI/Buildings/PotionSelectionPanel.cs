using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class PotionSelectionPanel : ConstructableMonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _potionSelectButtonPrefab;
        [SerializeField] private Transform _butttonTransform;

        private List<Button> _buttons;
        private GameLibrary _gameLibrary;

        public event System.Action<string> OnItemSelected;

        public override void Construct()
        {
            _buttons = new List<Button>();
            _gameLibrary = GameContext.DIContainer.Resolve<GameLibrary>();
            _closeButton.onClick.AddListener(Close);
        }

        public void Open(List<string> itemIds)
        {
            gameObject.SetActive(true);
            for(var i = 0; i < itemIds.Count; i++)
            {
                var button = _buttons.Count > i ? _buttons[i] : CreateNewButtton(itemIds[i]);
                if (!string.IsNullOrEmpty(itemIds[i]))
                {
                    button.image.sprite = _gameLibrary.GetItem(itemIds[i]).Sprite;
                }
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private Button CreateNewButtton(string itemId)
        {
            var button = Instantiate(_potionSelectButtonPrefab, _butttonTransform);
            button.onClick.AddListener(() => SelectItem(itemId));
            _buttons.Add(button);
            Debug.LogError("нормально ли работает");
            return button;
        }

        private void SelectItem(string itemId)
        {
            OnItemSelected?.Invoke(itemId);
            gameObject.SetActive(false);
        }
    }
}
using DataContainer;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class LootPanel : ConstructableMonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _workersDropdown;
        [SerializeField] private Button _lootButton;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        public event System.Action<int> OnLootCommandCreated;
        public event System.Action OnOpened;

        public override void Construct()
        {
            _lootButton.onClick.AddListener(CreateLootCommand);
            _closeButton.onClick.AddListener(Close);
            _openButton.onClick.AddListener(Open);
        }

        public void UpdateView(List<string> workerDatas)
        {
            _lootButton.interactable = workerDatas.Count > 0;
            _workersDropdown.ClearOptions();
            _workersDropdown.AddOptions(workerDatas);
        }

        private void Open()
        {
            gameObject.SetActive(true);
            OnOpened?.Invoke();
        }

        private void Close()
        {
            gameObject?.SetActive(false);
        }

        private void CreateLootCommand()
        {
            OnLootCommandCreated?.Invoke(_workersDropdown.value);
            Close();
        }
    }
}
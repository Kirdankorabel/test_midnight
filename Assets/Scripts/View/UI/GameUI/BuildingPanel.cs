using Controller.Building;
using DataContainer;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.UI;

namespace View.UI
{
    public class BuildingPanel : ConstructableMonoBehaviour, ICloseable
    {
        [SerializeField] private PlacableObjectDataContainer _config;
        [SerializeField] private Button _buttonPrefab;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _buttonsRoot;

        private Button[] _buttons;

        public bool IsOpen => gameObject.active;

        public event System.Action<PlaceableObjectData> OnBuildingSelected;

        public override void Construct()
        {
            _buttons = new Button[_config.ItemCount];
            for (var i = 0; i < _config.ItemCount; i++)
            {
                var index = i;
                _buttons[index] = Instantiate(_buttonPrefab, _buttonsRoot);
                _buttons[index].onClick.AddListener(() => SelectBuilding(index));
                _buttons[index].image.sprite = _config.GetItem(i).Sprite;
                _buttons[index].GetComponentInChildren<TMP_Text>().text = _config.GetItem(i).PlaceableObjectType.ToString();
            }
            _closeButton.onClick.AddListener(Close);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open(List<PlaceableObjectType> objectTypes)
        {
            Closer.AddCloseablle(this);
            gameObject.SetActive(true);
            for(var i = 0;i < _config.ItemCount;i++)
            {
                _buttons[i].gameObject.SetActive(objectTypes.Contains(_config.GetItem(i).PlaceableObjectType));
            }
        }

        private void SelectBuilding(int index)
        {
            OnBuildingSelected?.Invoke(_config.GetItem(index));
        }
    }
}
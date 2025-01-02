using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class ScrollPanel<T> : ConstructableMonoBehaviour, ICloseable
    {
        [SerializeField] protected HorizontalPanel<T> _horizontalPanelPrefab;
        [SerializeField] protected Transform _rootTransform;
        [SerializeField] protected RectTransform _view;
        [SerializeField] protected Vector2 _offset;
        [SerializeField] private Button _closeButton;
        [SerializeField] private bool _isCloseable = true;

        protected List<HorizontalPanel<T>> _panels = new List<HorizontalPanel<T>>();
        protected List<T> _data;

        public int ItemCount { get; protected set; }

        bool ICloseable.IsOpen => gameObject.active;

        public override void Construct()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(Close);
        }

        public virtual ScrollPanel<T> SetData(List<T> data)
        {
            _data = data;
            return this;
        }

        public virtual void Open()
        {
            if (_isCloseable)
                Closer.AddCloseablle(this);
            foreach (var panel in _panels)
                panel.gameObject.SetActive(false);
            gameObject.SetActive(true);
            if (_data != null)
            {
                for (var i = 0; i < _data.Count; i++)
                {
                    if (_panels.Count <= i)
                    {
                        AddPanel(i);
                    }
                    _panels[i].gameObject.SetActive(true);
                    SetInfo(i);
                }
            }
            UpdateSize();
        }

        protected virtual void AddPanel(int index)
        {
            var newPanel = Instantiate(_horizontalPanelPrefab, _rootTransform);
            _panels.Add(newPanel);
            newPanel.OnSizeCheanged += UpdateSize;
        }

        protected virtual void SetInfo(int index)
        {
            _panels[index].SetData(_data[index]);
        }

        public virtual Vector2 CalculateSize()
        {
            var totalSize = new Vector2(_horizontalPanelPrefab.GetSize().x, 0);
            _panels.ForEach(panel => totalSize += new Vector2(0, (panel.GetSize() + _offset).y));
            return totalSize;
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        protected void UpdateSize()
        {
            _view.sizeDelta = CalculateSize();
        }
    }
}
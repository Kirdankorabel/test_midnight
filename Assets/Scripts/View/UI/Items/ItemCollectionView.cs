using Controller.Items;
using Model.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class ItemCollectionView : UIPanel
    {
        [SerializeField] protected List<Cell> _cells;
        [SerializeField] protected Button _closeButton;
        [SerializeField] private Cell _cellPrefab;

        [Header("Cell spawn options")]
        [SerializeField] private int _spawnedCellCount;
        [SerializeField] private Transform _cellRoot;
        [SerializeField] private bool _enableDrugging;

        protected AbstractItemCollectionConroller _collectionController;

        private ItemFactory _itemFactory;

        public event System.Action OnOpened;
        public event System.Action<int> OnCellMouseDown;
        public event System.Action<int> OnCellMouseUp;

        public int CellsCount => _cells.Count;
        public AbstractItemCollectionConroller CollectionController => _collectionController;

        public override void Construct()
        {
            if (_spawnedCellCount > 0)
            {
                SpawnCells();
            }
            _closeButton.onClick.AddListener(Close);
            var counter = 0;
            _cells.ForEach(cell => InitializeCell(cell, counter++));
            _itemFactory = GameContext.DIContainer.Resolve<ItemFactory>();
        }

        public void UpdateView(AbstractItemCollectionConroller abstractItemCollectionConroller)
        {
            _collectionController = abstractItemCollectionConroller;
            ItemModel item;
            for (var i = 0; i < _cells.Count; i++)
            {
                item = abstractItemCollectionConroller.GetItem(i);
                _cells[i].ClearCell();
                if (!item.IsNullItem && !string.IsNullOrEmpty(item.ItemId))
                {
                    _cells[i].SetItem(_itemFactory.GetItem(item.ItemId));
                }
            }
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
            OnOpened?.Invoke();
        }

        public virtual void SetItemToCell(string itemId, int position)
        {
            _cells[position].SetItem(_itemFactory.GetItem(itemId));
        }

        public virtual void ReleaseItem(int i)
        {
            _cells[i].ClearCell();
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        private int GetCellIndex(Cell cell)
        {
            for (var i = 0; i < CellsCount; i++)
            {
                if (_cells[i] == cell)
                    return i;
            }
            return -1;
        }

        private void SpawnCells()
        {
            _cells.ForEach(cell => cell.DestroyCell());
            for(var i = 0; i < _spawnedCellCount; ++i)
            {
                _cells.Add(Instantiate(_cellPrefab, _cellRoot));
            }
            var counter = 0;
            _cells.ForEach(cell => InitializeCell(cell, counter++));
        }

        private void InitializeCell(Cell cell, int cellIndex)
        {
            cell.SetParentCollection(this);
            cell.OnCellMouseDown += () => OnCellMouseDownHeandler(cellIndex);
            cell.OnCellMouseUp += () => OnCellMouseUpHeandler(cellIndex);
        }

        private void OnCellMouseDownHeandler(int cellIndex)
        {
            OnCellMouseDown?.Invoke(cellIndex);
        }

        private void OnCellMouseUpHeandler(int cellIndex)
        {
            OnCellMouseDown?.Invoke(cellIndex);
        }
    }
}
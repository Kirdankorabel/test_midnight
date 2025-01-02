using Model;
using Model.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class RackViewPanel : ConstructableMonoBehaviour, ICloseable
    {
        [SerializeField] private Button _autocompleteButton;
        [SerializeField] private List<RaskPotionView> _potionButtons;
        [SerializeField] private PotionSelectionPanel _potionSelectionPanel;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _acceptButton;

        private GameLibrary _gameLibrary;
        private int _potionIndex;
        private List<string> _targetItems;

        public event System.Action OnTaskCreated;

        public List<string> TargetItems => _targetItems;

        public bool IsOpen => gameObject.active;

        public override void Construct()
        {
            _gameLibrary = GameContext.DIContainer.Resolve<GameLibrary>();
            GameContext.DIContainer.Resolve<UIManager>().Register(this);

            _closeButton.onClick.AddListener(Close);
            _acceptButton.onClick.AddListener(AcceptTask);
            _potionSelectionPanel.OnItemSelected += SetItemToCell;
            AddListenersForButtons();
        }

        public void Open(RackModel rackModel, ItemCollectionModel itemCollection)
        {
            Closer.AddCloseablle(this);
            _targetItems = new List<string>();
            gameObject.SetActive(true);
            RackCell cell;
            for (var i = 0; i < rackModel.Potions.Count; i++)
            {
                _targetItems.Add(string.Empty);
                cell = rackModel.Potions[i];
                if (!string.IsNullOrEmpty(cell.ItemInCell) && FindOnRack(cell.ItemInCell, itemCollection))
                {
                    var item = itemCollection.Items[i];
                    item.IsFreeItem = false;
                    SetBackImageToCell(i, rackModel.Potions[i]);

                    _targetItems[i] = rackModel.Potions[i].IsStatic ? rackModel.Potions[i].ItemInCell : _targetItems[i];
                }
                else
                {
                    _targetItems[i] = rackModel.Potions[i].ItemInCell;
                }

                SetForwardImageToCell(itemCollection.Items[i], i);
            }
            itemCollection.Items.ForEach(item => item.IsFreeItem = true);
        }

        private void Close()
        {
            gameObject?.SetActive(false);
            _potionSelectionPanel.Close();
        }

        private void AddListenersForButtons()
        {
            for(var i = 0; i < _potionButtons.Count; i++)
            {
                var position = i;
                _potionButtons[position].SetPosition(position);
                _potionButtons[position].OnPressLeft += OpenPotionSelectionPanel;
                _potionButtons[position].OnPressRight += RemovePotion;
            }
        }

        private void OpenPotionSelectionPanel(int position)
        {
            _potionIndex = position;
            var possibleItems = new List<string>();
            _gameLibrary.RecipeDataContainer.Data.ForEach(recipeData => possibleItems.Add(recipeData.ProductId));
            _potionSelectionPanel.Open(possibleItems);
        }

        private void RemovePotion(int position)
        {
            _potionIndex = position;
            SetBackImageToCell(position, null);
            _targetItems[_potionIndex] = string.Empty;
        }

        private void SetItemToCell(string itemId)
        {
            _potionButtons[_potionIndex].SetBackImage(_gameLibrary.GetItem(itemId).Sprite);
            _targetItems[_potionIndex] = itemId;
        }

        private void SetBackImageToCell(int index, RackCell rackPotionModel)
        {
            if (index >= _potionButtons.Count)
            {
                return;
            }
            if (rackPotionModel == null || string.IsNullOrEmpty(rackPotionModel.ItemInCell))
            {
                _potionButtons[index].SetBackImage(null);
            }
            else
            {
                _potionButtons[index].SetBackImage(_gameLibrary.GetItem(rackPotionModel.ItemInCell).Sprite);
            }
        }

        private void AcceptTask()
        {
            OnTaskCreated?.Invoke();
        }

        private void SetForwardImageToCell(ItemModel item, int positon)
        {
            if(positon >= _potionButtons.Count)
            {
                return;//TODO убрать
            }
            if(item.IsNullItem)
            {
                _potionButtons[positon].SetForward(null);
            }
            else
            {
                _potionButtons[positon].SetForward(_gameLibrary.GetItem(item.ItemId).Sprite);
            }
        }

        private bool FindOnRack(string itemId, ItemCollectionModel itemCollection)
        {
            return itemCollection.Items.Any(item => item.ItemId.Equals(itemId) && item.IsFreeItem);
        }

        void ICloseable.Close()
        {
            throw new System.NotImplementedException();
        }
    }
}
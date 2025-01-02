using Model;
using Model.Items;
using System.Collections.Generic;
using View;
using View.UI;

namespace Controller
{
    public class AccountController
    {
        private AccountModel _account;
        private ReseacrhItemCollectionView _reseacrhPanel;
        private GameLibrary _gameLibrary;
        private InventoryController _inventoryController;
        private BalanceView _balanceView;

        public int Balance => _account.Balance;
        public AccountModel Model => _account;

        public AccountController() { }

        public AccountController SetBalanceView(BalanceView balanceView)
        {
            _balanceView = balanceView;
            return this;
        }

        public void InitializeNew(int balance)
        {
            _gameLibrary = GameContext.DIContainer.Resolve<GameLibrary>();
            _inventoryController = GameContext.DIContainer.Resolve<InventoryController>();
            _account = new AccountModel();
            _account.OnBalanceUpdated += _balanceView.UpdateView;
            _account.UpdateBalance(balance);
        }

        public void SetLoaded(AccountModel accountModel)
        {
            if (_account != null)
            {
                _account.OnBalanceUpdated -= _balanceView.UpdateView;
            }
            _gameLibrary = GameContext.DIContainer.Resolve<GameLibrary>();
            _inventoryController = GameContext.DIContainer.Resolve<InventoryController>();
            _account = accountModel;
            _account.OnBalanceUpdated += _balanceView.UpdateView;
        }    

        public void ChangeBalance(int value)
        {
            _account.UpdateBalance(value);
        }

        public void AddMoneyFoItems(List<ItemModel> items)
        {
            for(var i = 0; i < items.Count; i++)
            {
                if (items[i] != null && items[i] != ItemModel.NullItem && !string.IsNullOrEmpty(items[i].ItemId))
                {
                    _account.UpdateBalance(_gameLibrary.GetItem(items[i].ItemId).DefaultPrise);
                }
            }
        }

        public AccountController SetResearhView(ReseacrhItemCollectionView reseacrhPanel)
        {
            _reseacrhPanel = reseacrhPanel;
            _reseacrhPanel.OnItemCreated += OnItemCreateHeandler;
            _reseacrhPanel.OnOpened += OpenReseacrhpanel;
            return this;
        }

        public void OnItemAddedHeandler()
        {
            var recipe = _gameLibrary.RecipeDataContainer.GetRecipe(_reseacrhPanel.GetItemIds());
            if(recipe != null && _account.OpenedRecipes.Contains(recipe.ItemId))
            {
                _reseacrhPanel.UpdateProductImage(_gameLibrary.GetItem(recipe.ProductId).Sprite);
            }
            else
            {
                _reseacrhPanel.UpdateProductImage(null);
            }
        }

        public void OnItemCreateHeandler()
        {
            var recipe = _gameLibrary.RecipeDataContainer.GetRecipe(_reseacrhPanel.GetItemIds());
            var item = _gameLibrary.GetItem(recipe.ProductId);
            _reseacrhPanel.CollectionController.TryToSetItem(new ItemModel(item), _reseacrhPanel.ComponentCellCount);
            if(!_account.OpenedRecipes.Contains(recipe.ItemId))
            {
                _account.OpenRecipe(recipe.ItemId);
            }
        }

        private void OpenReseacrhpanel()
        {
            _reseacrhPanel.SetItems();
            _inventoryController.OpenInventoryLeft();
        }
    }
}
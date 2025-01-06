using Controller;
using Controller.Characters;
using Controller.Items;
using DataContainer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class RecipeHorizontalPanel : HorizontalPanel<RecipeData>
    {
        [SerializeField] private List<Image> _components;
        [SerializeField] private Image _productImage;
        [SerializeField] private Button _addOrderButton;

        private RecipeData _recipe;
        private GameLibrary _gameLibrary;
        private WorkController _wworkController;
        private InventoryController _inventoryController;

        public event System.Action<RecipeData> OnClick;

        private void Start()
        {
            _addOrderButton.onClick.AddListener(AddOrder);
        }

        public override void SetData(RecipeData recipe)
        {
            _recipe = recipe;
            _gameLibrary = GameContext.DIContainer.Resolve<GameLibrary>();
            _inventoryController = GameContext.DIContainer.Resolve<InventoryController>();
            _wworkController = GameContext.DIContainer.Resolve<WorkController>();
            for (var i = 0; i < _components.Count; i++)
            {
                if (i < recipe.Components.Count)
                {
                    _components[i].gameObject.SetActive(true);
                    _components[i].sprite = _gameLibrary.GetItemSprite(recipe.Components[i]);
                }
                else
                {
                    _components[i].gameObject.SetActive(false);
                }
            }
            _productImage.sprite = _gameLibrary.GetItemSprite(recipe.ProductId);
            //_addOrderButton.interactable = _inventoryController.CheckItems(recipe.Components);
        }

        private void AddOrder()
        {
            _wworkController.AddTask(_recipe);
        }
    }
}
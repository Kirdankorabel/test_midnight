using Core;
using DataContainer;
using Model;
using Model.Items;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class GameLibrary : MonoBehaviour
    {
        private List<ItemDataContainer> _itemDatas;
        private AlcemicalPlantDataContainer _alcemicalPlantDataContainer;
        private PlacableObjectDataContainer _placableObjectDataContainer;
        private RecipeDataContainer _recipeDataContainer;
        private Sprite _defaaultSprite;

        public AlcemicalPlantDataContainer AlcemicalPlantDataContainer => _alcemicalPlantDataContainer;
        public RecipeDataContainer RecipeDataContainer => _recipeDataContainer;

        private LibraryModel _LibraryModel;

        public List<ItemDataContainer> ItemDatas => _itemDatas;

        public GameLibrary() { }

        public GameLibrary SetItemDatas(List<ItemDataContainer> itemDatas)
        {
            _itemDatas = itemDatas;
            return this;
        }

        public GameLibrary SetAlcemicalPlantDataContainer(AlcemicalPlantDataContainer alcemicalPlantDataContainer)
        {
            _alcemicalPlantDataContainer = alcemicalPlantDataContainer;
            return this;
        }

        public GameLibrary SetPlacableObjectDataContainer(PlacableObjectDataContainer placableObjectDataContainer)
        {
            _placableObjectDataContainer = placableObjectDataContainer;
            return this;
        }

        public GameLibrary SetRecipes(RecipeDataContainer recipes)
        {
            _recipeDataContainer = recipes;
            return this;
        }

        public void Initialize()
        {
            _LibraryModel = new LibraryModel();
            _LibraryModel.Initialize();
        }

        public Sprite GetDefaultSprite()
        {
            return _defaaultSprite;
        }

        public void SetData(LibraryModel LibraryModel)
        {
            _LibraryModel = LibraryModel;
        }

        public LibraryModel GetData()
        {
            return _LibraryModel;
        }

        public ItemData GetItem(string itemId)
        {
            var dataContainer = _itemDatas.Find(container => container.ContainsItem(itemId));
            var result = dataContainer == null ? null : dataContainer.GetItem(itemId);

            if (result == null || string.IsNullOrEmpty(itemId))
            {
                GameLog.AddMassage("Item not found: " + itemId);
            }
            return result;
        }

        public ItemData GetProduct(string recipeId)
        {
            var itemId = _recipeDataContainer.GetItem(recipeId).ProductId;
            var dataContainer = _itemDatas.Find(container => container.ContainsItem(itemId));
            var result = dataContainer == null ? null : dataContainer.GetItem(itemId);

            if (result == null)
            {
                GameLog.AddMassage("Item not found: " + itemId);
            }
            return result;
        }

        public ItemData GetProduct(List<string> components)
        {
            var recipe = RecipeDataContainer.GetRecipe(components);
            return GetItem(recipe.ProductId);
        }

        public Sprite GetItemSprite(string itemId)
        {
            return GetItem(itemId) == null || GetItem(itemId).Sprite == null ? GetDefaultSprite() : GetItem(itemId).Sprite;
        }

        public AlcemicalPlantData GetAlcemicalPlantData(string id)
        {
            return _alcemicalPlantDataContainer.GetAlcemicalPlantData(id);
        }

        public PlaceableObjectData GetBuildingInfo(string id)
        {
            return _placableObjectDataContainer.GetItem(id);
        }

        public void AddNewItem(ItemData itemData)
        {
            if (!_LibraryModel.CustomItems.Contains(itemData))
            {
                _LibraryModel.CustomItems.Add(itemData);
            }
        }

        public void AddItemsFromRecipe(List<ItemData> newItems)
        {
            newItems.ForEach(product => AddNewItem(product));
        }

        public List<ItemModel> GetItems(List<string> itemIds, int totalCount)
        {
            var items = new List<ItemModel>();
            itemIds.ForEach(itemId => items.Add(new ItemModel(GetItem(itemId))));
            var count = items.Count;
            for (var i = count; i < totalCount; i++)
            {
                items.Add(ItemModel.NullItem);
            }
            return items;
        }
    }
}
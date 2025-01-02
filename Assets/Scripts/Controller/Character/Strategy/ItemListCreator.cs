using Controller.Items;
using Model.Items;
using System.Collections.Generic;
using System.Linq;

public static class ItemListCreator
{
    public static List<ItemModel> CreateItemListAndMarkItems(List<string> wishList, AbstractItemCollectionConroller itemCollectionController)
    {
        var result = new List<ItemModel>();
        var collection = itemCollectionController.ItemCollectionModel;
        for (int i = 0; i < wishList.Count; i++)
        {
            if (!string.IsNullOrEmpty(wishList[i]) 
                && collection.Items.Any(item => !item.IsNullItem && item.ItemId.Equals(wishList[i]) && item.IsFreeItem))
            {
                var item = collection.Items.Find(item => !item.IsNullItem && item.ItemId.Equals(wishList[i]) && item.IsFreeItem);
                item.IsFreeItem = false;
                result.Add(item);
            }
        }
        return result;
    }
}

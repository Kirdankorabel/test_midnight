using DataContainer;
using Model.Items;
using System.Collections.Generic;

namespace Controller.Items
{
    public abstract class AbstractItemCollectionConroller
    {
        public event System.Action<ItemModel> OnItemAdded;

        public abstract int Size { get; }
        public abstract ItemCollectionModel ItemCollectionModel { get;}

        public abstract void SetCellData(List<CellData> cellData);
        public abstract bool ReleseItem(int cellIndex);
        public abstract void ReleseItem(ItemModel itemModel);
        public abstract bool TryToSetItem(ItemModel item, int cellIndex);
        public abstract bool SwapItem(ItemModel item, int cellIndex);
        public abstract ItemModel GetItem(int i);
        public abstract void AddItem(ItemData itemData);
        public abstract void AddItem(ItemModel item);
        public abstract List<ItemModel> GetItems(List<string> itemIds);
    }
}
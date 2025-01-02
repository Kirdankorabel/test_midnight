using View.UI;

namespace Controller.Items
{
    public class CollectionItemDragger
    {
        private AbstractItemCollectionConroller _collectionConroller;
        private ItemCollectionView _itemCollectionView;

        public event System.Action<int> OnDraggedFrom;
        public event System.Action<int> OnDraggedTo;

        public CollectionItemDragger(AbstractItemCollectionConroller controller, ItemCollectionView itemCollectionView)
        {
            _collectionConroller = controller;
            _itemCollectionView = itemCollectionView;
            itemCollectionView.OnCellMouseDown += SelectItemInCell;
        }

        private void SelectItemInCell(int index)
        {

        }
    }
}
using System.Collections.Generic;
using View;
using View.UI;

namespace Controller.Items
{
    public class DraggingController
    {
        private DraggingView _draggingView;
        private List<CollectionItemDragger> _collectionControllers = new List<CollectionItemDragger>();

        public DraggingController(DraggingView draggingView) 
        { 
            _draggingView = draggingView;
        }

        public void AddCollectionController(CollectionItemDragger conroller)
        {
            _collectionControllers.Add(conroller);
            conroller.OnDraggedTo += (index) => DruggingTo(conroller, index);
            conroller.OnDraggedFrom += (index) => DraggingFrom(conroller, index);
        }

        private void DraggingFrom(CollectionItemDragger conroller, int cellIndex)
        {

        }

        private void DruggingTo(CollectionItemDragger conroller, int cellIndex)
        {

        }
    }
}
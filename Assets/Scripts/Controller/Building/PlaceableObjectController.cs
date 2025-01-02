using Controller.Items;
using Model.Building;
using View.Game;

namespace Controller.Building
{
    public class PlaceableObjectController
    {
        protected PlaceableObjectModel _objectModel;
        protected PlaceableObject _placeableObject;
        protected AbstractItemCollectionConroller _inventoryController;

        public AbstractItemCollectionConroller ItemCollectionController => _inventoryController;

        public string PlObjectId => _objectModel.Id;
        public string PlObjectType => _objectModel.PlaceableObjectType;

        public PlaceableObjectController() { }

        public virtual void Construct() { }

        public virtual PlaceableObjectController SetModel(PlaceableObjectModel placeableObjectModel)
        {
            _objectModel = placeableObjectModel;
            return this;
        }

        public virtual PlaceableObjectController SetView(PlaceableObject placeableObject)
        {
            _placeableObject = placeableObject;
            _placeableObject.OnMouseDownEvent += OpenUI;
            return this;
        }

        public virtual void SaveData() { }

        public void Destroy()
        {
            _placeableObject.Destroy();
        }

        public virtual void OpenUI()
        {
        }
    }
}
using DataContainer;
using Model.Building;

namespace Controller.Building
{
    public class BuildingControllerFactory
    {
        public static PlaceableObjectController CreateController(PlaceableObjectModel placeableObject)
        {
            switch (placeableObject.PlaceableObjectType)
            {
                case "Plant":
                    return new PlantController(placeableObject.ItemCollectionModel.Size - 1);
                case "Storage":
                    return new StorageController(0);
                case "Rask":
                    return new RackController();
                default:
                    return new PlaceableObjectController();

            }
        }
    }
}
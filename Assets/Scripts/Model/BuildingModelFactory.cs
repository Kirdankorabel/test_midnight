using DataContainer;
using Model.Building;

namespace Model
{
    public class BuildingModelFactory
    {
        public static PlaceableObjectModel CreateModel(PlaceableObjectData placeableObject, string id)
        {
            switch (placeableObject.ItemId)
            {
                case "Rask":
                    return new PlaceableObjectModel(id);
                default:
                    return new PlaceableObjectModel(id);

            }
        }
    }
}
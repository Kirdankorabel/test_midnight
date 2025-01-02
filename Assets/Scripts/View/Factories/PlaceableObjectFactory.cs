using Controller.Building;
using DataContainer;
using UnityEngine;
using View.Game;

namespace View.Factory
{
    public class PlaceableObjectFactory : MonoBehaviour
    {
        [SerializeField] private PlacableObjectDataContainer _placableObjectDataContainer;

        public PlaceableObject InstantiatePlObject(PlaceableObjectController placeableObjectController)
        {
            var result = Instantiate(_placableObjectDataContainer.GetItem(placeableObjectController.PlObjectType).Prefab);
            result.SetController(placeableObjectController);
            return result;
        }
    }
}
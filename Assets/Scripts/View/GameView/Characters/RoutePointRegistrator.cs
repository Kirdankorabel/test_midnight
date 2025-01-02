using Controller.Building;
using Controller.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace View.Game.Characters
{
    public class RoutePointRegistrator : MonoBehaviour
    {
        [SerializeField] private List<RoutePointData> _routePoints;
        [SerializeField] private bool _isBuilding = true;
        private UseablePlaceableObjectController _useablePlaceableObjectController;

        private RouteController _routeController;

        public void RegistratePoints()
        {
            _routeController = GameContext.DIContainer.Resolve<RouteController>();
            if(_isBuilding)
            {
                _routePoints.ForEach(
                    rPoint => _routeController.AddPoint(_useablePlaceableObjectController.PlObjectId, rPoint, _useablePlaceableObjectController));
            }
            else
            {
                _routePoints.ForEach(
                    rPoint => _routeController.AddPoint(rPoint.ParentId, rPoint, null));
            }           
        }

        public void RemovePoitns()
        {
            _routeController = GameContext.DIContainer.Resolve<RouteController>();
            _routePoints.ForEach(rPoint => _routeController.RemovePoint(rPoint.ParentId, rPoint));
        }

        public RoutePointRegistrator SetPlObjectController(UseablePlaceableObjectController useablePlaceableObjectController)
        {
            _useablePlaceableObjectController = useablePlaceableObjectController;
            return this;
        }
    }

    [System.Serializable]
    public class RoutePointData
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private string _routeName;
        [SerializeField] private string _parentId;

        public Transform Transform => _transform;
        public string RouteName => _routeName;
        public string ParentId => _parentId;
    }
}
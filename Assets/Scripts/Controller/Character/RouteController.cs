using Controller.Building;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View.Game.Characters;

namespace Controller.Characters
{
    public class RouteController
    {
        private Dictionary<(string, string), Route> routes = new Dictionary<(string, string), Route>();

        public void AddPoint(string pointId, RoutePointData point, UseablePlaceableObjectController controller = null)
        {
            if (routes.ContainsKey((pointId, point.RouteName)))
            {
                routes[(pointId, point.RouteName)].AddPoint(new RoutePoint(point.Transform.position, pointId, controller));
            }
            else
            {
                routes.Add((pointId, point.RouteName), new Route(point.RouteName));
                routes[(pointId, point.RouteName)].AddPoint(new RoutePoint(point.Transform.position, pointId, controller));
            }
        }

        public void RemovePoint(string buildingId, RoutePointData point)
        {
            routes[(buildingId, point.RouteName)].RemovePoint(point.Transform.position);
        }

        public Route GetRoute(string buildingId, string routeName)
        {
            if (routes.ContainsKey((buildingId, routeName)))
            {
                return routes[(buildingId, routeName)];
            }
            else 
            {
                return new Route(string.Empty);
            }
        }

        public Route GetRoute(string routeName)
        {
            if (routes.Any(pair => pair.Key.Item2.Equals(routeName)))
            {
                return routes.First(pair => pair.Key.Item2.Equals(routeName)).Value;
            }
            else
            {
                return new Route(string.Empty);
            }
        }
    }

    public class Route
    {
        public readonly string routeType;

        private List<RoutePoint> _points;

        public List<RoutePoint> RoutePoints => _points;

        public Route(string routeType)
        {
            this.routeType = routeType;
            _points = new List<RoutePoint>();
        }

        public void AddPoint(RoutePoint point)
        {
            _points.Add(point);
        }

        public void RemovePoint(Vector3 position)
        {
            _points.RemoveAll(p => p.position ==  position);
        }

        public RoutePoint GetPoint()
        {
            if (_points.Count == 0)
            {
                return null;
            }
            RoutePoint result = _points[0];
            for (var i = 1; i < _points.Count; i++)
            {
                if (_points[i].IsFree)
                {
                    result = _points[i].QueueSize < result.QueueSize ? _points[i] : result;
                }
            }
            return result;
        }
    }
}
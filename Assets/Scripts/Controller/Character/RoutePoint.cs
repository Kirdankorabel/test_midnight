using Controller.Building;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class RoutePoint
    {
        public readonly Vector3 position;
        public readonly UseablePlaceableObjectController building;
        private string _id;
        private bool _isFree = true;

        private Queue<Action> _actions = new Queue<Action>();

        public bool IsFree
        {
            get => _isFree;
            set
            {
                _isFree = value;
                if(value && _actions.Count > 0)
                {
                    _actions.Dequeue()?.Invoke();
                    _isFree = false;
                }
            }
        }

        public string Id => _id;

        public int QueueSize => _actions.Count;

        public UseablePlaceableObjectController PlaceableObjectController => building;

        public RoutePoint(Vector3 position, string id,  UseablePlaceableObjectController placeableObjectController)
        {
            this._id = id;
            this.position = position;
            building = placeableObjectController;
        }

        public void AddActionToQueue(Action action)
        {
            _actions.Enqueue(action);
        }
    }
}
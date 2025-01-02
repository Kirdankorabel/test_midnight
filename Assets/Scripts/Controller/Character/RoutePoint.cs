using Controller.Building;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Characters
{
    public class RoutePoint
    {
        public readonly Vector3 position;
        public readonly UseablePlaceableObjectController controller;
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

        public int QueueSize => _actions.Count;

        public UseablePlaceableObjectController PlaceableObjectController => controller;

        public RoutePoint(Vector3 position)
        {
            this.position = position;
        }

        public RoutePoint(Vector3 position, UseablePlaceableObjectController placeableObjectController)
        {
            this.position = position;
            controller = placeableObjectController;
        }

        public void AddActionToQueue(Action action)
        {
            _actions.Enqueue(action);
        }
    }
}
using Controller.Building;
using Model;
using Model.Building;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using View.Game.Characters;

namespace View.Game
{
    public class PlaceableObject : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private GameObject[] _meshs;
        [SerializeField] protected PlaceableObjectType _buildingType;
        [SerializeField] protected string _panelName;
        [SerializeField] private string _objectId;
        [SerializeField] private RoutePointRegistrator _pointRegistrator;

        private PlaceableObjectController _controller;

        public event System.Action OnMouseDownEvent;

        public string Id => _objectId;
        public PlaceableObjectType BuildingType => _buildingType;
        public string PanelName => _panelName;

        public void SetLayer(int layer)
        {
            foreach (var child in _meshs)
            {
                child.layer = layer;
            }
        }

        public void SetController(PlaceableObjectController controller)
        {
            _controller = controller;
        }

        public void RegistratePoints()
        {
            _pointRegistrator.SetPlObjectController(_controller as UseablePlaceableObjectController);
            _pointRegistrator.RegistratePoints();
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            OnMouseDownEvent?.Invoke();
        }

        public void Destroy()
        {
            OnMouseDownEvent = null;
            if (_pointRegistrator)
            {
                _pointRegistrator.RemovePoitns();
            }
            Destroy(gameObject);
        }
    }
}
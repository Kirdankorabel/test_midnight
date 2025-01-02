using Controller.Building;
using Unity.AI.Navigation;
using UnityEngine;
using View.Factory;

namespace View.Game
{
    public class RoomView : MonoBehaviour
    {
        [SerializeField] private BuildingPlace[] _buildingPlaces;
        [SerializeField] private PlaceableObject[] _staticPlaceeableObjects;
        [SerializeField] private NavMeshSurface _nawMeshSufrace;
        [SerializeField] private RoomBlockerView _roomBlocker;

        private PlaceableObject[] _placeableObjects;
        private PlaceableObjectFactory _placeableObjectFactory;

        public event System.Action<int> OnPlaceSelected;
        public event System.Action OnObjectPlaced;

        public int GetPositionCount => _buildingPlaces.Length;
        public RoomBlockerView RoomBlocker => _roomBlocker;

        public void Construct()
        {
            _placeableObjectFactory = GameContext.DIContainer.Resolve<PlaceableObjectFactory>();
            for (var i = 0; i < _buildingPlaces.Length; i++)
            {
                var index = i;
                _buildingPlaces[index].OnSelected += () => OpenBuildingMenu(index);
            }
            _placeableObjects = new PlaceableObject[_buildingPlaces.Length + _staticPlaceeableObjects.Length];
            if(_roomBlocker != null)
            {
                _roomBlocker.OnRoomOpened += _nawMeshSufrace.BuildNavMesh;
            }
        }

        public void Enable(bool enabled)
        {
            for (var i = 0; i < _buildingPlaces.Length; i++)
            {
                if (_placeableObjects[i] == null)
                {
                    _buildingPlaces[i].Enable(enabled);
                }
            }
        }

        public void PlaceBuilding(PlaceableObjectController buildingController, int index)
        {
            if (_placeableObjects[index] != null)
            {
                Destroy(_placeableObjects[index].gameObject);
            }
            var building = _placeableObjectFactory.InstantiatePlObject(buildingController);
            buildingController.SetView(building);
            var position = _buildingPlaces[index].transform.position;
            building.transform.position = new Vector3(position.x, 1, position.z);
            building.transform.rotation = _buildingPlaces[index].Rotation;
            building.RegistratePoints();
            _placeableObjects[index] = building;
            _buildingPlaces[index].Enable(false);
            _nawMeshSufrace.BuildNavMesh();
        }

        public BuildingPlace GetBuildingPlace(int index)
        {
            return _buildingPlaces[index];
        }

        private void OpenBuildingMenu(int index)
        {
            OnPlaceSelected?.Invoke(index);
        }
    }
}
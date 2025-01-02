using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View.Game
{
    public class BuildingPlace : MonoBehaviour
    {
        [SerializeField] private Vector3 _eulerAngle;
        [SerializeField] private List<PlaceableObjectType> _placeableObjectTypes;

        public Quaternion Rotation => Quaternion.Euler(_eulerAngle);
        public List<PlaceableObjectType> PlaceableObjectTypes => _placeableObjectTypes;

        public event System.Action OnSelected;

        public void Enable(bool enabled)
        {
            gameObject.SetActive(enabled);
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            OnSelected?.Invoke();
        }
    }
}
using UnityEngine;

namespace View.UI
{
    public abstract class HorizontalPanel<T> : MonoBehaviour
    {
        [SerializeField] private Vector2 _size;

        public virtual event System.Action OnSizeCheanged;

        public virtual Vector2 GetSize()
        {
            return _size;
        }

        public abstract void SetData(T data);

        private void OnDestroy()
        {
            OnSizeCheanged = null;
        }
    }
}
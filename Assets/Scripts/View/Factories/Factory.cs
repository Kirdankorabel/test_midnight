using UnityEngine;
using UnityEngine.Pool;

namespace View
{
    public abstract class Factory<T> : ConstructableMonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _maxSize = 100;

        private ObjectPool<T> _pool;

        public override void Construct()
        {
            _pool = new ObjectPool<T>(
                InstantiateItem,
                OnGet,
                OnReleas,
                OnDestroyElement,
                false,
                _defaultCapacity,
                _maxSize);
        }

        public virtual T GetItem()
        {
            return _pool.Get();
        }

        public virtual void ReleaseItem(T dObject)
        {
            _pool.Release(dObject);
        }

        private void OnDestroy()
        {
            _pool.Dispose();
        }

        #region pool methods
        protected virtual T InstantiateItem()
        {
            var item = Instantiate(_prefab);
            return item;
        }

        protected virtual void OnGet(T gameObject)
        {
            gameObject.gameObject.SetActive(true);
            gameObject.transform.parent = transform;
        }

        protected virtual void OnReleas(T gameObject)
        {
            gameObject.gameObject.SetActive(false);
            gameObject.transform.parent = transform;
        }
        protected virtual void OnDestroyElement(T gameObject) { }
        #endregion
    }
}
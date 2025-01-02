using DI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace View.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<ConstructableMonoBehaviour> _UIElements;

        private readonly Dictionary<Type, object> _registrations = new Dictionary<Type, object>();

        public void InitializeUI()
        {
            _UIElements.ForEach(element => element.Construct());
        }

        public T GetPanel<T>()
        {
            if(_registrations.ContainsKey(typeof(T)))
            {
                return (T)_registrations[typeof(T)];
            }
            throw new Exception($"UIManager: Not found UIElement with type:{typeof(T)}");
        }

        public void Register<T>(T instance)
        {
            _registrations[typeof(T)] = instance;
        }

    }
}
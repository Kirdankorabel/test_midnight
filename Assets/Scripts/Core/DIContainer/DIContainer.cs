using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public class DIContainer
    {
        private readonly DIContainer _parentContainer;
        private readonly Dictionary<(string, Type), DIRegistration> _registrations = new Dictionary<(string, Type), DIRegistration>();
        private readonly HashSet<(string, Type)> _resolutions = new HashSet<(string, Type)> ();

        public DIContainer() { }
        public DIContainer(DIContainer parentContainer)
        {
            _parentContainer = parentContainer;
        }

        public void RegisterSingletone<T>(Func<DIContainer, T> factory)
        {
            Register((string.Empty, typeof(T)), factory, true);
        }

        public void RegisterSingletone<T>(string tag, Func<DIContainer, T> factory)
        {
            Register((tag, typeof(T)), factory, true);
        }
        
        public void RegisterTransient<T>(Func<DIContainer, T> factory)
        {
            Register((string.Empty, typeof(T)), factory, false);
        }

        public void RegisterTransient<T>(string tag, Func<DIContainer, T> factory)
        {
            Register((tag, typeof(T)), factory, false);
        }

        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(instance, string.Empty);
        }

        public void RegisterInstance<T>(T instance, string tag)
        {
            var key = (tag, typeof(T));
            if (_registrations.ContainsKey(key))
            {
                throw new Exception($"DI: Factory with tag:{key.Item1} and type:{key.Item2} has already contains");
            }
            _registrations[key] = new DIRegistration(true, instance);
        }

        public T Resolve<T>()
        {
            return Resolve<T>(string.Empty);
        }

        public T Resolve<T>(string tag)
        {
            var key = (tag, typeof(T));
            if(_resolutions.Contains(key))
            {
                throw new Exception($"DI: Ciclick dependency for tag:{key.Item1} and type:{key.Item2}");
            }
            try
            {
                if (_registrations.TryGetValue(key, out var registration))
                {
                    if (registration.IsSingletone)
                    {
                        if (registration.Instance == null)
                        {
                            registration.Instance = registration.Factory(this);
                        }
                        return (T)registration.Instance;
                    }
                    return (T)registration.Factory(this);
                }

                if (_parentContainer != null)
                {
                    _parentContainer.Resolve<T>(tag);
                }
            }
            finally
            {
                _resolutions.Remove(key);
            }

            throw new Exception($"DI: Not found item with tag:{key.Item1} and type:{key.Item2}");
        }
        
        private void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool isSingletone)
        {
            if(_registrations.ContainsKey(key))
            {
                throw new Exception($"DI: Factory with tag:{key.Item1} and type:{key.Item2} has already contains");
            }
            _registrations[key] = new DIRegistration(f => factory(f), isSingletone);
        }
    }
}
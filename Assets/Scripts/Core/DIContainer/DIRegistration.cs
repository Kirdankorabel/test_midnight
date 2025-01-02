using System;

namespace DI
{
    public class DIRegistration
    {
        public Func<DIContainer, object> Factory { get; private set; }
        public bool IsSingletone { get; private set; }
        public object Instance { get; set; }

        public DIRegistration(Func<DIContainer, object> factory, bool isSingletone)
        {
            Factory = factory;
            IsSingletone = isSingletone;
        }

        public DIRegistration(bool isSingletone, object instance)
        {
            IsSingletone = isSingletone;
            Instance = instance;
        }
    }
}
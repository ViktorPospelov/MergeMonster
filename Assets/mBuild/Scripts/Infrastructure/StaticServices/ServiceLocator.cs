using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void AddService<T>(T service) where T : class 
        {
            var type = typeof(T);
            _services.Add(type, service);
        }

        public static T GetService<T>() where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(typeof(T)))
            {
                object service = _services[typeof(T)]; 
                return service as T;
            }
            else
            {
                throw new InvalidOperationException($"Service of type{nameof(T)} not found");
            }
        }
    }
}

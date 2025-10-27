using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RomCom.Common.ServiceInstallers.Attributes;

namespace RomCom.Common.ServiceInstallers
{
    public static class MappedTypesFactory
    {
        public static IEnumerable<(Type ServiceType, Type ImplementationType, ServiceLifetime Lifetime)> GetMappedTypes(params Assembly[] assemblies)
        {
            var mappedTypes = new List<(Type, Type, ServiceLifetime)>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract);

                foreach (var type in types)
                {
                    ServiceLifetime? lifetime = null;

                    if (type.GetCustomAttribute<ScopedServiceAttribute>() != null)
                        lifetime = ServiceLifetime.Scoped;
                    else if (type.GetCustomAttribute<TransientServiceAttribute>() != null)
                        lifetime = ServiceLifetime.Transient;
                    else if (type.GetCustomAttribute<SingletonServiceAttribute>() != null)
                        lifetime = ServiceLifetime.Singleton;

                    if (lifetime.HasValue)
                    {
                        var interfaceType = type.GetInterfaces().FirstOrDefault();
                        if (interfaceType != null)
                        {
                            mappedTypes.Add((interfaceType, type, lifetime.Value));
                        }
                        else
                        {
                            // Register as self if no interface
                            mappedTypes.Add((type, type, lifetime.Value));
                        }
                    }
                }
            }

            return mappedTypes;
        }
    }

    public enum ServiceLifetime
    {
        Scoped,
        Transient,
        Singleton
    }
}


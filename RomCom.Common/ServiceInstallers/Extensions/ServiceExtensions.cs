using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RomCom.Common.ServiceInstallers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterApplicationServices(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            var mappedTypes = MappedTypesFactory.GetMappedTypes(assemblies);

            foreach (var (serviceType, implementationType, lifetime) in mappedTypes)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Scoped:
                        services.AddScoped(serviceType, implementationType);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(serviceType, implementationType);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(serviceType, implementationType);
                        break;
                }
            }

            return services;
        }
    }
}


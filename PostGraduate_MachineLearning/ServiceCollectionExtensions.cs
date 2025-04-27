using Autofac.Core;
using EmotionClassifier.Configuration;
using EmotionClassifier.Models.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Reflection;

namespace Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Type = t,
                Attribute = t.GetCustomAttribute<ServiceRegistrationAttribute>()
            })
            .Where(x => x.Attribute != null);

        foreach (var serviceType in serviceTypes)
        {
            if (serviceType.Attribute.ServiceType != null)
            {
                services.Add(new ServiceDescriptor(serviceType.Attribute.ServiceType, serviceType.Type,serviceType.Attribute.Lifetime));
            }
            else
            {
                services.Add(new ServiceDescriptor(serviceType.Type, serviceType.Type, serviceType.Attribute.Lifetime));
            }
        }
        return services;
    }
}


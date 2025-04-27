using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionClassifier.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceRegistrationAttribute : Attribute
    {
        public ServiceRegistrationAttribute(Type serviceType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }
        public ServiceRegistrationAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
            ServiceType = null;
        }

        public Type? ServiceType { get; }
        public ServiceLifetime Lifetime { get; }
    }
}

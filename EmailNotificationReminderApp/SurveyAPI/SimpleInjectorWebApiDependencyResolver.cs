using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace SurveyAPI
{
    internal class SimpleInjectorWebApiDependencyResolver : IDependencyResolver
    {
        private readonly Container container;

        public SimpleInjectorWebApiDependencyResolver(Container container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
           return this;
        }

        public void Dispose()
        {
            
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)this.container).GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            IServiceProvider provider = container;
            Type CollectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var services = (IEnumerable<object>)provider.GetService(CollectionType);
            return services ?? Enumerable.Empty<object>();
        }
    }
}
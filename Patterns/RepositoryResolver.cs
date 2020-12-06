using FiveGears.Core.Interfaces.Patterns;
using System;

namespace FiveGears.Core.Patterns
{
    public class RepositoryResolver : IRepositoryResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TRepository Resolve<TRepository>()
        {
            return (TRepository)_serviceProvider.GetService(typeof(TRepository));
        }
    }
}
